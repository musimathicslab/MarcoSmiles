using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _createNotesListCanvasGameObject;
    [SerializeField]
    private GameObject _preTrainingCanvasGameObject;
    [SerializeField]
    private GameObject _trainingCanvasGameObject;
    [SerializeField]
    private GameObject _secondaryCanvasGameObject;
    [SerializeField]
    private GameObject _playCanvasGameObject;
    [SerializeField]
    private GameObject _leftHandInteractorsGameObject;
    [SerializeField]
    private GameObject _rightHandInteractorsGameObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHandInteractors()
    {
        _leftHandInteractorsGameObject.SetActive(true);
        _rightHandInteractorsGameObject.SetActive(true);
    }

    public void HideHandInteractors()
    {
        _leftHandInteractorsGameObject.SetActive(false);
        _rightHandInteractorsGameObject.SetActive(false);
    }

    public void ShowCreateNotesListCanvas()
    {
        _createNotesListCanvasGameObject.SetActive(true);
        _preTrainingCanvasGameObject.SetActive(false);
        _trainingCanvasGameObject.SetActive(false);
        _secondaryCanvasGameObject.SetActive(false);
    }

    public void ShowTrainingCanvas()
    {
        _playCanvasGameObject.SetActive(false);
        _createNotesListCanvasGameObject.SetActive(false);
        _preTrainingCanvasGameObject.SetActive(false);
        _trainingCanvasGameObject.SetActive(true);
        _secondaryCanvasGameObject.SetActive(true);
    }

    public void ShowPlayCanvas()
    {
        _playCanvasGameObject.SetActive(true);
        _createNotesListCanvasGameObject.SetActive(false);
        _preTrainingCanvasGameObject.SetActive(false);
        _trainingCanvasGameObject.SetActive(false);
        _secondaryCanvasGameObject.SetActive(false);
    }

    public void ShowPreTrainingCanvas()
    {
        _playCanvasGameObject.SetActive(false);
        _createNotesListCanvasGameObject.SetActive(false);
        _preTrainingCanvasGameObject.SetActive(true);
        _trainingCanvasGameObject.SetActive(false);
        _secondaryCanvasGameObject.SetActive(false);
    }

    public void ShowCountdownCanvas()
    {
        _secondaryCanvasGameObject.transform.position = _trainingCanvasGameObject.transform.parent.transform.position + _trainingCanvasGameObject.transform.parent.transform.right * 1.5f;
        _secondaryCanvasGameObject.transform.rotation = _trainingCanvasGameObject.transform.parent.transform.rotation * Quaternion.Euler(0, 20, 0);
        _secondaryCanvasGameObject.GetComponent<SecondaryCanvasUIManager>().ShowCountdownCanvas();
    }

    public void ShowNextFourNotesCanvas()
    {
        _secondaryCanvasGameObject.transform.position = _trainingCanvasGameObject.transform.parent.transform.position + _trainingCanvasGameObject.transform.parent.transform.right * 1.5f;
        _secondaryCanvasGameObject.transform.rotation = _trainingCanvasGameObject.transform.parent.transform.rotation * Quaternion.Euler(0, 20, 0);
        _secondaryCanvasGameObject.GetComponent<SecondaryCanvasUIManager>().ShowNextFourNotesCanvas();
    }

    public void UpdateNextFourNotes(Note[] nextFourNotes)
    {
        _secondaryCanvasGameObject.GetComponent<SecondaryCanvasUIManager>().UpdateNextFourNotes(nextFourNotes);
    }

    public void UpdateAccuracy(string accuracy)
    {
        _secondaryCanvasGameObject.GetComponent<SecondaryCanvasUIManager>().SetAccuracyText(accuracy);
    }

    public void UpdatePoseTime(bool playMode, float poseTime)
    {
        ProgressBar _progressBar;
        if (playMode)
        {
            _progressBar = _playCanvasGameObject.GetComponent<PlayCanvasUIManager>().ProgressBar;
        }
        else
        {
            _progressBar = _trainingCanvasGameObject.GetComponent<TrainingCanvasUIManager>().ProgressBar;
        }
        _progressBar.UpdatePoseTime(poseTime);
    }

    public void StartProgressBar(bool playMode, float poseTime)
    {
        ProgressBar _progressBar;
        if (playMode)
        {
            _progressBar = _playCanvasGameObject.GetComponent<PlayCanvasUIManager>().ProgressBar;
        }
        else
        {
            _progressBar = _trainingCanvasGameObject.GetComponent<TrainingCanvasUIManager>().ProgressBar;
        }
        _progressBar.StartProgressBar(poseTime);
    }

    public void StopProgressBar(bool playMode)
    {
        ProgressBar _progressBar;
        if (playMode)
        {
            _progressBar = _playCanvasGameObject.GetComponent<PlayCanvasUIManager>().ProgressBar;
        }
        else
        {
            _progressBar = _trainingCanvasGameObject.GetComponent<TrainingCanvasUIManager>().ProgressBar;
        }
        _progressBar.StopProgressBar();
    }



}
