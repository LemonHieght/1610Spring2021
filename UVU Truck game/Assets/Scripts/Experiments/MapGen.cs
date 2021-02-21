using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    //Sebastian Lague
    public enum DrawMode {NoiseMap, ColorMap, Mesh};

    public DrawMode drawMode;

     private const int mapSize = 241;
     [Range(0,6)]
     public int levelOfDetail;
     public float noiseScale;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offSet;
    public float meshHeightMult;
    public AnimationCurve meshHeightCurve;
    
    public bool autoUpdate;

    public TerrainType[] terrain;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapSize, mapSize, seed, noiseScale, octaves, persistance, lacunarity, offSet);

        Color[] colorMap = new Color[mapSize * mapSize];
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < terrain.Length; i++)
                {
                    if (currentHeight <= terrain [i].heightValue)
                    {
                        colorMap[y * mapSize + x] = terrain[i].color;
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
            display.DrawTexture(TextureGen.TextureFromColorMap(colorMap, mapSize, mapSize));
        }
        else if(drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGen.GenerateTerrainMesh(noiseMap, meshHeightMult, meshHeightCurve, levelOfDetail), TextureGen.TextureFromColorMap(colorMap, mapSize, mapSize));;
        }
    }

    void OnValidate()
    {
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

