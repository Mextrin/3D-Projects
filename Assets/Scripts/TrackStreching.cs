using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class TrackStreching : MonoBehaviour
{

    public List<GameObject> tracks = new List<GameObject>();
    public Transform B;
    Vector3 prevAPos, prevBPos;

    public GameObject track;
    public Transform cube;
    public float z;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Mesh defaultMesh = track.GetComponent<MeshFilter>().sharedMesh;
        float defaultMeshLength = defaultMesh.bounds.size.z;

        Vector3 center = Vector3.Lerp(transform.position, B.position, 0.5f);
        cube.position = center;
        float distance = Vector3.Distance(transform.position, B.position);
        float neededMeshes = Mathf.Ceil(distance / defaultMeshLength);
        z = neededMeshes;
        Debug.Log("           " + neededMeshes);

        if (prevAPos == null)
        {
            prevAPos = transform.position;
        }
        if (prevBPos == null)
        {
            prevBPos = B.position;
        }

        float meshParts = neededMeshes * 2 - 1;
        if (transform.childCount != neededMeshes)
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
                tracks.RemoveAt(0);

            }

            if (transform.childCount == 0)
            {
                for (int i = 0; i < neededMeshes; i++)
                {
                    float lerpValue = (i * 2 + 1) / (meshParts + 1);
                    Vector3 spawnPosition = Vector3.Lerp(transform.position, B.position, lerpValue);
                    GameObject rails = (GameObject)Instantiate(track, spawnPosition, transform.rotation);
                    tracks.Add(rails);
                    rails.transform.parent = transform;
                }

            }
        }
        if (prevAPos != transform.position || prevBPos != B.position)
        {
            prevAPos = transform.position;
            prevBPos = B.position;


            for (int i = 0; i < neededMeshes; i++)
            {
                float lerpValue = (i * 2 + 1) / (meshParts + 1);
                tracks[i].transform.position = Vector3.Lerp(transform.position, B.position, lerpValue);
            }


            for (int a = 0; a < tracks.Count; a++)
            {
                Mesh trackMesh = tracks[a].GetComponent<MeshFilter>().sharedMesh;
                Vector3[] vertices = trackMesh.vertices;


                float startLerp = (a * 2) / (meshParts + 1);
                float endLerp = (a * 2 + 2) / (meshParts + 1);
                Vector3 startPos = Vector3.Lerp(transform.position, B.position, startLerp);
                Vector3 endPos = Vector3.Lerp(transform.position, B.position, endLerp);

                for (int b = 0; b < vertices.Length; b++)
                {
                    float currentLerp = b / vertices.Length;
                    vertices[b].z += startLerp;
                }

                defaultMesh.vertices = vertices;
                defaultMesh.RecalculateBounds();
            }
        }

    }
}
