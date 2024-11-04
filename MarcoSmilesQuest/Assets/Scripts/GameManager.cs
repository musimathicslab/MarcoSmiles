using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private TrainingScript trainingScript;
    [SerializeField]
    private CountdownScript countdownScript;
    [SerializeField]
    private GameObject OVRCameraRigIntegration;
    [SerializeField]
    private SynthScript synthScript;

    // Start is called before the first frame update
    void Start()
    {
        OVRCameraRigIntegration.SetActive(true);
        uiManager.ShowPreTrainingCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        StartButtonScript.OnStartButtonClicked += StartCountdown;
        EndButtonScript.OnEndButtonClicked += EndTraining;
        CountdownScript.OnCountdownEnded += StartTraining;
        NextFourNotes.OnNextFourNotesChanged += UpdateNextFourNotes;
        TrainingScript.PlayNote += PlayNote;
    }

    void OnDisable()
    {
        StartButtonScript.OnStartButtonClicked -= StartCountdown;
        EndButtonScript.OnEndButtonClicked -= EndTraining;
        CountdownScript.OnCountdownEnded -= StartTraining;
        NextFourNotes.OnNextFourNotesChanged -= UpdateNextFourNotes;
        TrainingScript.PlayNote -= PlayNote;
    }

    void StartCountdown(StartButtonScript startButtonScript)
    {
        uiManager.ShowTrainingCanvas();
        uiManager.ShowCountdownCanvas();
        countdownScript.StartCountdown();
    }

    void StartTraining(CountdownScript countdownScript)
    {
        countdownScript.Reset();
        uiManager.ShowNextFourNotesCanvas();
        trainingScript.StartTraining();
    }

    void EndTraining(EndButtonScript endButtonScript)
    {
        countdownScript.StopCountdown();
        trainingScript.StopTraining();
        uiManager.ShowPreTrainingCanvas();
    }

    void UpdateNextFourNotes(NextFourNotes nextFourNotes)
    {
        uiManager.UpdateNextFourNotes(nextFourNotes);
    }

    void PlayNote(NotesUtilities.Note note)
    {
        synthScript.PlayNote(note);
    }

}