using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    CameraMovement camera;
    [SerializeField] float adjustSpeed = 5f;
    // Use this for initialization
    void Start()
    {
        camera = transform.parent.GetComponentInChildren<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (camera != null)
        {
            Vector3 cameraRot = camera.transform.eulerAngles;
            cameraRot.x = 0f;
            cameraRot.z = 0f;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(cameraRot), adjustSpeed * Time.deltaTime);
        }
    }
}
