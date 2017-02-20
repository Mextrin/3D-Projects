using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public int speed, jumpPower, YLimit;
    float forwardMovement, rightMovement;
    public float rotationX, rotationY;
    Rigidbody gravity;
    bool isGrounded;
    public Shooter weapon;
    public GameObject camera;
    public Vector3 direction = new Vector3();

    // Use this for initialization
    void Start()
    {
        gravity = GetComponent<Rigidbody>();
        Screen.lockCursor = true;
    }

    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) ^ Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.W))
                forwardMovement = 1;
            else if (Input.GetKey(KeyCode.S))
                forwardMovement = -1;

        }
        else
            forwardMovement = 0;

        if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
                rightMovement = -1;
            else if (Input.GetKey(KeyCode.D)) 
                rightMovement = 1;
        }
        else
            rightMovement = 0;

        if (Input.GetKey(KeyCode.Space) && isGrounded)
            direction.y = jumpPower;
        else if (isGrounded)
            direction.y = 0;
        else
            direction.y -= gravity.mass / 100;
            
        //gravity.AddForce(Vector3.up * jumpPower);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            weapon.shoot = true;
        }
        else
            weapon.shoot = false;

        //Rotation
        rotationX += Input.GetAxis("Mouse X");
        rotationY += Input.GetAxis("Mouse Y");

        rotationY = Mathf.Clamp(rotationY, -YLimit, YLimit);

        transform.localEulerAngles = new Vector3(0, rotationX, 0);
        camera.transform.localEulerAngles = new Vector3(rotationY, 0, 0);

        //Movement
        gravity.velocity = new Vector3(0, direction.y, 0);
        transform.Translate(Vector3.forward * forwardMovement * speed * Time.deltaTime);
        transform.Translate(Vector3.right * rightMovement * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
            isGrounded = true;
    }
    
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }
}
