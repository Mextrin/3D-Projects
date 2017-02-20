using UnityEngine;
using System.Collections;

public class BodyMovement : MonoBehaviour
{
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float jumpPower;

    CharacterController controller;
    Transform cameraPoint;

    float rotationX, rotationY;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraPoint = GetComponentInChildren<CameraMovement>().transform;
    }

    void Update()
    {
        if (controller != null)
        {
            float moveHorizontal = Input.GetAxis("Horizontal") * maxSpeed;
            float moveVertical = Input.GetAxis("Vertical") * maxSpeed;


            controller.Move(transform.forward * moveVertical * Time.deltaTime);
            controller.Move(-transform.right * moveHorizontal * Time.deltaTime);
        }
        //transform.Translate(Vector3.forward * moveVertical * Time.deltaTime);
        //transform.Translate(-Vector3.right * moveHorizontal * Time.deltaTime);

    }
}
