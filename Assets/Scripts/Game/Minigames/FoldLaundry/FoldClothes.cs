﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FoldClothes : MonoBehaviour
{
    public UnityEvent OnCompletelyFold = new UnityEvent { };

    [SerializeField] private ClothesDatabase clothesDB;

    [SerializeField] private string clothId;
    [SerializeField] private int currentNumberOfTimesFolded = 0;
    [SerializeField] private SpriteRenderer sRenderer;
    [SerializeField]private bool canBeFolded = false;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private Directions currentDirection;

    public Directions getCurrentDirection() => currentDirection;
    private void Update()
    {
        SetDirection();
    }

    private Directions SwipeDirection()
    {
        float horizontalSwipe = Mathf.Abs(startPosition.x - endPosition.x);
        float verticalSwipe = Mathf.Abs(startPosition.y - endPosition.y);

        if (horizontalSwipe > 0 || verticalSwipe > 0)
        {
            if (horizontalSwipe > verticalSwipe)
            {
                if (startPosition.x > endPosition.x) return Directions.Left;
                else return Directions.Right;
            }
            else if (verticalSwipe > horizontalSwipe)
            {
                if (startPosition.y > endPosition.y) return Directions.Down;
                else return Directions.Up;
            }
        }
        return Directions.None;
    }

    private void SetDirection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            endPosition = startPosition;
        }

        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            currentDirection = SwipeDirection();
            CheckForFoldSequence();
        }
    }

    private void CheckForFoldSequence()
    {
        if (!canBeFolded) return;
        if (currentNumberOfTimesFolded >= clothesDB.GetData(this.clothId).
            clothesFoldingDirection.Length) return;

        if (currentDirection == clothesDB.GetData(this.clothId).
            clothesFoldingDirection[currentNumberOfTimesFolded])
        {
            sRenderer.sprite = clothesDB.GetData(this.clothId).foldedClothesSprite[currentNumberOfTimesFolded];
            currentNumberOfTimesFolded += 1;
        }

        if (currentNumberOfTimesFolded >= clothesDB.GetData(this.clothId).
            clothesFoldingDirection.Length)
        {
            Debug.Log("Done folding");
            StartCoroutine(OnCompletelyFolded());
            return;
        }
    }

    IEnumerator OnCompletelyFolded()
    {
        yield return new WaitForSeconds(1f);
        DisableCLothing();
        OnCompletelyFold.Invoke();
    }

    public void DisableCLothing()
    {
        canBeFolded = false;
        if (sRenderer != null) sRenderer.enabled = false;
    }

    public void EnableClothing()
    {
        canBeFolded = true;
        if (sRenderer != null) sRenderer.enabled = true;
        //arrowSprite.gameObject.SetActive(true);

    }

}
