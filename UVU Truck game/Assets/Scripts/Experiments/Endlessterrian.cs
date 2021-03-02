using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class Endlessterrian : MonoBehaviour
{
    private const float viewerMoveThresholdChunkUpdate = 25f;
    private const float sqrViewerMoveThresholdChunkUpdate = viewerMoveThresholdChunkUpdate * viewerMoveThresholdChunkUpdate;
    
    public static float maxViewDst;
    public LODInfo[] detailLevel;
    public Transform viewer;
    public Material mapMaterial;

    private static MapGen mapGenerator;
    public static Vector2 viewerPosition;
    private Vector2 viewerPositionOld;
    private int chunkSize;
    private int chunkVisibleInViewDst;

    private Dictionary<Vector2, TerrainChunk> terrainChunksDictionary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> _terrainChunksVisibleLast = new List<TerrainChunk>();

    void Start()
    {
        mapGenerator = FindObjectOfType<MapGen>();
        maxViewDst = detailLevel[detailLevel.Length - 1].visibleDstThreshold;
        chunkSize = MapGen.mapSize - 1;
        chunkVisibleInViewDst = Mathf.RoundToInt(maxViewDst/chunkSize);
        UpdateVisibleChunks();
    }

    void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
        if ((viewerPositionOld - viewerPosition).sqrMagnitude > sqrViewerMoveThresholdChunkUpdate)
        {
            viewerPositionOld = viewerPosition;
            UpdateVisibleChunks();
            
        }
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
                    terrainChunksDictionary.Add(viewChunkCoord, new TerrainChunk(viewChunkCoord, chunkSize, detailLevel, transform, mapMaterial));
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
        private bool receivedMapData;
        private int perviousDetailLevel = -1;
        
        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;
        private LODInfo[] detailLevel;
        private LODMesh[] lodMeshes;
        
        
        
        public TerrainChunk(Vector2 coord, int size, LODInfo[] detailLevel, Transform parent, Material material)
        {
            this.detailLevel = detailLevel;
            position = coord * size;
            bounds = new Bounds(position, Vector2.one*size);
            Vector3 positionV3 = new Vector3(position.x,0,position.y);
            
            meshObject = new GameObject("terrian Chunk");
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshRenderer.material = material;
            
            meshObject.transform.position = positionV3;
            // meshObject.transform.localScale = Vector3.one * size / 10f;
            meshObject.transform.parent = parent;
            SetVisible(false);

            lodMeshes = new LODMesh[detailLevel.Length];
            for (int i = 0; i < detailLevel.Length; i++)
            {
                lodMeshes [i] = new LODMesh(detailLevel[i].lod, UpdateTerrainChunk);
            }

            mapGenerator.RequestMapData(position, OnMapDataReceive);
        }

        void OnMapDataReceive(MapData mapData)
        {
            this.mapData = mapData;
            receivedMapData = true;

            Texture2D texture2D = TextureGen.TextureFromColorMap(mapData.colorMap, MapGen.mapSize, MapGen.mapSize);
            meshRenderer.material.mainTexture = texture2D;
            
            UpdateTerrainChunk();

        }

        // private void MeshDataReceive(MeshData meshData)
        // {
        //     meshFilter.mesh = meshData.CreateMesh();
        // }
        

        public void UpdateTerrainChunk()
        {
            if (receivedMapData)
            {
                float viewerDstFromEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
                bool visible = viewerDstFromEdge <= maxViewDst;
                if (visible)
                {
                    int lodIndex = 0;
                    for (int i = 0; i < detailLevel.Length; i++)
                    {
                        if (viewerDstFromEdge > detailLevel[i].visibleDstThreshold)
                        {
                            lodIndex = i + 1;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (lodIndex != perviousDetailLevel)
                    {
                        LODMesh mesh = lodMeshes[lodIndex];
                        if (mesh.hasMesh)
                        {
                            meshFilter.mesh = mesh.mesh;
                            perviousDetailLevel = lodIndex;
                        }
                        else if (!mesh.hasRequestedMesh)
                        {
                            mesh.RequestMesh(mapData);
                        }
                    }
                }
                SetVisible(visible);
            }
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

    class LODMesh
    {
        public Mesh mesh;
        public bool hasRequestedMesh;
        public bool hasMesh;
        private int lod;

        private System.Action updateCallback;

        public LODMesh(int lod, System.Action updateCallback)
        {
            this.lod = lod;
            this.updateCallback = updateCallback;
        }

        public void ReceivedMeshData(MeshData meshData)
        {
            mesh = meshData.CreateMesh();
            hasMesh = true;

            updateCallback();
        }

        public void RequestMesh(MapData mapData)
        {
            hasRequestedMesh = true;
            mapGenerator.RequestMeshData(mapData, lod, ReceivedMeshData);
        }
    }
    [System.Serializable]
    public struct LODInfo
    {
        public int lod;
        public float visibleDstThreshold;
    }
}
