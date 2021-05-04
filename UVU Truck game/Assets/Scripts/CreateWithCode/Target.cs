using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Target : MonoBehaviour
{
    private Rigidbody rigid;
    private float minSpeed = 10;
    private float maxSpeed = 20;
    private float maxTorque = 10;
    private float xRange = 4;
    private float yRange = -2;
    public int pointVal;
    public IntData intData;
    public ParticleSystem explosion;
    private GameManager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        
        rigid.AddForce(RandomForce(), ForceMode.Impulse);
        rigid.AddTorque(RandomTorque(),RandomTorque(), RandomTorque(), ForceMode.Impulse);
        
        transform.position = RandomPosition();
    }

    Vector3 RandomForce()
        {
            return Vector3.up * UnityEngine.Random.Range(minSpeed, maxSpeed);
        }

    Vector3 RandomPosition()
    {
        return new Vector3( UnityEngine.Random.Range(-xRange, xRange), yRange);
    }

    float RandomTorque()
    {
        return UnityEngine.Random.Range(-maxTorque, maxTorque);
    }
    
    private void OnMouseDown()
    {
        intData.value += pointVal;
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Enemy"))
        {
            intData.value -= 20;
        }
    }
}
