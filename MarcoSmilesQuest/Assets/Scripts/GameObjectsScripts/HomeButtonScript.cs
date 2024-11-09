using System;
using UnityEngine;

public class HomeButtonScript : MonoBehaviour
{

    public static event Action OnBackHome;

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
        OnBackHome?.Invoke();
    }

}
