using System;
using System.Linq;
using static NotesList;

public class NextFourNotes
{
    // List of Note of four elements
    public static Note[] Notes;
    public static event Action OnNextFourNotesChanged;

    public static void GenerateNextFourNotes()
    {
        Notes = (from _ in Enumerable.Range(0, 4)
                select GetRandomNote()).ToArray(); // C# List comprehension!
    }

    public static Note GetNextNote()
    {
        Note nextNote = Notes[0];
        for (int i = 0; i < 3; i++)
        {
            Notes[i] = Notes[i + 1];
        }
        Notes[3] = GetRandomNote();
        OnNextFourNotesChanged?.Invoke();
        return nextNote;
    }

}