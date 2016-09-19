using UnityEngine;
using System.Collections;

public class CameraLookAt : MonoBehaviour
{
    public Transform target;
    Vector3 velocity = Vector3.zero;
    public float moveTime, rotateTime;
	


	void Update ()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, moveTime);

        var direction = Quaternion.LookRotation(target.parent.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, direction, rotateTime);
	}
}
