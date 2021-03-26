using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 spawnPos = new Vector3(Random.Range(-23, 23), 0, -15);
            int animalIndex = Random.Range(0, animalPrefabs.Length);
            
            Instantiate(animalPrefabs[animalIndex], spawnPos,
                animalPrefabs[animalIndex].transform.rotation);  
        }
    }
}
