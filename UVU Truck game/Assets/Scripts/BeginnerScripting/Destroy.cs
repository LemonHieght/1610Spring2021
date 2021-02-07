using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject other;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // Destroy(gameObject);
            Destroy(other);
            // Destroy(GetComponent<MeshRenderer>());
        }
    }
}
