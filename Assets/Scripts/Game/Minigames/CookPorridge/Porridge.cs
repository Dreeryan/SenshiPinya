﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class Porridge : MonoBehaviour
{
    private float currentTemp = 0.0f;
    private bool isRightTemp = false;
    private bool hasWon = false;

    [Header("Porridge Settings")]
    [SerializeField] private float maxTemp = 100.0f;
    [SerializeField] private float rightTemp = 30f;
    [SerializeField] private float addTemperature;
    [SerializeField] private float decreaseTemperature;
    [SerializeField] private float secondsToUndercooked;
    [SerializeField] private float secondsToCooked;
    [SerializeField] private float secondsToWin;

    [Header("Animator")]
    public Animator fireAnimator;
    public Animator potCoverAnimator;

    [Header("UI")]
    [SerializeField] private Slider porridgeSlider;

    [Header("References")]
    [SerializeField] private Counter counter;


    [Header("Sound Settings")]
    [SerializeField] private UnityEvent onFireTurnOn;
    [SerializeField] private UnityEvent onUndercooked;
    [SerializeField] private UnityEvent onSlightlyCooked;
    [SerializeField] private UnityEvent onCooked;

    void Update()
    {
        UpdateTemp();
        //print(currentTemp);

        // If its at the right temp
        if (isRightTemp && !hasWon)
        {
            StartCoroutine("RightTempCountdown");
        }
        else
        {
            StopCoroutine("RightTempCountdown");
        }
    }

    public void UpdateTemp()
    {
        // Getting the current temp from the slider value
        currentTemp = (int)(porridgeSlider.value * 100);

        if (Input.GetMouseButton(0) && Time.timeScale > 0 && !hasWon && !EventSystem.current.IsPointerOverGameObject())
        {
            porridgeSlider.value += addTemperature * Time.deltaTime;

            if (currentTemp >= maxTemp) currentTemp = maxTemp;
        }
        else
        {
            if (!hasWon) porridgeSlider.value -= decreaseTemperature * Time.deltaTime;

            if (currentTemp <= 0) currentTemp = 0;
        }

        fireAnimator.SetFloat("Temp", currentTemp);
        potCoverAnimator.SetFloat("Temp", currentTemp);

        if (currentTemp > 0)
        {
            fireAnimator.SetBool("isFireOn", true);
            potCoverAnimator.SetBool("isFireOn", true);
            onFireTurnOn?.Invoke();
        }
        else
        {
            fireAnimator.SetBool("isFireOn", false);
            potCoverAnimator.SetBool("isFireOn", false);
        }

        if (currentTemp > rightTemp)
        {
            isRightTemp = true;
        }
        else
        {
            isRightTemp = false;
        }
    }

    IEnumerator RightTempCountdown()
    {        
        yield return new WaitForSeconds(secondsToUndercooked);

        yield return new WaitForSeconds(secondsToCooked);

        potCoverAnimator.SetBool("isCooked", true);
        onCooked?.Invoke();
        yield return new WaitForSeconds(secondsToWin);
        hasWon = true;

        counter.IncreaseProgress();
    }
}
