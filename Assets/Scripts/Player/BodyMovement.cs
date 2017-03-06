using UnityEngine;
using System.Collections;
using System;

public class BodyMovement : Entity
{
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float jumpPower;
    [SerializeField]
    float rotationSpeed;

    CameraMovement camera;
    CharacterController controller;
    IWalk input;
    Transform body;

    float rotationX, rotationY;
    

    void Start()
    {
        camera = GetComponentInChildren<CameraMovement>();
        controller = GetComponent<CharacterController>();
        body = transform.GetChild(0);
        input = GetComponentInChildren<IWalk>();
    }

    void Update()
    {
        float moveHorizontal = input.Horizontal(this);
        float moveVertical = input.Vertical(this);

        if (camera != null)
        {

            Vector3 cameraRot = camera.transform.eulerAngles;
            cameraRot.x = 0f;
            cameraRot.z = 0f;

            body.rotation = Quaternion.Slerp(body.rotation, Quaternion.Euler(cameraRot), rotationSpeed  * Time.deltaTime);
        }

        if (controller != null)
        {
            //float walkConstant;
            //float moveH = Mathf.Abs(moveHorizontal);
            //float moveV = Mathf.Abs(moveVertical);
            //
            //if (moveV > moveH)
            //{
            //    walkConstant = moveV;
            //}
            //else
            //{
            //    walkConstant = moveH;
            //}

            //controller.Move(body.forward * walkConstant * maxSpeed * Time.deltaTime);
            controller.Move(-body.right * moveHorizontal * maxSpeed * Time.deltaTime);
            controller.Move(body.forward * moveVertical * maxSpeed * Time.deltaTime);
        }

        //transform.Translate(Vector3.forward * moveVertical * Time.deltaTime);
        //transform.Translate(-Vector3.right * moveHorizontal * Time.deltaTime);

    }
}
