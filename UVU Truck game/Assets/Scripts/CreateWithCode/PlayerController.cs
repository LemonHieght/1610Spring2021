using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;

    public float turnSpeed = 5f;

    private float horizontalInput;

    private float verticalInput;
    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //forward and backwards movement through vertical input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        //turning movement through horizontal input
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
    }
}
