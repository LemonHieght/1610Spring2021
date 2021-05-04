using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1;
    public Text gameOver;
    public IntData data;
    public bool gameActive;
    public GameObject titleScreen;
    
    // Start is called before the first frame update
    public void StartGame(int difficult)
    {
        titleScreen.gameObject.SetActive(false);
        gameActive = true;
        spawnRate /= difficult;
        StartCoroutine(SpawnTargt());
        gameOver.gameObject.SetActive(false);
    }

    private void Start()
    {
        data.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (data.value < 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameActive = false;
        gameOver.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnTargt()
    {
        while (gameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = UnityEngine.Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
}
