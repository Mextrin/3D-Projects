using UnityEngine;
using System.Collections;
using System;

public class PlayerInput : MonoBehaviour, IDrive
{
    public float Acceleration()
    {
        return Input.GetAxis("Gas");
    }

    public float Brake()
    {
        return Input.GetAxis("Brake");
    }

    public float Steering()
    {
        return Input.GetAxis("Steering");
    }
}
