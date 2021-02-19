using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public int mapWith;
    public int mapHeight;
    public float noiseScale;
    public int octaves;
    public float persistance;
    public float lacunarity;
    
    public bool autoUpdate;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWith, mapHeight, noiseScale, octaves, persistance, lacunarity);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }
}
