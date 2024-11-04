using System;

public class NotesUtilities
{

    public enum Note
    {
        DO,
        DO_SHARP,
        RE,
        RE_SHARP,
        MI,
        FA,
        FA_SHARP,
        SOL,
        SOL_SHARP,
        LA,
        LA_SHARP,
        SI
    }

    public static Note GetRandomNote()
    {
        Array values = Enum.GetValues(typeof(Note));
        Random random = new Random();
        Note randomNote = (Note)values.GetValue(random.Next(values.Length));
        return randomNote;
    }

    public static string NoteToString(Note note)
    {
        switch (note)
        {
            case Note.DO:
                return "DO";
            case Note.DO_SHARP:
                return "DO#";
            case Note.RE:
                return "RE";
            case Note.RE_SHARP:
                return "RE#";
            case Note.MI:
                return "MI";
            case Note.FA:
                return "FA";
            case Note.FA_SHARP:
                return "FA#";
            case Note.SOL:
                return "SOL";
            case Note.SOL_SHARP:
                return "SOL#";
            case Note.LA:
                return "LA";
            case Note.LA_SHARP:
                return "LA#";
            case Note.SI:
                return "SI";
            default:
                return "Error somewhere :( Check the logs!";
        }
    }

    public static string EuropeanToInternational(string note)
    {
        switch (note)
        {
            case "DO":
                return "C";
            case "DO#":
                return "C#";
            case "RE":
                return "D";
            case "RE#":
                return "D#";
            case "MI":
                return "E";
            case "FA":
                return "F";
            case "FA#":
                return "F#";
            case "SOL":
                return "G";
            case "SOL#":
                return "G#";
            case "LA":
                return "A";
            case "LA#":
                return "A#";
            case "SI":
                return "B";
            default:
                return "Error somewhere :( Check the logs!";
        }
    }

}
