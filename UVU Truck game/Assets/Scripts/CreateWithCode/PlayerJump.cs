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
    private Animator playerAni;
    public ParticleSystem death;
    public GameObject playerModel;
    
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMod;
        playerAni = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            playerAni.SetTrigger("Jump_trig");
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
        Instantiate(death, playerModel.transform.position, playerModel.transform.rotation);
        gameObject.SetActive(false);
    }
}