using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCanvasUIManager : MonoBehaviour
{

    [SerializeField]
    private TMPro.TextMeshProUGUI _modelGuess;
    [SerializeField]
    private TMPro.TextMeshProUGUI _status;
    [SerializeField]
    public ProgressBar ProgressBar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
