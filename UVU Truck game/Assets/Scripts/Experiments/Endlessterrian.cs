using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class Endlessterrian : MonoBehaviour
{
    public const float maxViewDst = 300;
    public Transform viewer;

    public static Vector2 viewerPosition;
    private int chunkSize;
    private int chunkVisibleInViewDst;

    private Dictionary<Vector2, TerrainChunk> terrainChunksDictionary = new Dictionary<Vector2, TerrainChunk>();

    void Start()
    {
        chunkSize = MapGen.mapSize - 1;
        chunkVisibleInViewDst = Mathf.RoundToInt(maxViewDst/chunkSize);
    }

    void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
        UpdateVisibleChunks();
    }

    void UpdateVisibleChunks()
    {
        int currentChunkCoordX = Mathf.RoundToInt(viewer.position.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewer.position.y / chunkSize);

        for (int yOffset = -chunkVisibleInViewDst; yOffset <= chunkVisibleInViewDst; yOffset++)
        {
            for (int xOffset = -chunkVisibleInViewDst; xOffset <= chunkVisibleInViewDst; xOffset++)
            {
                Vector2 viewChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                if (terrainChunksDictionary.ContainsKey(viewChunkCoord))
                {
                    terrainChunksDictionary[viewChunkCoord].UpdateTerrainChunk();
                }
                else
                {
                    terrainChunksDictionary.Add(viewChunkCoord, new TerrainChunk(viewChunkCoord, chunkSize));
                }
            }
        }
    }

    public class TerrainChunk
    {
        private GameObject meshObject;
        private Vector2 position;
        private Bounds bounds;
        
        public TerrainChunk(Vector2 coord, int size)
        {
            position = coord * size;
            bounds = new Bounds(position, Vector2.one*size);
            Vector3 positionV3 = new Vector3(position.x,0,position.y);
            meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            meshObject.transform.position = positionV3;
            meshObject.transform.localScale = Vector3.one * size / 10f;
            SetVisible(false);
        }

        public void UpdateTerrainChunk()
        {
            float viewerDstFromEdge =Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
            bool visible = viewerDstFromEdge <= maxViewDst;
            SetVisible(visible);
        }

        public void SetVisible(bool visible)
        {
            meshObject.SetActive(visible);
        }
    }
}
