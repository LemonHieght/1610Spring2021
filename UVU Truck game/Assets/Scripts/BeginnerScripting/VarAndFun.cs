using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarAndFun : MonoBehaviour
{
    private int myInt = 5; 
    // Start is called before the first frame update
    void Start()
    {
        myInt = TimesTwo(myInt);
        Debug.Log(myInt);
    }
    int TimesTwo(int number)
    {
        int bob;
        bob = number * 2;
        return bob;
    }
}
