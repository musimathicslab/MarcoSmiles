using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private TrainingScript _trainingScript;
    [SerializeField]
    private CountdownScript _countdownScript;
    [SerializeField]
    private GameObject _ovrCameraRigIntegration;
    [SerializeField]
    private SynthScript _synthScript;
    [SerializeField]
    private ServerGateway _serverGateway;

    // Start is called before the first frame update
    void Start()
    {
        _ovrCameraRigIntegration.SetActive(true);
        LoadNotesList();
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
        SaveListButtonScript.OnCreateList += SaveNotesList;
        HomeButtonScript.OnBackHome += ShowHome;
        NewNotesListButton.OnNewNoteListButtonClicked += _uiManager.ShowCreateNotesListCanvas;
    }

    void OnDisable()
    {
        StartButtonScript.OnStartButtonClicked -= StartCountdown;
        EndButtonScript.OnEndButtonClicked -= EndTraining;
        CountdownScript.OnCountdownEnded -= StartTraining;
        NextFourNotes.OnNextFourNotesChanged -= UpdateNextFourNotes;
        TrainingScript.PlayNote -= PlayNote;
        SaveListButtonScript.OnCreateList -= SaveNotesList;
        HomeButtonScript.OnBackHome -= ShowHome;
        NewNotesListButton.OnNewNoteListButtonClicked -= _uiManager.ShowCreateNotesListCanvas;
    }

    void LoadNotesList()
    {
        // Load Note[] from player prefs
        string json = PlayerPrefs.GetString("noteslist");
        if (json == null || json.Trim().Length == 0)
        {
            // If the notes list is empty, we have to create it -> Show new screen
            _uiManager.ShowCreateNotesListCanvas();
        }
        else
        {
            // If the notes list is not empty, load it
            NotesList.Notes = NotesList.FromJson(json);
            _uiManager.ShowPreTrainingCanvas();
        }
    }

    void SaveNotesList(string startNote, int length)
    {
        // Tell the server to create a new model
        _serverGateway.CreateNewModel(length, (response) =>
        {
            // Set notesList in NotesList
            NotesList.CreateNotesList(new Note(startNote), length);
            // Save Note[] to player prefs
            PlayerPrefs.SetString("noteslist", NotesList.ToJson());
            _uiManager.ShowPreTrainingCanvas();
        });
    }

    void StartCountdown(StartButtonScript startButtonScript)
    {
        if (NotesList.Notes != null && NotesList.Notes.Length > 0)
        {
            _uiManager.ShowTrainingCanvas();
            _uiManager.ShowCountdownCanvas();
            _countdownScript.StartCountdown();
        }
        else
        {
            _uiManager.ShowCreateNotesListCanvas();
        }
    }

    void StartTraining(CountdownScript countdownScript)
    {
        countdownScript.Reset();
        _uiManager.ShowNextFourNotesCanvas();
        _uiManager.HideHandInteractors();
        _trainingScript.StartTraining();
    }

    void EndTraining(EndButtonScript endButtonScript)
    {
        _countdownScript.StopCountdown();
        _trainingScript.StopTraining();
        _uiManager.ShowPreTrainingCanvas();
        _uiManager.ShowHandInteractors();
    }

    void UpdateNextFourNotes()
    {
        _uiManager.UpdateNextFourNotes(NextFourNotes.Notes);
    }

    void PlayNote(Note note)
    {
        _synthScript.PlayNote(note);
    }

    void ShowHome()
    {
        _uiManager.ShowPreTrainingCanvas();
    }

}