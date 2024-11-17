using System;
using UnityEngine;

public class StopPlayingButtonScript : MonoBehaviour
{

    public static event Action OnEndButtonClicked;

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
        OnEndButtonClicked?.Invoke();
    }

}
