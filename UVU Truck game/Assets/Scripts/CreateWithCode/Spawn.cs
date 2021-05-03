using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawn : MonoBehaviour
{
    public GameObject enemy;
    private int enemyCount;
    private float spawnRange = 9;
    private Vector3 generate;
    private int waveNum = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        float spawnX = UnityEngine.Random.Range(-spawnRange, spawnRange);
        float spawnZ = UnityEngine.Random.Range(-spawnRange, spawnRange);
        Vector3 random = new Vector3(spawnX, 0, spawnZ);
        generate = random;
        
        SpawnWave(waveNum);
    }
    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNum++;
            SpawnWave(waveNum);
        }
    }
    void SpawnWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemy, generate, enemy.transform.rotation);
        }
    }
}
