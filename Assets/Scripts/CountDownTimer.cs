using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] float timeRemaining = 60f;
    [SerializeField] bool timerIsRunning = false;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] BoardEventHandler boardEventHandler;


    void Start()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                UpdateTimerDisplay(timeRemaining);

                boardEventHandler.RaiseOnTimerEndAction();
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        timeToDisplay = Mathf.Clamp(timeToDisplay, 0, Mathf.Infinity);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = seconds.ToString("00");
    }

    public void ResetTimer(float newTime)
    {
        timeRemaining = newTime;
        timerIsRunning = true;
    }
}