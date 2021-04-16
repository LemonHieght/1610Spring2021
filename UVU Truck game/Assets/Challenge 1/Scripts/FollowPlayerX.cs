using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class FollowPlayerX : MonoBehaviour
{
    public GameObject plane;
    public Vector3 offset = new Vector3(0, 3, -7);
    public float turnSpeed = 10f;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        offset = Quaternion.AngleAxis(Input.GetAxisRaw("Mouse X") * turnSpeed, Vector3.up)  * offset;
        transform.position = plane.transform.position + offset;
        transform.LookAt(plane.transform.position);
    }
}
