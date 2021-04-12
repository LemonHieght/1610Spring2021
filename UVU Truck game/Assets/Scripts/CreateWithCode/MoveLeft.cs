using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20f;
    private float leftBoarder = -15;
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        if (transform.position.x < leftBoarder)
        {
            Destroy(gameObject);
        }
    }
}
