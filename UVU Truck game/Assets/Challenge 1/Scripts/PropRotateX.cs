using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRotateX : MonoBehaviour
{
    public float speed = 20f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back *Time.deltaTime *speed);
    }
}
