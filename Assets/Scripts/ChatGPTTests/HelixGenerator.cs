using UnityEngine;

public class HelixGenerator : MonoBehaviour
{
    public float radius = 1.0f; // Radius of the helix
    public float height = 5.0f; // Height of the helix
    public float twists = 5.0f; // Number of twists in the helix
    public int numSegments = 100; // Number of segments in the helix

    void Start()
    {
        GenerateHelixMesh();
    }

    void GenerateHelixMesh()
    {
        Mesh helixMesh = new Mesh();
        helixMesh.name = "Helix";

        int totalVertices = (numSegments + 1) * 2;
        Vector3[] vertices = new Vector3[totalVertices];
        int[] triangles = new int[numSegments * 6];

        float angleIncrement = 2 * Mathf.PI * twists / numSegments;

        // Generate vertices for the helix
        for (int i = 0; i <= numSegments; i++)
        {
            float u = i * angleIncrement;
            float x = Mathf.Cos(u) * radius;
            float z = Mathf.Sin(u) * radius;
            float y = (float)i / numSegments * height;

            vertices[i] = new Vector3(x, y, z);
            vertices[i + numSegments + 1] = new Vector3(x, y, z);
        }

        // Generate triangles for the helix
        for (int i = 0; i < numSegments; i++)
        {
            int bottomLeft = i;
            int bottomRight = i + 1;
            int topVertex = numSegments + 1;
            int topRight = (i + 1) % numSegments + numSegments + 1;

            triangles[i * 6] = bottomLeft;
            triangles[i * 6 + 1] = topVertex;
            triangles[i * 6 + 2] = bottomRight;
            triangles[i * 6 + 3] = bottomRight;
            triangles[i * 6 + 4] = topVertex;
            triangles[i * 6 + 5] = topRight;
        }

        // Assign vertices and triangles to the mesh
        helixMesh.vertices = vertices;
        helixMesh.triangles = triangles;

        // Calculate normals (for shading)
        helixMesh.RecalculateNormals();

        // Create a MeshFilter and MeshRenderer for the GameObject
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        // Assign the helix mesh to the MeshFilter
        meshFilter.mesh = helixMesh;
    }
}
