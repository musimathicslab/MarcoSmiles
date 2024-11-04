using System;
using static NotesUtilities;

public class NextFourNotes
{
    // List of Note of four elements
    public Note[] notes = new Note[4];
    public static event Action<NextFourNotes> OnNextFourNotesChanged;

    public NextFourNotes()
    {
        for (int i = 0; i < 4; i++)
        {
            notes[i] = GetRandomNote();
        }
    }

    public Note GetNextNote()
    {
        Note nextNote = notes[0];
        for (int i = 0; i < 3; i++)
        {
            notes[i] = notes[i + 1];
        }
        notes[3] = GetRandomNote();
        OnNextFourNotesChanged?.Invoke(this);
        return nextNote;
    }

}