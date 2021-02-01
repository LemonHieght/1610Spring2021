using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSyn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.position.x);
        if (transform.position.y <= 5f)
        {
            Debug.Log("Soon may the Wellerman come...");
            
        }
    }
}
