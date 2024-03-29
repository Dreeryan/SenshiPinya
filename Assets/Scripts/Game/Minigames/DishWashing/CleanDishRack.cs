﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CleanDishRack : MonoBehaviour
{
    public class OnDishAdded : UnityEvent<int> { }
    private OnDishAdded dishAdded = new OnDishAdded();
    public OnDishAdded  DishAdded => dishAdded;

    [Header("References")]
    [SerializeField] private Dish[]     dirtyDishes;
    [SerializeField] private Sprite[]   cleanSprites;

    private int             dishCount = 0;
    private SpriteRenderer  sRenderer;

    private void Start()
    {
        if (sRenderer == null) sRenderer = GetComponent<SpriteRenderer>();
        foreach (Dish dish in dirtyDishes)
        {
            dish.OnDishCleaned.AddListener(DishCleaned);
        }
    }

    private void ChangeSprite()
    {
        if (sRenderer == null ||
            cleanSprites[dishCount - 1] == null) return;

        GetComponent<SpriteRenderer>().sprite = cleanSprites[dishCount - 1];
    }

    private void DishCleaned()
    {
        Debug.Log("DishCleaned"); 
        dishCount++;
        ChangeSprite();
        WinCheck.Instance.IncreaseProgress();
        dishAdded.Invoke(dishCount);
    }
}
