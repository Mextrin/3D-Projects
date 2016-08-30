using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class MeshBending : MonoBehaviour
{
    public bool update;
    public float rotation;
    Mesh mesh;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        if (update)
        {

            //vertices = mesh.vertices;

            float meshWidth = mesh.bounds.size.x;
            for (var i = 0; i < vertices.Length; i++)
            {
                float formPos = Mathf.Lerp(meshWidth / 2, -meshWidth / 2, 0);
                float zeroPos = vertices[i].x + formPos;
                float rotateValue = (-rotation / 2) * (zeroPos / meshWidth);

                zeroPos -= 2 * vertices[i].y * Mathf.Cos((90 - rotateValue) * Mathf.Deg2Rad);

                vertices[i].y += zeroPos * Mathf.Sin(rotateValue * Mathf.Deg2Rad);
                vertices[i].x = zeroPos * Mathf.Cos(rotateValue * Mathf.Deg2Rad) - formPos;
            }

            update = !update;
        }

        //float meshWidth = mesh.bounds.size.z;
        //for (int i = 0; i < verticies.Length; i++)
        //{
        //    float bendPosition = verticies[i].z + (meshWidth / 2);
        //    //Debug.Log(verticies.Length + "             " + i);
        //    verticies[i].z += rotation * i * Mathf.Cos(bendPosition);
        //}

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }
}
