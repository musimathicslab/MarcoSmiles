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
    private PlayScript _playScript;
    [SerializeField]
    private SynthScript _synthScript;
    [SerializeField]
    private ServerGateway _serverGateway;
    [SerializeField]
    private GameObject _ovrCameraRigIntegration;

    private bool _playMode = false;

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
        StartTrainingButtonScript.OnStartButtonClicked += StartCountdown;
        EndTrainingButtonScript.OnEndButtonClicked += EndTraining;
        StartPlayingButtonScript.OnStartPlayingButtonClicked += StartPlaying;
        StopPlayingButtonScript.OnEndButtonClicked += StopPlaying;
        NewNotesListButtonScript.OnNewNoteListButtonClicked += _uiManager.ShowCreateNotesListCanvas;
        HomeButtonScript.OnBackHome += ShowHome;
        SaveListButtonScript.OnCreateList += SaveNotesList;
        CountdownScript.OnCountdownEnded += StartTraining;
        NextFourNotes.OnNextFourNotesChanged += UpdateNextFourNotes;
        TrainingScript.PlayNote += PlayNote;
        TrainingScript.UpdateAccuracy += UpdateAccuracy;
        TrainingScript.StartProgressBar += StartProgressBar;
        PlayScript.PlayNote += PlayNote;
        PlayScript.StartProgressBar += StartProgressBar;
    }

    void OnDisable()
    {
        StartTrainingButtonScript.OnStartButtonClicked -= StartCountdown;
        EndTrainingButtonScript.OnEndButtonClicked -= EndTraining;
        StartPlayingButtonScript.OnStartPlayingButtonClicked -= StartPlaying;
        StopPlayingButtonScript.OnEndButtonClicked -= StopPlaying;
        NewNotesListButtonScript.OnNewNoteListButtonClicked -= _uiManager.ShowCreateNotesListCanvas;
        HomeButtonScript.OnBackHome -= ShowHome;
        SaveListButtonScript.OnCreateList -= SaveNotesList;
        CountdownScript.OnCountdownEnded -= StartTraining;
        NextFourNotes.OnNextFourNotesChanged -= UpdateNextFourNotes;
        TrainingScript.PlayNote -= PlayNote;
        TrainingScript.UpdateAccuracy -= UpdateAccuracy;
        TrainingScript.StartProgressBar -= StartProgressBar;
        PlayScript.PlayNote -= PlayNote;
        PlayScript.StartProgressBar -= StartProgressBar;
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

    void StartCountdown()
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

    void StartTraining()
    {
        _playMode = false;
        _countdownScript.Reset();
        _uiManager.ShowNextFourNotesCanvas();
        _uiManager.HideHandInteractors();
        _trainingScript.StartTraining();
    }

    void EndTraining()
    {
        _countdownScript.StopCountdown();
        _trainingScript.StopTraining();
        _uiManager.ShowPreTrainingCanvas();
        _uiManager.ShowHandInteractors();
        StopProgressBar();
    }

    void StartPlaying()
    {
        _playMode = true;
        _uiManager.ShowPlayCanvas();
        _uiManager.HideHandInteractors();
        _playScript.StartPlayCountdown();
    }

    void StopPlaying()
    {
        StopProgressBar();
        _playMode = false;
        _playScript.StopPlaying();
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

    void UpdateAccuracy(int accuracy)
    {
        _uiManager.UpdateAccuracy(accuracy.ToString());
    }

    void StartProgressBar(float poseTime)
    {
        _uiManager.StartProgressBar(_playMode, poseTime);
    }

    void StopProgressBar()
    {
        _uiManager.StopProgressBar(_playMode);
    }

}