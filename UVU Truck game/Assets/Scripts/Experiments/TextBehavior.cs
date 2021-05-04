using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextBehavior : MonoBehaviour
{
    private Text textObject;
    public IntData data;
    
    // Start is called before the first frame update
    void Start()
    {
        textObject = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textObject.text = data.value.ToString();
    }
}
