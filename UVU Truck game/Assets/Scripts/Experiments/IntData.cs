using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntData : ScriptableObject
{
    public int value = 1;

    void ChangeVal(int number)
    {
        value += number;
    }
}
