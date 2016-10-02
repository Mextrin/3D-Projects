using UnityEngine;
using System.Collections;

public class WheelRolling : MonoBehaviour
{
    /*[HideInInspector]*/ public float rollSpeed;
    /*[HideInInspector]*/ public float wheelRadius;
    public bool isGrounded;
    public float currentAngle, startAngle;
    public MeshCollider carCollider;

    float previousRotation;
    public float rotationSpeed;

    // Use this for initialization
    void Start()
    {
        wheelRadius = GetComponent<SphereCollider>().radius;
        Physics.IgnoreCollision(GetComponent<SphereCollider>(), carCollider, true);
    }

    // Update is called once per frame
    void Update()
    {
        float output = rollSpeed * Mathf.PI * 2 * Mathf.Rad2Deg;
        transform.Rotate(-Vector3.forward * output * Time.deltaTime);
        //transform.localRotation = Quaternion.AngleAxis(currentAngle + startAngle, Vector3.up);


    }

    

    void OnTriggerStay(Collider collision)
    {
        if (!isGrounded)
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if(isGrounded)
        {
            isGrounded = false;
        }
    }
}
