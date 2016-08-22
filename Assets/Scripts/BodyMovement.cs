using UnityEngine;
using System.Collections;

public class BodyMovement : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public float minRotationLimitY, maxRotationLimitY;

    public bool isGrounded;

    public Rigidbody rigidbody;

    public Transform camera;
    public Transform head;

    float rotationX, rotationY;

    void Start()
    {
        head = transform.FindChild("Head");
        camera = transform.FindChild("Camera");
        rigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {        
        //Rotating
        rotationX += Input.GetAxis("Mouse X");
        rotationY += Input.GetAxis("Mouse Y");

        if (rotationY < -minRotationLimitY)
            rotationY = -minRotationLimitY;

        else if (rotationY > maxRotationLimitY)
            rotationY = maxRotationLimitY;

        if (Input.GetKey(KeyCode.W))
        {

        }

        transform.localEulerAngles = new Vector3(0, rotationX, 0);
        head.localEulerAngles = new Vector3(rotationY, 0, 0);
        head.localEulerAngles = new Vector3(rotationY, 0, 0);
    }
     void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Grounded")) isGrounded = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Grounded")) isGrounded = false;
    }
}
