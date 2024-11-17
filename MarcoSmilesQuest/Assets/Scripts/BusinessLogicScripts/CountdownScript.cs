using System;
using UnityEngine;

public class CountdownScript : MonoBehaviour
{

    [SerializeField]
    private SecondaryCanvasUIManager _secondaryCanvasUIManager;
    public static event Action OnCountdownEnded;
    private int _countdown = 3;

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
        Reset();
    }

    private void Tick()
    {
        _secondaryCanvasUIManager.SetCountdownText("Training will start in " + _countdown.ToString() + "...");
        _countdown--;
        if (_countdown < 0)
        {   
            CancelInvoke();
            OnCountdownEnded?.Invoke();
        }
    }

    public void Reset()
    {
        _countdown = 3;
        _secondaryCanvasUIManager.SetCountdownText("Training will start in 3...");
    }

}
