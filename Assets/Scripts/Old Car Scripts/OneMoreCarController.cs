using UnityEngine;
using System.Collections;

public class OneMoreCarController : MonoBehaviour
{
    public WheelRolling[] wheelTorque = new WheelRolling[4];
    public float topSpeed, brakePower;
    public float accelerationForce;
    public float brakeForce;

    float acceleration;
    float brakes;

    Rigidbody rigidbody;

    public bool debugSpeed;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {

        acceleration = Input.GetAxis("Acceleration");
        brakes = Input.GetAxis("Brakes");

        accelerationForce = acceleration * topSpeed;
        brakeForce = brakes * brakePower;

        //outputForce = 0;
        //for (int i = 0; i < wheelTorque.Length; i++)
        //{
        //    outputForce += wheelTorque[i].spinTorque;
        //}

        wheelTorque[0].rollSpeed = accelerationForce;
        wheelTorque[1].rollSpeed = -accelerationForce;
        wheelTorque[2].rollSpeed = accelerationForce;
        wheelTorque[3].rollSpeed = -accelerationForce;

        rigidbody.AddForce(transform.forward * accelerationForce, ForceMode.Acceleration);

        if (debugSpeed)
        {
            Debug.Log("              " + (rigidbody.velocity.magnitude * 60 * 60) / 1000 + " km/h");

        }
    }
}
