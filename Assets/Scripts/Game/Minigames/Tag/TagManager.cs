﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TagManager : MonoBehaviour
{
    private UnityEvent  onMinigameCompleted = new UnityEvent();
    public UnityEvent   OnMinigameCompleted => onMinigameCompleted;

    [Header("Kids")]
    [SerializeField] private TagCharacter[] kids;
    [SerializeField] private TagCharacter   startingTagged;

    [Header("Settings")]
    [SerializeField] private float completionTimer = 5.0f;
    [Header("")]
    public float debugTimer;

    private TagCharacter currentTagged;
    private Coroutine   completionCountdown = null;

    void Start()
    {
        // Adds listener for when a kid gets tagged
        foreach (TagCharacter kid in kids)
        {
            kid.tagged.AddListener(OnCurrentTaggedChange);
            kid.DebugUpdateColor();
        }

        currentTagged = startingTagged;
        currentTagged.IsTagged = true;
        currentTagged.DebugUpdateColor();
    }

    // Changes the current tagged kid
    void OnCurrentTaggedChange(TagCharacter newTagged) 
    {
        currentTagged.IsTagged = false;

        currentTagged = newTagged;
        currentTagged.IsTagged = true;

        // If the current tagged is one of the other kids, complete the minigame
        if (currentTagged.CompareTag("Enemy")
            && completionCountdown == null)
        {
            completionCountdown = StartCoroutine(BeginCompletionCountdown());
        }
        else if (currentTagged.CompareTag("Player")
            && completionCountdown != null)
        {
            StopCoroutine(completionCountdown);
            completionCountdown = null;
        }
    }

    void OnComplete()
    {
        onMinigameCompleted.Invoke();
        //Time.timeScale = 0.0f;
    }

    IEnumerator BeginCompletionCountdown()
    {
        float timer = completionTimer;
        debugTimer = timer;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            debugTimer = timer;
            yield return null;
        }
        yield return null;
        OnComplete();
    }
}
