using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMenu : MonoBehaviour
{
    [SerializeField] float smoothTime;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationConstant;
    public Transform targetPos, targetRot;
    Vector3 velocity = Vector3.zero;
    float startDistance;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (targetPos != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPos.position, ref velocity, smoothTime);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot.rotation, rotationConstant * Time.deltaTime);
        }
    }

    public void SetFullTransform(Transform position)
    {
        targetPos = position;
        targetRot = position;
    }

    public void SetRotation(Transform rotation)
    {
        targetRot = rotation;
    }

    public void SetPosition(Transform position)
    {
        targetPos = position;
    }
}
