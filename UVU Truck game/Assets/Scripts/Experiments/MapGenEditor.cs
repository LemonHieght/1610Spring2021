using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGen))]
public class MapGenEditor : Editor
{
//Sebastian Lague
    public override void OnInspectorGUI()
    {
        MapGen mapGen = (MapGen) target;

        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.DrawMapEdit();
            }
        }
        if (GUILayout.Button("Generate"))
        {
            mapGen.DrawMapEdit();
        }
    }
}
