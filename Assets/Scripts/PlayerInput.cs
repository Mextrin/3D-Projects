using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerInput : MonoBehaviour, IDrive
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
}
