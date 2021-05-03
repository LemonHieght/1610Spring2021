using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu]
public class FloatScript : ScriptableObject
{
    public float value = 1f;

    void ChangeVal(float number)
    {
        value += number;
    }
}
