using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawn : MonoBehaviour
{
    public GameObject enemy;

    private float spawnRange = 9;
    
    // Start is called before the first frame update
    void Start()
    {
        float spawnX = UnityEngine.Random.Range(-spawnRange, spawnRange);
        float spawnZ = UnityEngine.Random.Range(-spawnRange, spawnRange);
        Vector3 random = new Vector3(spawnX, 0, spawnZ);
        Instantiate(enemy, random, enemy.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
