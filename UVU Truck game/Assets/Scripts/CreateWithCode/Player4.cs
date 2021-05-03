using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player4 : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rigid;
    private GameObject focal;
    public float timer;
    private bool timerActive = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        focal = GameObject.Find("Focus Point");
        timer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        rigid.AddForce(focal.transform.forward * speed * vertical);
        
        if (timerActive == true)
        {
            timer -= Time.deltaTime;

        }
        else if (timer >= 0f)
        {
            timerActive = false;
            timer = 5f;
            speed = 10f;
        }
        
    }

    void Speed(bool powerUp)
    {
        timerActive = powerUp;
        speed *= 2;
    }
}
