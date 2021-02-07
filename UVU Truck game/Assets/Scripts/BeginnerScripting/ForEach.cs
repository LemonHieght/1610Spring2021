using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForEach : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[] strings = new string[3];
        
        strings[0] = "One";
        strings[1] = "Two";
        strings[2] = "Three";

        foreach (string item in strings)
        {
            print(item);
        }
    }

}
