using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent triggerEnter;
    public UnityEvent triggerStay;
    public UnityEvent triggerExit;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (player)
        {
            triggerEnter.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (player)
        {
            triggerStay.Invoke();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (player)
        {
            triggerExit.Invoke();
        }
    }
}
