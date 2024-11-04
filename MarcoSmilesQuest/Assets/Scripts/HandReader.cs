using UnityEngine;
using Oculus.Interaction.Input;
using System;
using HandUtilitites;
using ServerCommunicationUtilities;

public class HandReader : MonoBehaviour
{
    [SerializeField]
    private Hand leftHand;

    [SerializeField]
    private Hand rightHand;

    [SerializeField]
    private ServerGateway serverGateway;

    void Start()
    {
        // // TODO: change '5' with the actual note
        // serverGateway.SendHandData(new RequestWrapper(ReadHand(leftHand), 5), (response) =>
        // {
        //     Debug.Log(response);
        //     serverGateway.EndTraining((response) =>
        //     {
        //         Debug.Log(response);
        //     });
        // });
    }

    void Update()
    {
    }

    public HandWrapper ReadHand(HandSide side)
    {
        Hand hand = side == HandSide.Left ? leftHand : rightHand;
        HandWrapper handWrapper = new HandWrapper();
        hand.GetJointPosesLocal(out ReadOnlyHandJointPoses jointPosesFromWrist);
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
                Debug.Log(e);
                jointPose = new Pose(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
            }
            handWrapper[Enum.GetName(typeof(UsefulJointIds), jointId)] = new HandCoordinates { position = jointPose.position, rotation = jointPose.rotation };
        }
        return handWrapper;
    }

}