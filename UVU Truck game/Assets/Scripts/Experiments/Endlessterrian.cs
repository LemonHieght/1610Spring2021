using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class Endlessterrian : MonoBehaviour
{
    public const float maxViewDst = 450;
    public Transform viewer;
    public Material mapMaterial;

    private static MapGen mapGenerator;
    public static Vector2 viewerPosition;
    private int chunkSize;
    private int chunkVisibleInViewDst;

    private Dictionary<Vector2, TerrainChunk> terrainChunksDictionary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> _terrainChunksVisibleLast = new List<TerrainChunk>();

    void Start()
    {
        mapGenerator = FindObjectOfType<MapGen>();
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
        for (int i = 0; i < _terrainChunksVisibleLast.Count; i++)
        {
            _terrainChunksVisibleLast [i].SetVisible(false);
        }
        _terrainChunksVisibleLast.Clear();
        
        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

        for (int yOffset = -chunkVisibleInViewDst; yOffset <= chunkVisibleInViewDst; yOffset++)
        {
            for (int xOffset = -chunkVisibleInViewDst; xOffset <= chunkVisibleInViewDst; xOffset++)
            {
                Vector2 viewChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                if (terrainChunksDictionary.ContainsKey(viewChunkCoord))
                {
                    terrainChunksDictionary[viewChunkCoord].UpdateTerrainChunk();
                    if (terrainChunksDictionary [viewChunkCoord].IsVisible())
                    {
                        _terrainChunksVisibleLast.Add(terrainChunksDictionary [viewChunkCoord]);
                    }
                }
                else
                {
                    terrainChunksDictionary.Add(viewChunkCoord, new TerrainChunk(viewChunkCoord, chunkSize, transform, mapMaterial));
                }
            }
        }
    }

    public class TerrainChunk
    {
        private GameObject meshObject;
        private Vector2 position;
        private Bounds bounds;

        private MapData mapData;
        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;
        
        public TerrainChunk(Vector2 coord, int size, Transform parent, Material material)
        {
            position = coord * size;
            bounds = new Bounds(position, Vector2.one*size);
            Vector3 positionV3 = new Vector3(position.x,0,position.y);
            
            meshObject = new GameObject("terrian Chunk");
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshRenderer.material = material;
            
            meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            meshObject.transform.position = positionV3;
            // meshObject.transform.localScale = Vector3.one * size / 10f;
            meshObject.transform.parent = parent;
            SetVisible(false);

            mapGenerator.RequestMapData(OnMapDataReceive);

        }

        void OnMapDataReceive(MapData mapData)
        {
            mapGenerator.RequestMeshData(mapData, MeshDataReceive);
            
        }

        private void MeshDataReceive(MeshData meshData)
        {
            meshFilter.mesh = meshData.CreateMesh();
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

        public bool IsVisible()
        {
            return meshObject.activeSelf;
        }
    }
}
