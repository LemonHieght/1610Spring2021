using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class If : MonoBehaviour
{
    public float drinkingChocolateTemp = 150f;
    public float hotLimitTemp = 100f;
    public float coldLimitTemp = 70f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TempTest();
        }
    }

    void FixedUpdate()
    {
        drinkingChocolateTemp -= Time.deltaTime;
    }
    void TempTest()
    {
        if (drinkingChocolateTemp >= hotLimitTemp)
        {
            print("Hot Chocolate to hot");
        }
        else if (drinkingChocolateTemp <= coldLimitTemp)
        {
            print("Hot Chocolate is now Chocolate milk");
        }
        else
        {
            print("Hot Chocolate is just right");
        }
    }
}
