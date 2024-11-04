using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SecondaryCanvasUIManager : MonoBehaviour
{

    [SerializeField]
    private TMPro.TextMeshProUGUI countdownText;
    [SerializeField]
    private Canvas countdownCanvas;
    [SerializeField]
    private Canvas nextFourNotesCanvas;
    [SerializeField]
    private TMPro.TextMeshProUGUI[] nextFourNotesTexts;

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
        countdownText.text = text;
    }

    public void ShowCountdownCanvas()
    {
        nextFourNotesCanvas.gameObject.SetActive(false);
        countdownCanvas.gameObject.SetActive(true);
    }

    public void ShowNextFourNotesCanvas()
    {
        countdownCanvas.gameObject.SetActive(false);
        nextFourNotesCanvas.gameObject.SetActive(true);
    }

    public void UpdateNextFourNotes(NextFourNotes nextFourNotes)
    {
        for (int i = 0; i < nextFourNotes.notes.Length; i++)
        {
            nextFourNotesTexts[i].text = NotesUtilities.NoteToString(nextFourNotes.notes[i]);
        }
    }
}
