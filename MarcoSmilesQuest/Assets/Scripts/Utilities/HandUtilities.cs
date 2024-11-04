using System;
using System.Reflection;
using Oculus.Interaction.Input;
using UnityEngine;

namespace HandUtilitites
{

    public enum HandSide
    {
        Left,
        Right
    }

    public enum UsefulJointIds
    {
        HandThumb0 = HandJointId.HandThumb0,
        HandThumb1 = HandJointId.HandThumb1,
        HandThumb2 = HandJointId.HandThumb2,
        HandThumb3 = HandJointId.HandThumb3,
        HandIndex1 = HandJointId.HandIndex1,
        HandIndex2 = HandJointId.HandIndex2,
        HandIndex3 = HandJointId.HandIndex3,
        HandMiddle1 = HandJointId.HandMiddle1,
        HandMiddle2 = HandJointId.HandMiddle2,
        HandMiddle3 = HandJointId.HandMiddle3,
        HandRing1 = HandJointId.HandRing1,
        HandRing2 = HandJointId.HandRing2,
        HandRing3 = HandJointId.HandRing3,
        HandPinky0 = HandJointId.HandPinky0,
        HandPinky1 = HandJointId.HandPinky1,
        HandPinky2 = HandJointId.HandPinky2,
        HandPinky3 = HandJointId.HandPinky3,
        HandThumbTip = HandJointId.HandThumbTip,
        HandIndexTip = HandJointId.HandIndexTip,
        HandMiddleTip = HandJointId.HandMiddleTip,
        HandRingTip = HandJointId.HandRingTip,
        HandPinkyTip = HandJointId.HandPinkyTip
    };

    [Serializable]
    public class HandCoordinates
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    [Serializable]
    public class HandWrapper
    {

        public HandCoordinates HandThumb0;
        public HandCoordinates HandThumb1;
        public HandCoordinates HandThumb2;
        public HandCoordinates HandThumb3;
        public HandCoordinates HandIndex1;
        public HandCoordinates HandIndex2;
        public HandCoordinates HandIndex3;
        public HandCoordinates HandMiddle1;
        public HandCoordinates HandMiddle2;
        public HandCoordinates HandMiddle3;
        public HandCoordinates HandRing1;
        public HandCoordinates HandRing2;
        public HandCoordinates HandRing3;
        public HandCoordinates HandPinky0;
        public HandCoordinates HandPinky1;
        public HandCoordinates HandPinky2;
        public HandCoordinates HandPinky3;
        public HandCoordinates HandThumbTip;
        public HandCoordinates HandIndexTip;
        public HandCoordinates HandMiddleTip;
        public HandCoordinates HandRingTip;
        public HandCoordinates HandPinkyTip;

        public object this[string propertyName]
        {
            get
            {
                Type myType = typeof(HandWrapper);
                FieldInfo myPropInfo = myType.GetField(propertyName);
                return myPropInfo.GetValue(this);
            }
            set
            {
                Type myType = typeof(HandWrapper);
                FieldInfo myPropInfo = myType.GetField(propertyName);
                myPropInfo.SetValue(this, value);
            }
        }

    }
    
    [Serializable]
    public class HandsWrapper
    {
        HandWrapper leftHand;
        HandWrapper rightHand;
    }

}