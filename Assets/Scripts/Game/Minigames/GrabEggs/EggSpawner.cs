﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    [SerializeField] private Counter counter;
    [SerializeField] private List<Transform> SpawnPoints;
    [SerializeField] private GameObject eggPrefab;
    private int spawnPointIndex = 0;
    private void Start()
    {
        SpawnEggs();
    }
    void SpawnEggs()
    {
        while (spawnPointIndex < SpawnPoints.Count)
        {
            GameObject egg = Instantiate(eggPrefab, SpawnPoints[spawnPointIndex].position, Quaternion.identity);
            egg.GetComponent<Egg>().Initialize(counter);
            spawnPointIndex++;
        }
    }

    //[Header("Variables")]
    //[Tooltip("It counts how many items will be spawned")]
    //[SerializeField] private int spawnCount;

    //public List<GameObject> eggPrefabs = new List<GameObject>();
    //[SerializeField] Transform[] spawnArea;
    //public List<GameObject> spawnedItems = new List<GameObject>();


    //// Start is called before the first frame update
    //void Start()
    //{
    //    SpawnItem();
    //}

    //public void SpawnItem()
    //{
    //    // Instantiates prefabs into spawnedItems list
    //    foreach (GameObject go in eggPrefabs)
    //    {
    //        //A: Make these into a variable
    //        int spawnX = Random.Range(-8, 8);
    //        int spawnY = Random.Range(-4, 4);

    //        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

    //        GameObject newItems = Instantiate(go, transform.position, Quaternion.identity);

    //        //A: If all you need is the spawn count and not the actual spawned items objects
    //        // Just increment the int instead of assigning
    //        // You should also assign the count AFTER and OUTSIDE the loop or its doing this again and again
    //        // Not performant
    //        spawnedItems.Add(newItems);
    //        spawnCount = spawnedItems.Count;
    //    }
    //}

}
