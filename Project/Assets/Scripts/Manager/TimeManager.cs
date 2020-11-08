using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    private TimeSpan timePlaying;
    private bool timerGoing;
    private float elapsedTime;

    public string curTimeStr;

    private void Awake()
    {
        Instance = this;

        if (Instance == this) Debug.Log("Time Manager Singleton Initialized");
    }

    private void Start()
    {
        timerGoing = false;
    }

    public void StartTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void StopTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("m':'ss'.'ff");

            curTimeStr = timePlayingStr;
            yield return null;
        }
    }
}
