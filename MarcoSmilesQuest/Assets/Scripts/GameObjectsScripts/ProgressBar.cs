using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    [SerializeField]
    private Image _progressBar;
    private float _widthGoal;
    private float _poseTime = 4f;
    private float _elapsedTime = 0f;
    private bool _isRunning = false;

    void Start()
    {
        // JIC
        ResetProgressBar();
    }

    void Update()
    {
        if (_isRunning)
        {
            _elapsedTime += Time.deltaTime;

            // Calculate progress percentage (0 to 1)
            float progress = Mathf.Clamp01(_elapsedTime / _poseTime);

            // Update progress bar fill
            UpdateProgressBar(progress);

            // Stop the progress bar at the end
            if (_elapsedTime >= _poseTime)
            {
                _isRunning = false; // Stop once the progress reaches the end
            }
        }
    }

    public void StartProgressBar(float poseTime)
    {
        // Get the parent's width
        _widthGoal = gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.width - 110f;

        // Lesgo
        ResetProgressBar();
        _elapsedTime = 0f;
        _poseTime = poseTime;
        _isRunning = true;
    
    }

    public void StopProgressBar()
    {
        _isRunning = false;
        ResetProgressBar();
    }

    private void ResetProgressBar()
    {
        if (_progressBar != null)
        {
            RectTransform rt = _progressBar.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(0, rt.sizeDelta.y);
        }
    }

    public void UpdatePoseTime(float poseTime)
    {
        _poseTime = poseTime;
    }

    private void UpdateProgressBar(float progress)
    {
        if (_progressBar == null)
            return;

        RectTransform rt = _progressBar.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(_widthGoal * progress, rt.sizeDelta.y);
    }

}
