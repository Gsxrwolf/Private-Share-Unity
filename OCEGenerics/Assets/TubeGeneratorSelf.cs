using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeGeneratorSelf : MonoBehaviour
{
    private MeshFilter filter;
    private Mesh mesh;
    private MeshRenderer renderer;
    [SerializeField] private Material material;
    [SerializeField] private Vector2Int resolution;
    [SerializeField] private Vector2 scale;
    private void Update()
    {
        filter = GetComponent<MeshFilter>();
        mesh = new Mesh();
        mesh.name = "testMesh";
        filter.sharedMesh = mesh;

        renderer = GetComponent<MeshRenderer>();
        renderer.sharedMaterial = material;
        GenerateMesh();
    }
    private void GenerateMesh()
    {
        Vector3[] verts = new Vector3[resolution.x * resolution.y];
        int[] tris = new int[3 * 2 * (resolution.x) * (resolution.y - 1)];

        int triIndex = 0;
        for (int y = 0, i = 0; y < resolution.y; y++)
        {
            for (int x = 0; x < resolution.x; x++, i++)
            {
                verts[i] = new Vector3(x, 0, y);

                Vector2 percent = new Vector2(x, y);
                percent.x /= (resolution.x);
                percent.y /= (resolution.y - 1);
                percent -= Vector2.one * 0.5f;

                float curAngleRad = percent.x * 360f * Mathf.Deg2Rad;
                Vector2 circlePos = new Vector2(Mathf.Cos(curAngleRad), Mathf.Sin(curAngleRad)) * scale.x;

                verts[i] = new Vector3(circlePos.x, circlePos.y, percent.y * scale.y);

                if ( y < resolution.y - 1)
                {
                    if( x < resolution.x - 1 )
                    {
                        tris[triIndex + 0] = i;
                        tris[triIndex + 1] = i + 1;
                        tris[triIndex + 2] = i + resolution.x + 1;

                        tris[triIndex + 3] = i;
                        tris[triIndex + 4] = i + resolution.x + 1;
                        tris[triIndex + 5] = i + resolution.x;
                    }
                    else
                    {
                        tris[triIndex + 0] = i;
                        tris[triIndex + 1] = i - resolution.x + 1;
                        tris[triIndex + 2] = i + 1;

                        tris[triIndex + 3] = i;
                        tris[triIndex + 4] = i + 1;
                        tris[triIndex + 5] = i + resolution.x;
                    }

                    triIndex += 6;
                }
            }
        }

        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
    }
}
