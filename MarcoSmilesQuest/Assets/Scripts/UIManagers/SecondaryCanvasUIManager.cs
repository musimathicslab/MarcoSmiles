using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SecondaryCanvasUIManager : MonoBehaviour
{

    [SerializeField]
    private TMPro.TextMeshProUGUI _countdownText;
    [SerializeField]
    private Canvas _countdownCanvas;
    [SerializeField]
    private Canvas _nextFourNotesCanvas;
    [SerializeField]
    private TMPro.TextMeshProUGUI[] _nextFourNotesTexts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCountdownText(string text)
    {
        _countdownText.text = text;
    }

    public void ShowCountdownCanvas()
    {
        _nextFourNotesCanvas.gameObject.SetActive(false);
        _countdownCanvas.gameObject.SetActive(true);
    }

    public void ShowNextFourNotesCanvas()
    {
        _countdownCanvas.gameObject.SetActive(false);
        _nextFourNotesCanvas.gameObject.SetActive(true);
    }

    public void UpdateNextFourNotes(Note[] nextFourNotes)
    {
        for (int i = 0; i < nextFourNotes.Length; i++)
        {
            _nextFourNotesTexts[i].text = nextFourNotes[i].ToString();
        }
    }
}
