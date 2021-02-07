using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeAndMod : MonoBehaviour
{
    public int alpha = 5;

    private int beta = 0;

    private int charlie = 5;
    
    private AnotherScript myOtherClass;
    
    // Start is called before the first frame update
    void Start()
    {
        alpha = 10;
        myOtherClass.FruitMachine(alpha,myOtherClass.apples);
    }

    void Example(int pens, int crayons)
    {
        int answer;
        answer = pens * crayons * beta;
        Debug.Log(answer);
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Alpha is set to: " + alpha);
    }
}
