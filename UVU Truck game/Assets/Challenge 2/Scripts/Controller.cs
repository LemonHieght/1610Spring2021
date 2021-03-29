using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject dogPrefab;
    private float timer = 1f;
    public bool shoot = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && shoot == true)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            shoot = false;

        }

        else if (shoot == false)
        {
            timer--;
        }

        if (timer <= 0)
        {
            shoot = true;
            timer = 50f;
        }
    }
}
