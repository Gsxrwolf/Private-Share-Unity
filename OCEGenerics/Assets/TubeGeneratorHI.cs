using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TubeGeneratorHI : MonoBehaviour
{
    private MeshFilter meshFilter;
    private Mesh mesh;
    private MeshRenderer meshRenderer;
    [SerializeField]
    private Material m_MeshMaterial;

    [SerializeField]
    private Vector2Int m_Resolution;
    [SerializeField]
    private Vector2 m_Scale;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();
        mesh.name = "TestMesh";
        meshFilter.sharedMesh = mesh;

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = m_MeshMaterial;
    }

    private void Update()
    {
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        Vector3[] verts = new Vector3[m_Resolution.x * m_Resolution.y];
        int[] tris = new int[3 * 2 * (m_Resolution.x) * (m_Resolution.y - 1)];

        int triIndex = 0;
        for (int y = 0, i = 0; y < m_Resolution.y; y++)
        {
            for (int x = 0; x < m_Resolution.x; x++, i++)
            {
                //Möglichkeit 1
                Vector2 percent = new Vector2(x, y);
                percent.x /= (m_Resolution.x); //Bereich 0 - (res.x - 1) / res.x
                percent.y /= (m_Resolution.y - 1); //Bereich 0-1
                percent.y -= 0.5f;

                float currentAngleRad = percent.x * 360f * Mathf.Deg2Rad; //same as percent.x * Mathf.PI * 2;
                // * m_Scale.x ist der Radius
                Vector2 circlePos = new Vector2(Mathf.Cos(currentAngleRad), Mathf.Sin(currentAngleRad)) * m_Scale.x;

                verts[i] = new Vector3(circlePos.x, circlePos.y, percent.y * m_Scale.y);

                if (y < m_Resolution.y - 1)
                {
                    if(x < m_Resolution.x - 1)
                    {
                        tris[triIndex + 0] = i;
                        tris[triIndex + 1] = i + 1;
                        tris[triIndex + 2] = i + m_Resolution.x + 1;

                        tris[triIndex + 3] = i;
                        tris[triIndex + 4] = i + m_Resolution.x + 1;
                        tris[triIndex + 5] = i + m_Resolution.x;
                    }
                    else //this is the right side case
                    {
                        tris[triIndex + 0] = i;
                        tris[triIndex + 1] = i - m_Resolution.x + 1;
                        tris[triIndex + 2] = i + 1;

                        tris[triIndex + 3] = i;
                        tris[triIndex + 4] = i + 1;
                        tris[triIndex + 5] = i + m_Resolution.x;
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
