using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingCanvasUIManager : MonoBehaviour
{
    
    [SerializeField]
    private TMPro.TextMeshProUGUI noteToPlay;
    [SerializeField]
    private TMPro.TextMeshProUGUI modelGuess;
    [SerializeField]
    private TMPro.TextMeshProUGUI status;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNoteToPlay(string note)
    {
        noteToPlay.text = note;
    }

    public void SetModelGuess(string infoText)
    {
        modelGuess.text = infoText;
    }

    public void SetStatus(string statusText)
    {
        status.text = statusText;
    }

}
