using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public float startDelay = 2f;
    public float spawnInterval = 1.5f;

    private void Start()
    {
        InvokeRepeating("SpawnAnimal",startDelay,spawnInterval);
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.S))
        {
           SpawnAnimal();
        }*/
    }

    void SpawnAnimal()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-23, 23), 0, -15);
        int animalIndex = Random.Range(0, animalPrefabs.Length);
            
        Instantiate(animalPrefabs[animalIndex], spawnPos,
            animalPrefabs[animalIndex].transform.rotation);  
    }
}
