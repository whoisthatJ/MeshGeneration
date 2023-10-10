using UnityEngine;

public class MugGenerator : MonoBehaviour
{
    public int numSegments = 32; // Number of segments around the mug
    public int numSides = 16; // Number of sides along the mug's height
    public float radiusTop = 1.0f; // Top radius of the mug
    public float radiusBottom = 0.8f; // Bottom radius of the mug
    public float height = 2.0f; // Height of the mug
    public float handleRadius = 0.2f; // Radius of the handle

    void Start()
    {
        GenerateMugWithHandleMesh();
    }

    void GenerateMugWithHandleMesh()
    {
        Mesh mugMesh = new Mesh();
        mugMesh.name = "MugWithHandle";

        // Vertices and triangles arrays
        int totalVertices = (numSegments + 1) * (numSides + 1);
        Vector3[] vertices = new Vector3[totalVertices];
        int[] triangles = new int[numSegments * numSides * 6];

        // Generate vertices and triangles for the mug body
        for (int i = 0; i <= numSegments; i++)
        {
            for (int j = 0; j <= numSides; j++)
            {
                float u = (float)i / numSegments * 2 * Mathf.PI;
                float v = (float)j / numSides * height;

                float x = Mathf.Cos(u);
                float y = v;
                float z = Mathf.Sin(u);

                // Interpolate the radius from top to bottom
                float t = (float)j / numSides;
                float radius = Mathf.Lerp(radiusTop, radiusBottom, t);

                vertices[i * (numSides + 1) + j] = new Vector3(x * radius, y, z * radius);
            }
        }

        // Generate vertices and triangles for the handle
        int handleStartVertexIndex = totalVertices;
        for (int i = 0; i <= numSegments; i++)
        {
            for (int j = 0; j <= numSides; j++)
            {
                float u = (float)i / numSegments * 2 * Mathf.PI;
                float v = (float)j / numSides * height;

                float x = Mathf.Cos(u) * (radiusBottom + handleRadius);
                float y = v + handleRadius;
                float z = Mathf.Sin(u) * (radiusBottom + handleRadius);

                vertices[handleStartVertexIndex + i * (numSides + 1) + j] = new Vector3(x, y, z);
            }
        }

        // Generate triangles for the mug body
        for (int i = 0; i < numSegments; i++)
        {
            for (int j = 0; j < numSides; j++)
            {
                int topLeft = i * (numSides + 1) + j;
                int topRight = topLeft + 1;
                int bottomLeft = (i + 1) * (numSides + 1) + j;
                int bottomRight = bottomLeft + 1;

                triangles[(i * numSides + j) * 6 + 0] = topLeft;
                triangles[(i * numSides + j) * 6 + 1] = bottomLeft;
                triangles[(i * numSides + j) * 6 + 2] = topRight;
                triangles[(i * numSides + j) * 6 + 3] = topRight;
                triangles[(i * numSides + j) * 6 + 4] = bottomLeft;
                triangles[(i * numSides + j) * 6 + 5] = bottomRight;
            }
        }

        // Generate triangles for the handle
        for (int i = 0; i < numSegments; i++)
        {
            for (int j = 0; j < numSides; j++)
            {
                int topLeft = handleStartVertexIndex + i * (numSides + 1) + j;
                int topRight = topLeft + 1;
                int bottomLeft = handleStartVertexIndex + (i + 1) * (numSides + 1) + j;
                int bottomRight = bottomLeft + 1;

                triangles[(i * numSides + j) * 6 + 0 + numSegments * numSides * 6] = topLeft;
                triangles[(i * numSides + j) * 6 + 1 + numSegments * numSides * 6] = bottomLeft;
                triangles[(i * numSides + j) * 6 + 2 + numSegments * numSides * 6] = topRight;
                triangles[(i * numSides + j) * 6 + 3 + numSegments * numSides * 6] = topRight;
                triangles[(i * numSides + j) * 6 + 4 + numSegments * numSides * 6] = bottomLeft;
                triangles[(i * numSides + j) * 6 + 5 + numSegments * numSides * 6] = bottomRight;
            }
        }

        // Assign vertices and triangles to the mesh
        mugMesh.vertices = vertices;
        mugMesh.triangles = triangles;

        // Calculate normals (for shading)
        mugMesh.RecalculateNormals();

        // Create a MeshFilter and MeshRenderer for the GameObject
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        // Assign the mug mesh to the MeshFilter
        meshFilter.mesh = mugMesh;
    }
}
