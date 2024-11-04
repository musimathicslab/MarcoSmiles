using System;
using HandUtilitites;

namespace ServerCommunicationUtilities
{
    [Serializable]
    public class RequestWrapper
    {
        public HandWrapper handWrapper;
        public int note;

        public RequestWrapper(HandWrapper handWrapper, int note)
        {
            this.handWrapper = handWrapper;
            this.note = note;
        }
    }
}