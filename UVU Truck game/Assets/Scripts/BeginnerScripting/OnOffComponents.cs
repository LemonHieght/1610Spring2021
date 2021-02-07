using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffComponents : MonoBehaviour
{
    private Light myLight;
    
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            myLight.enabled = !myLight.enabled;
        }
    }
}
