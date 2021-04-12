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
    bool gameOver = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMod;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

        }

        if (gameOver == true)
        {
            GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        gameObject.SetActive(false);
    }
}