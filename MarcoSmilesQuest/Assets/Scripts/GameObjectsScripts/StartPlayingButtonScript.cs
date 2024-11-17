using System;
using UnityEngine;

public class StartPlayingButtonScript : MonoBehaviour
{

    public static event Action OnStartPlayingButtonClicked;

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
        OnStartPlayingButtonClicked?.Invoke();
    }
    
}
