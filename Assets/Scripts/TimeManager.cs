using TMPro;
using UnityEngine;
using System.Collections;


public class TimeManager : MonoBehaviour
{
    [SerializeField] private float startTimer;
    private float currentTime;

    [SerializeField] private TMP_Text timerTxt;

    private bool isPaused = false;
    private bool isPausedOnce = false;


    void Start()
    {
        currentTime = startTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused || currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            if (currentTime > 60)
            {
                FormatTimer();
            }
            else
            {
                timerTxt.text = currentTime.ToString("0:00");
            }
        }
        if (currentTime <= 0)
        {
            currentTime = 0;
            timerTxt.text = "00:00";
        }
    }

    void FormatTimer()
    {
        float mins = Mathf.FloorToInt(currentTime / 60);
        float secs = Mathf.FloorToInt(currentTime % 60);

        timerTxt.text = string.Format("{0:00}:{1:00}", mins, secs);
    }

    public void OnPauseBtnPressed(float duration)
    {
        if (!isPausedOnce)
        {
            StartCoroutine(PauseTimerRoutine(duration));
            isPausedOnce = true;
        }
    }

    private IEnumerator PauseTimerRoutine(float secs)
    {
        isPaused = true;
        float elapsed = 0f;
        Time.timeScale = 0f;

        while (elapsed < secs)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1f;
        isPaused = false;
    }
}
