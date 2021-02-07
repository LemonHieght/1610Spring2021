using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Awake");
    }

    // Update is called once per frame
    void Start()
    {
        Debug.Log("Start");
    }

    void Update()
    {
        Debug.Log("Update time: " + Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate time: " + Time.deltaTime);
    }
    
}
