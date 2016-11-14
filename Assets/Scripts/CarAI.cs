using UnityEngine;
using System.Collections.Generic;
using System;

public class CarAI : MonoBehaviour, IDrive
{
    RPMCarController car;
    List<GameObject> targets = new List<GameObject>();

    float gas, brake, steer;
    public float requiredVelocity = 5f;

    Vector3 midPoint;

    void Start()
    {
        targets.AddRange(GameObject.FindGameObjectsWithTag("Playable Entity"));
        targets.Remove(transform.parent.gameObject);
        //Debug.Log("              " + targets[0].name);


        car = GetComponentInParent<RPMCarController>();
    }

    public float Acceleration()
    {

        if (requiredVelocity > car.velocity)
        {
            Debug.Log("                     " + (1 - ((car.velocity) / requiredVelocity)) + " | " + requiredVelocity);
            return 1 - ((car.velocity) / requiredVelocity);
        }
        return 0.25f;
    }

    public float Brake()
    {
        if (requiredVelocity < car.velocity)
        {
            Debug.Log("                     " + car.velocity / requiredVelocity + " | " + requiredVelocity);
            return requiredVelocity / car.velocity;
        }
        return 0;
    }

    public float Steering()
    {
        float relativeAngle = Mathf.Abs(targets[0].transform.eulerAngles.y - transform.eulerAngles.y);
        float angleToTarget = Vector3.Angle(transform.forward, targets[0].transform.position - transform.position);
        float turningAngle = 0;

        //U-Turn
        if (relativeAngle > 170f && angleToTarget < 90f)
        {
            turningAngle = 0f;
        }
        else
        {
            float distanceToPoint = Vector3.Distance(transform.position, targets[0].transform.position);

            //v = s / t
            //v = Circle surface length / (remaining angle / max angle)
            requiredVelocity = ((distanceToPoint) * Mathf.PI) / (relativeAngle / car.maxAngle);
            
            turningAngle = 180f; 
        }

        //Vector3 directionToTarget = Vector3.Cross(transform.forward, transform.position - targets[0].transform.position);
        //
        //if (directionToTarget.y > 0f)
        //{
        //    angleToTarget *= -1;
        //}


        float turningFactor = turningAngle / 180;
        //Debug.Log("                     " + relativeAngle + " | " + angleToTarget + " - " + turningFactor);
        return turningFactor;
    }

    public float Direction()
    {
        float relativeDirection = Vector3.Cross(transform.forward, transform.position - targets[0].transform.position).y;
        if (relativeDirection > 0f)
        {
            return -1f;
        }
        return 1f;

    }

    public void UpdateUI()
    {
        return;
    }
}
