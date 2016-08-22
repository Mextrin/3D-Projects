using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour
{

    public float weight;
    public bool isGrounded;

    Vector3 direction = new Vector3();

    void FixedUpdate()
    {
        if (!isGrounded)
            direction.y -= weight;
        else
            direction.y = 0;
    }

    void Update()
    {
        direction = new Vector3(0, direction.y, 0);
    }
}
