using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TestGeneratorHI : MonoBehaviour
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
    [SerializeField]
    private float m_UniversalScale;

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
        int[] tris = new int[3 * 2 * (m_Resolution.x - 1) * (m_Resolution.y - 1)];

        int maxRes = Mathf.Max(m_Resolution.x, m_Resolution.y);
        Vector2 resOffset = Vector2.zero;
        resOffset.x = (m_Resolution.x - 1f) / (maxRes - 1f);
        resOffset.y = (m_Resolution.y - 1f) / (maxRes - 1f);

        int triIndex = 0;
        for (int y = 0, i = 0; y < m_Resolution.y; y++)
        {
            for (int x = 0; x < m_Resolution.x; x++, i++)
            {
                //Möglichkeit 1
                //Vector2 percent = new Vector2(x, y);
                //percent.x /= (m_Resolution.x - 1); //Bereich 0-1
                //percent.y /= (m_Resolution.y - 1); //Bereich 0-1
                //percent -= Vector2.one * 0.5f;

                //verts[i] = new Vector3(percent.x * m_Scale.x, 0, percent.y * m_Scale.y);

                //Möglichkeit 2
                Vector2 percent = new Vector2(x, y) / (maxRes - 1);
                percent -= resOffset * 0.5f;

                verts[i] = new Vector3(percent.x, 0, percent.y) * m_UniversalScale;

                if (x < m_Resolution.x - 1 && y < m_Resolution.y - 1)
                {
                    tris[triIndex + 0] = i;
                    tris[triIndex + 1] = i + m_Resolution.x + 1;
                    tris[triIndex + 2] = i + 1;

                    tris[triIndex + 3] = i;
                    tris[triIndex + 4] = i + m_Resolution.x;
                    tris[triIndex + 5] = i + m_Resolution.x + 1;

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
