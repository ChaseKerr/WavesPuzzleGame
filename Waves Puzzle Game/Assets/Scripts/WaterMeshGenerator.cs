using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WaterMeshGenerator : MonoBehaviour
{
    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;

    [Header("Water Dimensions")]
    [SerializeField]
    private int xSize = 0;
    [SerializeField]
    private int ySize = 0;

    // temporary
    private List<bool> activated = new List<bool>();  // list of which indices are active
    private List<float> times = new List<float>();
    private float waveRadius = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        vertices = new Vector3[xSize * ySize];

        // # of quads * 2 tris per quad * 3 points per tri
        int numTris = (xSize - 1) * (ySize - 1) * 2 * 3;
        triangles = new int[numTris];

        CreateWater();
        UpdateMesh();

        mesh.RecalculateNormals();

        // temp
        for (int i = 0; i < xSize * ySize; ++i)
        {
            activated.Add(false);
            times.Add(0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        waveRadius += 5 * Time.deltaTime;

        // UPDATE VERTICES

        // calculate next vertices to be activated
        Vector2Int origin = new Vector2Int(50, 50);
        List<Vector2Int> next = new List<Vector2Int>();
        float threshold = waveRadius * waveRadius;

        for (int i = (int)-waveRadius; i <= (int)waveRadius; ++i)
        {
            for (int j = (int)-waveRadius; j <= (int)waveRadius; ++j)
            {
                if (i * i + j * j < threshold)
                {
                    next.Add(new Vector2Int(i, j));
                }
            }
        }

        // check all points and activate points not already active
        foreach (Vector2Int pos in next)
        {
            // original pos centered around 0,0
            // so this adjusts by shifting the wave origin
            Vector2Int shifted = pos + origin;

            if (shifted.x >= 0 && shifted.x < xSize &&
                shifted.y >= 0 && shifted.y < ySize)
            {
                // convert pos to index
                int index = GetNearestWaterVertexIndex(shifted);

                // add index if not already activated
                if (!activated[index])
                {
                    activated[index] = true;
                    times[index] = Time.time;
                }
            }
        }

        // update all vertices
        for (int v = 0; v < xSize * ySize; ++v)
        {
            if (activated[v])
            {
                vertices[v].y = Mathf.Sin(Time.time - times[v]);
            }
        }

        UpdateMesh();
    }

    // Creates the mesh representing the water
    void CreateWater()
    {
        // vertices
        int i = 0;

        for (int y = 0; y < ySize; ++y) // z (in 3D)
        {
            for (int x = 0; x < xSize; ++x)
            {
                vertices[i] = new Vector3(x, 0, y);

                i++;
            }
        }

        // triangles
        i = 0;

        for (int y = 0; y < ySize - 1; ++y) // z (in 3D)
        {
            for (int x = 0; x < xSize - 1; ++x)
            {
                int curVertex = (y * xSize) + x;

                triangles[i] = curVertex;
                triangles[i + 1] = curVertex + xSize;
                triangles[i + 2] = curVertex + xSize + 1;

                triangles[i + 3] = curVertex;
                triangles[i + 4] = curVertex + xSize + 1;
                triangles[i + 5] = curVertex + 1;

                i += 6;
            }
        }
    }

    // Updates the mesh's vertices, triangles, and recalculates normals (costly)
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    // Given a position in world space, converts to local space (of water) 
    // and returns nearest vertex index (of vertices array)
    int GetNearestWaterVertexIndex(Vector3 position)
    {
        Vector3Int roundedPos = Vector3Int.RoundToInt(position);
        return (roundedPos.z * ySize) + roundedPos.x;
    }

    int GetNearestWaterVertexIndex(Vector2Int position)
    {
        return (position.y * ySize) + position.x;
    }

    // PUBLIC
    public void CreateWave(Vector3 rawOrigin)
    {
        int waveOrigin = GetNearestWaterVertexIndex(rawOrigin);

        // more
    }
}
