﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise 
{
    //Sebastian Lague

    public enum NormalizeMode { Local, Global };
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight,int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offSet, NormalizeMode normalizeMode)
    {
       float[,] noiseMap = new float[mapWidth, mapHeight];
       
       System.Random prng = new System.Random(seed);
       Vector2[] octaveOffSet = new Vector2[octaves];

       float maxPosHeight = 0f;
       float amplitude = 1;
       float frequency = 1;
       
       for (int i = 0; i < octaves; i++)
       {
           float offSetX = prng.Next(-100000, 100000) + offSet.x;
           float offSetY = prng.Next(-100000, 100000) - offSet.y;
           octaveOffSet [i] = new Vector2(offSetX,offSetY);

           maxPosHeight += amplitude;
           amplitude *= persistance;
       }
       if (scale <= 0)
       {
           scale = 0.0001f;
       }

       float maxLocalNoiseheight = float.MinValue;
       float minLocalNoiseHeight = float.MaxValue;

       float halfWidth = mapWidth / 2f;
       float halfHeight = mapHeight / 2f;
       
       for (int y = 0; y < mapHeight; y++)
       {
           for (int x = 0; x < mapWidth; x++)
           {
               amplitude = 1;
               frequency = 1;
               float noiseHeight = 0;
               for(int i = 0; i < octaves; i++)
               {
                   float sampleX = (x - halfWidth + octaveOffSet[i].x) / scale * frequency;
                   float sampleY = (y - halfHeight + octaveOffSet[i].y) / scale * frequency;

                   float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                   noiseHeight += perlinValue * amplitude;
                   
                   amplitude *= persistance;
                   frequency *= lacunarity;
               }

               if (noiseHeight > maxLocalNoiseheight)
               {
                   maxLocalNoiseheight = noiseHeight;
               }
               else if(noiseHeight < minLocalNoiseHeight)
               {
                   minLocalNoiseHeight = noiseHeight;
               }
               noiseMap[x, y] = noiseHeight;
           }
       }
       for (int y = 0; y < mapHeight; y++)
       {
           for (int x = 0; x < mapWidth; x++)
           {
               if (normalizeMode == NormalizeMode.Local)
               {
                   noiseMap[x, y] = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseheight, noiseMap[x, y]);

               }
               else
               {
                   float normHeight = (noiseMap[x, y] +1) / (2f * maxPosHeight / 1.666f);
                   noiseMap[x, y] = Mathf.Clamp(normHeight, 0, int.MaxValue);
               }
               ////best for All at once not endless
               // noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseheight, noiseMap[x, y]);
           }
       }
       return noiseMap;
    }
}
