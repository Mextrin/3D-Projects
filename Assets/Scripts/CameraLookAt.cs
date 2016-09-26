using UnityEngine;
using System.Collections;

public class CameraLookAt : MonoBehaviour
{
    public float axis;

	void Update ()
    {
        transform.Rotate(Vector3.up * axis * Time.deltaTime);
	}
}
