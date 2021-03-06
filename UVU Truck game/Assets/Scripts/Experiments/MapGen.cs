using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;
using System;
using System.Threading;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class MapGen : MonoBehaviour
{
    //Sebastian Lague
    public enum DrawMode {NoiseMap, ColorMap, Mesh};

    public Noise.NormalizeMode normalizeMode;

    public DrawMode drawMode;
    
    public const int mapSize = 241;
     [FormerlySerializedAs("levelOfDetail")] [Range(0,6)]
     public int editorLevelOfDetail;
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
    
    Queue<MapThreadInfo<MapData>> mapDataThreadQueue = new Queue<MapThreadInfo<MapData>>();
    Queue<MapThreadInfo<MeshData>> meshDataThreadInfoQueue = new Queue<MapThreadInfo<MeshData>>();

    public void DrawMapEdit()
    {
        MapData mapData = GenerateMapData(Vector2.zero);
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGen.TextureFromHeightMap(mapData.heightMap));
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGen.TextureFromColorMap(mapData.colorMap, mapSize, mapSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(
                MeshGen.GenerateTerrainMesh(mapData.heightMap, meshHeightMult, meshHeightCurve, editorLevelOfDetail),
                TextureGen.TextureFromColorMap(mapData.colorMap, mapSize, mapSize));
        }
    }

    public void RequestMapData(Vector2 center, Action<MapData> callback)
    {
        ThreadStart threadStart = delegate { MapDataThread(center, callback); };
        
        new Thread(threadStart).Start();
    }

    void MapDataThread(Vector2 center, Action<MapData> callback)
    {
        MapData mapData = GenerateMapData(center);
        lock (mapDataThreadQueue)
        {
            mapDataThreadQueue.Enqueue(new MapThreadInfo<MapData>(callback, mapData));
        }
    }

    public void RequestMeshData(MapData mapData, int lod, Action<MeshData> callback)
    {
        ThreadStart threadStart = delegate { MeshDataThread(mapData, lod, callback); };
        new Thread(threadStart).Start();
        
    }

    void MeshDataThread(MapData mapData, int lod, Action<MeshData> callback)
    {
        MeshData meshData = MeshGen.GenerateTerrainMesh(mapData.heightMap, meshHeightMult, meshHeightCurve, lod);
        lock (meshDataThreadInfoQueue)
        {
            meshDataThreadInfoQueue.Enqueue(new MapThreadInfo<MeshData>(callback, meshData));
        }
    }

    void Update()
    {
        if (mapDataThreadQueue.Count > 0)
        {
            for (int i = 0; i < mapDataThreadQueue.Count; i++)
            {
                MapThreadInfo<MapData> threadInfo = mapDataThreadQueue.Dequeue();
                threadInfo.callback(threadInfo.parmeter);
            }
        }

        if (meshDataThreadInfoQueue.Count > 0)
        {
            for (int i = 0; i < meshDataThreadInfoQueue.Count; i++)
            {
                MapThreadInfo<MeshData> threadInfo = meshDataThreadInfoQueue.Dequeue();
                threadInfo.callback(threadInfo.parmeter);
            }
        }
    }

    MapData GenerateMapData(Vector2 center)
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapSize, mapSize, seed, noiseScale, octaves, persistance, lacunarity, center + offSet, normalizeMode);

        Color[] colorMap = new Color[mapSize * mapSize];
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < terrain.Length; i++)
                {
                    if (currentHeight >= terrain [i].heightValue)
                    {
                        colorMap[y * mapSize + x] = terrain[i].color;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        return new MapData(noiseMap, colorMap);
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

    struct MapThreadInfo<T>
    {
        public readonly Action<T> callback;
        public readonly T parmeter;

        public MapThreadInfo(Action<T> callback, T parmeter)
        {
            this.callback = callback;
            this.parmeter = parmeter;
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

public struct MapData
{
    public readonly float[,] heightMap;
    public readonly Color[] colorMap;

    public MapData(float[,] heightMap, Color[] colorMap)
    {
        this.heightMap = heightMap;
        this.colorMap = colorMap;
    }
}

