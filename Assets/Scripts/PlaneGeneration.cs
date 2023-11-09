using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlaneGeneration : MonoBehaviour
{
    [SerializeField] private int xSize = 50;
    [SerializeField] private int zSize = 50;
    [SerializeField] private float pointStep = 0.2f;

    [SerializeField] private Vector3[] vertices;
    private int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        GenerateVerts();
        GeneratePlane();

    }

    private void GeneratePlane()
    {
        Mesh generatedPlane = new Mesh();

        generatedPlane.vertices = vertices;
        generatedPlane.triangles = triangles;
        generatedPlane.RecalculateNormals();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        // Assign the donut mesh to the MeshFilter
        meshFilter.mesh = generatedPlane;
        //meshRenderer.material = GraphicsSettings.currentRenderPipeline.defaultMaterial;
    }

    private void GenerateVerts()
    {
        vertices = new Vector3[xSize * zSize];
        triangles = new int[(xSize-1)*(zSize-1)*6];
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z<zSize; z++)
            {
                vertices[x * zSize + z] = new Vector3(x * pointStep, Mathf.Sin(z)*pointStep, z * pointStep);
            }
        }
        int trianglesCount = 0;
        for (int x = 0; x < xSize - 1; x++)
        {
            for (int z = 0; z < zSize - 1; z++)
            {
                triangles[trianglesCount] = (x + 1) * zSize + z;
                triangles[trianglesCount + 1] = x * zSize + z;
                triangles[trianglesCount + 2] = x * zSize + z + 1;
                triangles[trianglesCount + 3] = x * zSize + z + 1;
                triangles[trianglesCount + 4] = (x + 1) * zSize + z + 1;
                triangles[trianglesCount + 5] = (x + 1) * zSize + z;
                trianglesCount += 6;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;
        Gizmos.color = Color.red;
        foreach (Vector3 v in vertices)
            Gizmos.DrawSphere(v, pointStep * 0.25f);
    }

}
