using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Veiwer : MonoBehaviour
{
    private Vector3 player;
    public float speed = 30f;
    private Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Turn off game.");
        player =new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        
        Movement(player);
    }

    void Movement(Vector3 direction)
    {
        rigid.MovePosition(transform.position + (direction * speed * Time.deltaTime));
        if (player != Vector3.zero)
        {
            transform.forward = player;
        }  
    }
}
