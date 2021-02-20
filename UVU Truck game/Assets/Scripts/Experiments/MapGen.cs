using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offSet;
    
    public bool autoUpdate;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offSet);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }

    void OnValidate()
    {
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }

        if (mapWidth < 1)
        {
            mapWidth = 1;
        }

        if (lacunarity < 1)
        {
            lacunarity = 1;
        }

        if (octaves < 1)
        {
            octaves = 1;
        }

        if (noiseScale < 0)
        {
            noiseScale = 0;
        }
    }
}
