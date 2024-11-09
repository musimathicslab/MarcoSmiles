using System.Collections;
using System.Collections.Generic;
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
        _createNotesListCanvasGameObject.SetActive(false);
        _preTrainingCanvasGameObject.SetActive(false);
        _trainingCanvasGameObject.SetActive(true);
        _secondaryCanvasGameObject.SetActive(true);
    }

    public void ShowPreTrainingCanvas()
    {
        _createNotesListCanvasGameObject.SetActive(false);
        _preTrainingCanvasGameObject.SetActive(true);
        _trainingCanvasGameObject.SetActive(false);
        _secondaryCanvasGameObject.SetActive(false);
    }

    public void ShowCountdownCanvas()
    {
        _secondaryCanvasGameObject.transform.position = _trainingCanvasGameObject.transform.position + _trainingCanvasGameObject.transform.right * 1.5f;
        _secondaryCanvasGameObject.GetComponent<SecondaryCanvasUIManager>().ShowCountdownCanvas();
    }

    public void ShowNextFourNotesCanvas()
    {
        _secondaryCanvasGameObject.transform.position = _trainingCanvasGameObject.transform.position + _trainingCanvasGameObject.transform.right * 1.5f;
        _secondaryCanvasGameObject.GetComponent<SecondaryCanvasUIManager>().ShowNextFourNotesCanvas();
    }

    public void UpdateNextFourNotes(Note[] nextFourNotes)
    {
        _secondaryCanvasGameObject.GetComponent<SecondaryCanvasUIManager>().UpdateNextFourNotes(nextFourNotes);
    }

}
