using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMenu : MonoBehaviour
{
    [SerializeField] float smoothTime;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationConstant;
    public Transform toMoveTo;
    Vector3 velocity = Vector3.zero;
    float startDistance;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (toMoveTo != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, toMoveTo.position, ref velocity, smoothTime);

            transform.rotation = Quaternion.Slerp(transform.rotation, toMoveTo.rotation, rotationConstant * Time.deltaTime);
        }
    }
}
