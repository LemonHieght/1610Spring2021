using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public enum DrawMode {NoiseMap, ColorMap};

    public DrawMode drawMode;

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

    public TerrainType[] terrain;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offSet);

        Color[] colorMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < terrain.Length; i++)
                {
                    if (currentHeight <= terrain [i].heightValue)
                    {
                        colorMap[y * mapWidth + x] = terrain[i].color;
                        break;
                    }
                }
            }
        }
        
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGen.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGen.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        }
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

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float heightValue;
    public Color color;
    
}

