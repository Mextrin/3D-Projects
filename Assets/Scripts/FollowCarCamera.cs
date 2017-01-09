using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCarCamera : MonoBehaviour
{
    public Transform car;
    [SerializeField] float rotationSpeed;
    [SerializeField]
    bool autoRotate;

    // Use this for initialization
    void Start()
    {
        //car = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = car.position;
        if (autoRotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, car.rotation, rotationSpeed * Time.deltaTime);

        }
    }
}
