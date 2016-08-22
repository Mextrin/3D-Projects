using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour
{
    public float torqueSpeed;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddTorque(Vector3.forward * torqueSpeed);
            rb.AddTorque(Vector3.up * torqueSpeed);
        }

    }
}
