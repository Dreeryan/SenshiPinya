﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugIncrementMotivation : MonoBehaviour
{
    [SerializeField] private int incrementValue = 1;
    [SerializeField] private KeyCode increaseKey = KeyCode.PageUp;
    [SerializeField] private KeyCode decreaseKey = KeyCode.PageDown;

    private void Update()
    {
        if (Input.GetKeyDown(increaseKey)) IncrementMotivation(incrementValue);
        else if (Input.GetKeyDown(decreaseKey)) IncrementMotivation(-incrementValue);
    }

    void IncrementMotivation(int value)
    {
        if (MotivationManager.Instance == null) return;

        MotivationManager.Instance.IncrementMotivation(value);
    }
}