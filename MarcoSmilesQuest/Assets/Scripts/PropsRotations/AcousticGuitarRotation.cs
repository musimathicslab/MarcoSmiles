using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcousticGuitarRotation : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // Rotate to be up and vertical
        // this.gameObject.transform.rotation = Quaternion.Euler(-90, 180, 180);
        
        // Rotate to face the camera
        GameObject target = GameObject.FindGameObjectWithTag("MainCamera");
        this.gameObject.transform.LookAt(target.transform);
        this.gameObject.transform.rotation = Quaternion.Euler(-90, this.gameObject.transform.rotation.eulerAngles.y, this.gameObject.transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
