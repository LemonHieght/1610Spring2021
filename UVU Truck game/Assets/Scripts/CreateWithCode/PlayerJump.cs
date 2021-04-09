using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rigid;
    public float jumpForce = 100;
    public float gravityMod;
    public bool isGrounded = true;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMod;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded == true)
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
    }
}
