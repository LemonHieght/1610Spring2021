using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab;

    private Vector3 spawnLocation = new Vector3(35, 1, 0);
    private float startDelay = 2;
    private float repeatingDelay = 2;
    
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawningObject", startDelay, repeatingDelay);
    }
    void SpawningObject()
    {
        Instantiate(prefab, spawnLocation, prefab.transform.rotation);
    }
}
