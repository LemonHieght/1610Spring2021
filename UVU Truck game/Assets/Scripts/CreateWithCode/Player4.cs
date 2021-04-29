using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player4 : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rigid;
    private GameObject focal;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        focal = GameObject.Find("Focus Point");
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        rigid.AddForce(focal.transform.forward * speed * vertical);
    }
}
