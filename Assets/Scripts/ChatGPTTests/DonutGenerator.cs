using UnityEngine;

public class DonutGenerator : MonoBehaviour
{
    public float radius = 2.0f; // Outer radius of the torus
    public float tubeRadius = 0.5f; // Radius of the tube

    public int numSegments = 32; // Number of segments around the torus
    public int numSides = 16; // Number of sides along the tube

    void Start()
    {
        GenerateDonutMesh();
    }

    void GenerateDonutMesh()
    {
        Mesh donutMesh = new Mesh();
        donutMesh.name = "Donut";

        // Vertices and triangles arrays
        Vector3[] vertices = new Vector3[(numSegments + 1) * (numSides + 1)];
        int[] triangles = new int[numSegments * numSides * 6];

        // Generate vertices and triangles
        for (int i = 0; i <= numSegments; i++)
        {
            for (int j = 0; j <= numSides; j++)
            {
                float u = (float)i / numSegments * 2 * Mathf.PI;
                float v = (float)j / numSides * 2 * Mathf.PI;

                float x = (radius + tubeRadius * Mathf.Cos(v)) * Mathf.Cos(u);
                float y = (radius + tubeRadius * Mathf.Cos(v)) * Mathf.Sin(u);
                float z = tubeRadius * Mathf.Sin(v);

                vertices[i * (numSides + 1) + j] = new Vector3(x, y, z);

                if (i < numSegments && j < numSides)
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
        }

        // Assign vertices and triangles to the mesh
        donutMesh.vertices = vertices;
        donutMesh.triangles = triangles;

        // Calculate normals (for shading)
        donutMesh.RecalculateNormals();

        // Create a MeshFilter and MeshRenderer for the GameObject
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        // Assign the donut mesh to the MeshFilter
        meshFilter.mesh = donutMesh;
    }
}
