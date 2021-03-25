using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;
    private int playArea = 20;
    public GameObject carrotPrefab;

    // public float turnSpeed = 5f;

    private float horizontalInput;

    private float verticalInput;
    // Update is called once per frame
    private void Update()
    {
        if (transform.position.x < -playArea)
        {
            transform.position = new Vector3(-playArea, transform.position.y, transform.position.z);
        }
        if (transform.position.x > playArea)
        {
            transform.position = new Vector3(playArea, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,-10);
        }
        if (transform.position.z > 30)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 30);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //forward and backwards movement through vertical input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        //turning movement through horizontal input
        // transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
    }

    void Shoot()
    {
        Instantiate(carrotPrefab, transform.position, carrotPrefab.transform.rotation);
    }
}
