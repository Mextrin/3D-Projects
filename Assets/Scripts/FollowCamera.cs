using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform moveTarget;
    public Transform rotationTarget;
    [SerializeField] float rotationSpeed;
    public bool autoRotate;

    Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (moveTarget != null)
        {

            float distance = Vector3.Distance(transform.position, moveTarget.position);
            transform.position = Vector3.SmoothDamp(transform.position, moveTarget.position, ref velocity, 1 / distance);
            //transform.position = car.position;

            //Rotation
            if (autoRotate)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget.rotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
