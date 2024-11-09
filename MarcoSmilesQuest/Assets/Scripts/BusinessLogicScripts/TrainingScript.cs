using System;
using Newtonsoft.Json;
using OVRSimpleJSON;
using UnityEngine;

public class TrainingScript : MonoBehaviour
{
    public static event Action<Note> PlayNote;

    [SerializeField]
    private HandReader _handReader;
    [SerializeField]
    private TrainingCanvasUIManager _trainingCanvasUIManager;
    [SerializeField]
    private ServerGateway _serverGateway;

    private Note _noteToPlay;
    private int _cycles = 0;
    private float _poseTime = 4;
    private const float _fedbackTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnEnable()
    {
        _cycles = 0;
        NextFourNotes.GenerateNextFourNotes();
    }

    private void Train()
    {

        // Get a random note to play
        _noteToPlay = NextFourNotes.GetNextNote();

        // Tell the user to play it
        _trainingCanvasUIManager.SetNoteToPlay(_noteToPlay.ToString());
        _trainingCanvasUIManager.SetModelGuess("...");
        _trainingCanvasUIManager.SetStatus("Keep steady pose..");

        // Collect data and send it to the server
        Invoke("CollectDataAndSendToServer", _poseTime);

    }

    public void StartTraining()
    {
        Invoke("Train", 0);
    }

    public void StopTraining()
    {
        CancelInvoke("Train");
        CancelInvoke("CollectDataAndSendToServer");
        _serverGateway.EndTraining((response) =>
        {
            _trainingCanvasUIManager.SetModelGuess("...");
            _trainingCanvasUIManager.SetNoteToPlay("...");
            _trainingCanvasUIManager.SetStatus("...");
        });
    }

    private void CollectDataAndSendToServer()
    {

        if (!_handReader.IsHandTracking())
        {
            Debug.LogError("Hand tracking lost!");
            Invoke("Train", 0);
        }
        else
        {
            // Init RequestWrapper
            RequestWrapper requestWrapper = new RequestWrapper(_noteToPlay.ComputeDistance());

            // Get the hand data
            for (int i = 0; i < 128; i++)
            {
                HandWrapper leftHandWrapper = _handReader.ReadHand(HandSide.Left);
                requestWrapper.HandWrappers.Add(leftHandWrapper);
            }
            Debug.LogError("HandWrapper: " + JsonConvert.SerializeObject(requestWrapper.HandWrappers[0]));

            SendToServer(requestWrapper);
        }
    }

    private void SendToServer(RequestWrapper requestWrapper)
    {
        // Send the hand data to the server
        _serverGateway.SendHandData(requestWrapper, (response) =>
        {
            int messageAsInt = int.Parse(JSON.Parse(response)["message"]);
            Note notePredicted = NotesList.Notes[messageAsInt];
            _trainingCanvasUIManager.SetModelGuess(notePredicted.ToString());
            PlayNote?.Invoke(notePredicted);
            UpdateStatus(notePredicted);
            UpdatePoseTime();
            Invoke("Train", _fedbackTime);
        });
    }

    private void UpdateStatus(Note modelGuess)
    {
        if (modelGuess == _noteToPlay)
        {
            _trainingCanvasUIManager.SetStatus("Correct!");
        }
        else
        {
            _trainingCanvasUIManager.SetStatus("Incorrect, keep going!");
        }
    }

    private void UpdatePoseTime()
    {
        if (_poseTime >= 1.5f && _cycles % 5 == 0)
        {
            _poseTime = _poseTime - 0.5f;
        }
        _cycles++;
    }

}
