﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class TimerManager : BaseManager<TimerManager>
{
    public static System.Action<float> OnTimerUpdated;


    private float curTime;

    public float CurTime => curTime;

    private Coroutine timerRoutine;
    public void Awake()
    {
        SceneManager.sceneUnloaded += ResetTimer;
    }

    public void ResetTimer(Scene scene)
    {
        curTime = 0;
    }

    public void StartTimer()
    {
        if (timerRoutine != null) return;
        SceneLoader.Instance.LoadScene("Timer", true);
        timerRoutine = StartCoroutine(TimerRoutine());
    }

    public void StopTimer()
    {
        if (timerRoutine == null) return;

        StopCoroutine(timerRoutine);
        timerRoutine = null;
    }

    IEnumerator TimerRoutine()
    {
        for(; ; )
        {
            if (!GameManager.Instance.IsPaused)
            {
                curTime += Time.timeScale * Time.deltaTime;
                OnTimerUpdated?.Invoke(curTime);
            }
            yield return null;
        }
    }
}
