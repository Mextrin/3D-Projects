using UnityEngine;
using System.Collections;

public class WheelRolling : MonoBehaviour
{
    public float spinRate;
    /*[HideInInspector]*/ public float spinTorque;
    /*[HideInInspector]*/ public float rollSpeed;
    /*[HideInInspector]*/ public float wheelRadius;
    public float currentAngle;

    // Use this for initialization
    void Start()
    {
        wheelRadius = GetComponent<SphereCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        spinTorque = spinRate / (2 * wheelRadius * Mathf.PI);
        transform.Rotate(-Vector3.forward * rollSpeed * Time.deltaTime);
        //transform.localEulerAngles = new Vector3(0f, -90f, transform.rotation.z);
    }
}
