﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
