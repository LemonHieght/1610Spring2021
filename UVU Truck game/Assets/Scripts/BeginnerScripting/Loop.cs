using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    private int cupsInSink = 4;

    private bool emptySink = false;
    // Start is called before the first frame update
    void Start()
    {
        while (cupsInSink > 0)
        {
            Debug.Log(cupsInSink + " cups left to wash");
            cupsInSink--;
            emptySink = true;
        }
        do
        {
            print("All Done");
        } 
        while (emptySink == true);
    }
}
