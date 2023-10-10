using UnityEngine;

public class IceCreamConeGenerator : MonoBehaviour
{
    public float coneRadius = 1.0f; // Radius of the cone base
    public float coneHeight = 2.0f; // Height of the cone
    public float scoopRadius = 1.5f; // Radius of the ice cream scoop
    public float scoopHeight = 1.0f; // Height of the ice cream scoop

    public int numSegments = 32; // Number of segments for both cone and scoop

    void Start()
    {
        GenerateIceCreamConeMesh();
    }

    void GenerateIceCreamConeMesh()
    {
        Mesh iceCreamMesh = new Mesh();
        iceCreamMesh.name = "IceCreamCone";

        int totalVertices = (numSegments + 1) * 3;
        Vector3[] vertices = new Vector3[totalVertices];
        int[] triangles = new int[numSegments * 6 * 2]; // Double the triangles for both cone and scoop

        // Generate vertices for the cone
        for (int i = 0; i <= numSegments; i++)
        {
            float u = (float)i / numSegments * 2 * Mathf.PI;
            float x = Mathf.Cos(u) * coneRadius;
            float z = Mathf.Sin(u) * coneRadius;

            vertices[i] = new Vector3(x, 0, z);
            vertices[i + numSegments + 1] = new Vector3(0, coneHeight, 0);
        }

        // Generate triangles for the cone
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

        // Generate vertices for the scoop
        for (int i = 0; i <= numSegments; i++)
        {
            float u = (float)i / numSegments * 2 * Mathf.PI;
            float x = Mathf.Cos(u) * scoopRadius;
            float z = Mathf.Sin(u) * scoopRadius;

            vertices[i + numSegments * 2 + 2] = new Vector3(x, coneHeight + scoopHeight, z);
        }

        // Generate triangles for the scoop
        for (int i = 0; i < numSegments; i++)
        {
            int bottomLeft = i + numSegments * 2 + 2;
            int bottomRight = i + numSegments * 2 + 3;
            int topVertex = numSegments * 2 + 2;
            int topRight = (i + 1) % numSegments + numSegments * 2 + 2;

            triangles[i * 6 + numSegments * 6] = bottomLeft;
            triangles[i * 6 + 1 + numSegments * 6] = topVertex;
            triangles[i * 6 + 2 + numSegments * 6] = bottomRight;
            triangles[i * 6 + 3 + numSegments * 6] = bottomRight;
            triangles[i * 6 + 4 + numSegments * 6] = topVertex;
            triangles[i * 6 + 5 + numSegments * 6] = topRight;
        }

        // Assign vertices and triangles to the mesh
        iceCreamMesh.vertices = vertices;
        iceCreamMesh.triangles = triangles;

        // Calculate normals (for shading)
        iceCreamMesh.RecalculateNormals();

        // Create a MeshFilter and MeshRenderer for the GameObject
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        // Assign the ice cream cone mesh to the MeshFilter
        meshFilter.mesh = iceCreamMesh;
    }
}
