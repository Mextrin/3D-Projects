using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class MeshBending : MonoBehaviour
{
    public bool update;
    public float rotation;
    Mesh mesh;
    List<Vector3> originalVector = new List<Vector3>();
    float originalWidth;
    public Transform a, b;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mesh = GetComponent<MeshFilter>().sharedMesh;
        Vector3[] vertices;
        float meshWidth;

        if (originalVector.Count > 0)
        {
            vertices = originalVector.ToArray();
            //meshWidth = originalWidth;
        }
        else
        {
            vertices = mesh.vertices;

            //originalVector.Clear();
            foreach (Vector3 vertice in vertices)
            {
                originalVector.Add(vertice);
            }
            //originalWidth = meshWidth;
        }

        meshWidth = mesh.bounds.size.z;
        for (var i = 0; i < vertices.Length; i++)
        {
            float bendStartPos = vertices[i].z + meshWidth / 2;
            float rotateValue = (-rotation / 2) * (bendStartPos / meshWidth);

            bendStartPos -= 2 * vertices[i].x * Mathf.Cos((90 - rotateValue) * Mathf.Deg2Rad);

            vertices[i].x += bendStartPos * Mathf.Sin(rotateValue * Mathf.Deg2Rad);
            vertices[i].z = bendStartPos * Mathf.Cos(rotateValue * Mathf.Deg2Rad) - (meshWidth / 2);
        }


        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }
}
