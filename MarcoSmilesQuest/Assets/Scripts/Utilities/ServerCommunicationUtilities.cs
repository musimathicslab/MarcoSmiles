using System;
using System.Collections.Generic;

[Serializable]
public class RequestWrapper
{
    public List<HandWrapper> HandWrappers;
    public int Note;


    public RequestWrapper()
    {
        HandWrappers = new List<HandWrapper>();
    }

    public RequestWrapper(int note)
    {
        HandWrappers = new List<HandWrapper>();
        Note = note;
    }
}