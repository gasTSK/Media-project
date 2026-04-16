using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GableMesh : MonoBehaviour
{
    public float width = 10f;
    public float wallHeight = 3f;
    public float roofPeak = 5.5f;

    void OnEnable()
    {
        GenerateMesh();
    }

    void OnValidate()
    {
        GenerateMesh();
    }

    void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        float halfWidth = width / 2f;

        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-halfWidth, wallHeight, 0),
            new Vector3(halfWidth, wallHeight, 0),
            new Vector3(0, roofPeak, 0)
        };

        int[] triangles = new int[]
        {
            0, 2, 1
        };

        Vector2[] uv = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0.5f, 1)
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}