﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyDishRack : MonoBehaviour
{
    [SerializeField] private Sprite[] dirtySprites;
    [SerializeField] private CleanDishRack rack;
    [SerializeField] private GameObject[] dirtyDishes;

    // Update is called once per frame
	
	//A: Dont do this per update. Do this everytime the dishcounter was changed
		//Can shorten to spriteRenderer.sprite = dirtySprites[dishCounter]
		//Null check before accessing SpriteRenderer and dirtySprites element
    void Update()
    {
        if (rack.dishCounter == 1)
        {
            GetComponent<SpriteRenderer>().sprite = dirtySprites[0];
            dirtyDishes[0].gameObject.SetActive(true);
        }
        if (rack.dishCounter == 2)
        {
            GetComponent<SpriteRenderer>().sprite = dirtySprites[1];
            dirtyDishes[1].gameObject.SetActive(true);
        }
    }
}
