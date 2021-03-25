using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletTimer = 1f;
    void Update()
    {
        if (bulletTimer > 0)
        {
            bulletTimer -= Time.deltaTime;
        }
        else if (bulletTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
