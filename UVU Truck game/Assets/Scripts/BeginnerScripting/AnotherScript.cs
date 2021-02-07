using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherScript : MonoBehaviour
{
    public int apples;

    public int banana;

    private int stapler;

    private int sellotape;

    public void FruitMachine(int a, int b)
    {
        int answer;
        answer = a + b;
        Debug.Log("fruit total is: " + answer);
    }

    void OfficeSort(int a, int b)
    {
        int answer;
        answer = a + b;
        Debug.Log("Office supplies total: " + answer);
    }
}
