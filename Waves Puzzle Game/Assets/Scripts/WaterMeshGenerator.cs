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
    }

    // Update is called once per frame
    void Update()
    {
        // update vertices
        for (int i = 0; i < 10; ++i)
            vertices[Random.Range(0, xSize * ySize)].y += 0.25f;

        UpdateMesh();
    }

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

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
