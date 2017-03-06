using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerInput : MonoBehaviour, IDrive, IWalk
{
    public float Acceleration(WheelColliderController vehicle)
    {
        float input = Input.GetAxis("Gas");
        return input;
    }

    public float Brake(WheelColliderController vehicle)
    {
        float input = Input.GetAxis("Brake");
        return input;
    }
    
    public float Steering(WheelColliderController vehicle)
    {
        return Input.GetAxis("Steering");
    }

    public float Horizontal(Entity entity)
    {
        float input = Input.GetAxis("Horizontal");
        return input;
    }

    public float Vertical(Entity entity)
    {
        float input = Input.GetAxis("Vertical");
        return input;
    }
}
