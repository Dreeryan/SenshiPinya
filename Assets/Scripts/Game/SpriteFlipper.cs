﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Vector2 lastPosition;
    private bool    isFlipped;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        isFlipped= false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlipped)
        {
            if (spriteRenderer != null)
                spriteRenderer.flipX = true;
        }

        else
        {
            if (spriteRenderer != null)
                spriteRenderer.flipX = false;
        }

        // If moving right
        if (transform.position.x >= lastPosition.x)
            isFlipped = true;

        // If moving left  
        if (transform.position.x <= lastPosition.x)
            isFlipped = false;
    }
}