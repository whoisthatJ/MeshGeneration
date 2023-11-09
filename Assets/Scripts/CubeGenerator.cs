using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;    
    private Vector3[] vertices;
    private int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        GenerateVerts();
        GenerateShape();

    }

    private void GenerateShape()
    {
        Mesh generatedCube = new Mesh();

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshFilter.mesh = generatedCube;
        generatedCube.Clear();
        generatedCube.vertices = vertices;
        generatedCube.triangles = triangles;

        //meshRenderer.material = defaultMaterial;
        generatedCube.RecalculateNormals();
        //generatedPlane.RecalculateBounds();

        // Assign the donut mesh to the MeshFilter
    }

    private void GenerateVerts()
    {
        vertices = new Vector3[8];
        vertices[0] = Vector3.zero; 
        vertices[1] = Vector3.right;
        vertices[2] = Vector3.up;
        vertices[3] = Vector3.right + Vector3.up;
        vertices[4] = Vector3.forward;
        vertices[5] = Vector3.right + Vector3.forward;
        vertices[6] = Vector3.up + Vector3.forward;
        vertices[7] = Vector3.one;

        int[] tris = { 0, 2, 1, 2, 3, 1,
                       1, 3, 5, 3, 7, 5,
                       0, 1, 4, 1, 5, 4,
                       2, 6, 3, 6, 7, 3,
                       2, 0, 4, 2, 4, 6, 
                       6, 4, 5, 5, 7, 6
        };
        triangles = tris;

    }
    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;
        Gizmos.color = Color.red;
        //Gizmos.draw
        foreach (Vector3 v in vertices)
            Gizmos.DrawSphere(v, 0.1f);
    }
}
