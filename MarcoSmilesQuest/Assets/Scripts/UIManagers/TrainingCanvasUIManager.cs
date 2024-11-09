using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingCanvasUIManager : MonoBehaviour
{
    
    [SerializeField]
    private TMPro.TextMeshProUGUI _noteToPlay;
    [SerializeField]
    private TMPro.TextMeshProUGUI _modelGuess;
    [SerializeField]
    private TMPro.TextMeshProUGUI _status;

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
        _noteToPlay.text = note;
    }

    public void SetModelGuess(string infoText)
    {
        _modelGuess.text = infoText;
    }

    public void SetStatus(string statusText)
    {
        _status.text = statusText;
    }

}
