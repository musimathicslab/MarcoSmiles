using UnityEngine;
using Oculus.Interaction.Input;
using System;

public class HandReader : MonoBehaviour
{
    [SerializeField]
    private Hand _leftHand;

    [SerializeField]
    private Hand _rightHand;

    [SerializeField]
    private ServerGateway _serverGateway;

    void Start()
    {
    }

    void Update()
    {
    }

    public HandWrapper ReadHand(HandSide side)
    {
        Hand hand = side == HandSide.Left ? _leftHand : _rightHand;
        HandWrapper handWrapper = new HandWrapper();
        hand.GetJointPosesFromWrist(out ReadOnlyHandJointPoses jointPosesFromWrist);
        
        // Iterare su ogni joint della mano
        foreach (var jointId in Enum.GetValues(typeof(UsefulJointIds)))
        {
            Pose jointPose;
            try
            {
                jointPose = jointPosesFromWrist[(int)jointId];
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                Debug.LogError("JointId: " + jointId);
                Debug.LogError("JointPosesFromWrist: " + jointPosesFromWrist.Count);
                jointPose = new Pose(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
            }
            handWrapper[Enum.GetName(typeof(UsefulJointIds), jointId)] = new HandCoordinates { PositionX = jointPose.position.x, PositionY = jointPose.position.y, PositionZ = jointPose.position.z };
        }
        return handWrapper;
    }

    public bool IsHandTracking()
    {
        return _leftHand.IsConnected && _leftHand.GetData().IsTracked && _rightHand.IsConnected && _rightHand.GetData().IsTracked;
    }

}