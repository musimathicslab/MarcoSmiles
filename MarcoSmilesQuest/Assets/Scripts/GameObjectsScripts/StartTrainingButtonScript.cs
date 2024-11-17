using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrainingButtonScript : MonoBehaviour
{

    public static event Action OnStartButtonClicked;

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
        OnStartButtonClicked?.Invoke();
    }

}
