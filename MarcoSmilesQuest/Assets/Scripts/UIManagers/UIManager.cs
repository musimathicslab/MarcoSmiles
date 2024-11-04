using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject preTrainingCanvasGameObject;
    [SerializeField]
    private GameObject trainingCanvasGameObject;
    [SerializeField]
    private GameObject secondaryCanvasGameObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTrainingCanvas()
    {
        trainingCanvasGameObject.SetActive(true);
        secondaryCanvasGameObject.SetActive(true);
        preTrainingCanvasGameObject.SetActive(false);
    }

    public void ShowPreTrainingCanvas()
    {
        preTrainingCanvasGameObject.SetActive(true);
        trainingCanvasGameObject.SetActive(false);
        secondaryCanvasGameObject.SetActive(false);
    }

    public void ShowCountdownCanvas()
    {
        secondaryCanvasGameObject.GetComponent<SecondaryCanvasUIManager>().ShowCountdownCanvas();
    }

    public void ShowNextFourNotesCanvas()
    {
        secondaryCanvasGameObject.GetComponent<SecondaryCanvasUIManager>().ShowNextFourNotesCanvas();
    }

    public void UpdateNextFourNotes(NextFourNotes nextFourNotes)
    {
        secondaryCanvasGameObject.GetComponent<SecondaryCanvasUIManager>().UpdateNextFourNotes(nextFourNotes);
    }

}
