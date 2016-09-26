using UnityEngine;
using System.Collections;

public class WheelRolling : MonoBehaviour
{
    /*[HideInInspector]*/ public float rollSpeed;
    /*[HideInInspector]*/ public float wheelRadius;
    public bool isGrounded;
    public float currentAngle, startAngle;
    public MeshCollider carCollider;

    // Use this for initialization
    void Start()
    {
        wheelRadius = GetComponent<SphereCollider>().radius;
        Physics.IgnoreCollision(GetComponent<SphereCollider>(), carCollider, true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-Vector3.forward * rollSpeed * Mathf.PI * 20 * Time.deltaTime);
        transform.localRotation = Quaternion.AngleAxis(currentAngle + startAngle, Vector3.up);
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
