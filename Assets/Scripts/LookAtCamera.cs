﻿using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour
{
    public GameObject camera;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camera.transform);
    }
}
