using System;
using UnityEngine;

public class CountdownScript : MonoBehaviour
{

    [SerializeField]
    private SecondaryCanvasUIManager secondaryCanvasUIManager;
    public static event Action<CountdownScript> OnCountdownEnded;
    private int countdown = 3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartCountdown()
    {
        InvokeRepeating("Tick", 0.5f, 1);
    }

    public void StopCountdown()
    {
        CancelInvoke();
    }

    private void Tick()
    {
        secondaryCanvasUIManager.SetCountdownText("Training will start in " + countdown.ToString() + "...");
        countdown--;
        if (countdown < 0)
        {   
            CancelInvoke();
            OnCountdownEnded?.Invoke(this);
        }
    }

    public void Reset()
    {
        countdown = 3;
        secondaryCanvasUIManager.SetCountdownText("Training will start in 3...");
    }

}
