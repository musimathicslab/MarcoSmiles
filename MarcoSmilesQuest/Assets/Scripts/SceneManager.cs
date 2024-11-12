using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    [SerializeField]
    private OVRManager cameraRig;
    [SerializeField]
    private MRUK MRUK;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckIfIsInRoom()
    {
        if (!MRUK.GetCurrentRoom().IsPositionInRoom(cameraRig.gameObject.transform.position)){
            OVRScene.RequestSpaceSetup();
        }
    }
}
