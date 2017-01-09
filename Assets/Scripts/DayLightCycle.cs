using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLightCycle : MonoBehaviour
{
    private enum TimeUnit
    {
        Seconds, Minutes, Hours
    }
    [SerializeField]
    TimeUnit time;

    [SerializeField]
    float speed;

    float rotationSpeed;

    void Start()
    {
        switch (time)
        {
            case TimeUnit.Seconds:
                rotationSpeed = 360 / speed;
                break;

            case TimeUnit.Minutes:
                rotationSpeed = 360 / (60 * speed);
                break;

            case TimeUnit.Hours:
                rotationSpeed = 360 / (60 * 60 * speed);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.Rotate(Vector3.right * (rotationSpeed) * Time.deltaTime);
        }
    }
}
