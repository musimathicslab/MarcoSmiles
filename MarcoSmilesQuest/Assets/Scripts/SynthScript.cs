using System.Collections;
using System.Collections.Generic;
using HGS.Tone;
using UnityEngine;

public class SynthScript : MonoBehaviour
{

    [SerializeField]
    private ToneSynth synth;

    // Start is called before the first frame update
    void Start()
    {
        synth.SetInstrument(MidiInstrumentCode.Acoustic_Grand_Piano);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayNote(NotesUtilities.Note toPlay)
    {
        synth.TriggerAttackAndRelease(ToneNote.Parse(
            AddOctave(
                NotesUtilities.EuropeanToInternational(
                    NotesUtilities.NoteToString(toPlay)
                )
            )
        ), duration: 1f);
    }

    private static string AddOctave(string note, int octave = 3)
    {
        return note + octave.ToString();
    }

    private static string AddOctave(NotesUtilities.Note note, int octave = 3)
    {
        return NotesUtilities.NoteToString(note) + octave.ToString();
    }

}
