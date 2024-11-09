using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class NotesList
{
    public static Note[] Notes;

    public static void CreateEmptyNotesList(int length)
    {
        Notes = new Note[length];
    }

    public static void CreateNotesList(Note startNote, int length)
    {
        Notes = new Note[length];
        Notes[0] = startNote;
        for (int i = 1; i < length; i++)
        {
            Notes[i] = Notes[i - 1].ComputeNextNote();
        }
    }

    public static Note GetFirstNote()
    {
        return Notes[0];
    }

    public static Note GetRandomNote()
    {
        System.Random random = new System.Random();
        return Notes[random.Next(Notes.Length)];
    }

    public static string ToJson()
    {
        return JsonConvert.SerializeObject(new List<Note>(Notes));
    }

    public static Note[] FromJson(string json)
    {
        return JsonConvert.DeserializeObject<List<Note>>(json).ToArray();
    }

}