using System;
using System.Data;
using HandUtilitites;
using OVRSimpleJSON;
using ServerCommunicationUtilities;
using UnityEngine;

public class TrainingScript : MonoBehaviour
{

    [SerializeField]
    private HandReader handReader;
    [SerializeField]
    private TrainingCanvasUIManager trainingCanvasUIManager;
    [SerializeField]
    private ServerGateway serverGateway;
    private NextFourNotes nextFourNotes;
    private NotesUtilities.Note noteToPlay;
    private static int poseTime = 4;
    public static event Action<NotesUtilities.Note> PlayNote;


    // Start is called before the first frame update
    void Start()
    {
        serverGateway.HelloWorld((response) =>
        {
            Debug.Log(response);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        nextFourNotes = new NextFourNotes();
    }
    
    private void Train()
    {

        // Get a random note to play
        noteToPlay = nextFourNotes.GetNextNote();

        // Tell the user to play it
        trainingCanvasUIManager.SetNoteToPlay(NotesUtilities.NoteToString(noteToPlay));
        trainingCanvasUIManager.SetModelGuess("...");
        trainingCanvasUIManager.SetStatus("Keep steady pose..");

        // Collect data and send it to the server
        Invoke("CollectDataAndSendToServer", poseTime);

    }

    public void StartTraining()
    {
        InvokeRepeating("Train", 0, poseTime + 2);
    }

    public void StopTraining()
    {
        serverGateway.EndTraining((response) =>
        {
            CancelInvoke("Train");
            CancelInvoke("CollectDataAndSendToServer");
            trainingCanvasUIManager.SetModelGuess("...");
            trainingCanvasUIManager.SetNoteToPlay("...");
            trainingCanvasUIManager.SetStatus("...");
        });
    }

    private void CollectDataAndSendToServer()
    {
        // Get the hand data
        HandWrapper leftHandWrapper = handReader.ReadHand(HandSide.Left);

        // Send the hand data to the server
        serverGateway.SendHandData(new RequestWrapper(leftHandWrapper, (int)noteToPlay), (response) =>
        {
            string toShow = "";
            try
            {
                int messageAsInt = int.Parse(JSON.Parse(response)["message"]);
                NotesUtilities.Note notePredicted = (NotesUtilities.Note)messageAsInt;
                toShow = NotesUtilities.NoteToString(notePredicted);
                PlayNote?.Invoke(notePredicted);
                UpdateStatus(notePredicted);
            }
            catch (Exception e)
            {
                CancelInvoke("Train");
                Debug.LogError(e);
                toShow = "Error somewhere :( Check the server logs!";
            }
            finally
            {
                trainingCanvasUIManager.SetModelGuess(toShow);
            }
        });
    }

    private void UpdateStatus(NotesUtilities.Note modelGuess)
    {
        if (modelGuess == noteToPlay)
        {
            trainingCanvasUIManager.SetStatus("Correct!");
        }
        else
        {
            trainingCanvasUIManager.SetStatus("Incorrect, keep going!");
        }
    }

}
