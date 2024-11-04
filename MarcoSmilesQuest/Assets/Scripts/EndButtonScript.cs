using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButtonScript : MonoBehaviour
{

    public static event Action<EndButtonScript> OnEndButtonClicked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        OnEndButtonClicked?.Invoke(this);
    }
    
}
