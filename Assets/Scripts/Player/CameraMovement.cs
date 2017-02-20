using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float rotationX, rotationY;
    [SerializeField]
    float minRotationLimitY, maxRotationLimitY;

    [SerializeField]
    float sensitivity;

    //Auto rotate tracking
    [SerializeField]
    float inactiveTime;
    float time;

    Transform camera;
    FollowCamera cameraScript;

    // Use this for initialization
    void Start()
    {
        camera = transform.GetChild(0);
        cameraScript = GetComponent<FollowCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        float prevX = rotationX;
        float prevY = rotationY;

        rotationY = Input.GetAxis("Mouse Y") * sensitivity;
        rotationX = Input.GetAxis("Mouse X") * sensitivity;

        float minLimitY;
        float maxLimitY;
        if (!cameraScript.autoRotate)
        {
            minLimitY = (minRotationLimitY < 0) ? 360 + minRotationLimitY : minRotationLimitY;
            maxLimitY = (maxRotationLimitY > 360) ? maxRotationLimitY - 360 : maxRotationLimitY;

            if (minLimitY > maxLimitY)
            {
                if (transform.eulerAngles.x < minLimitY && transform.eulerAngles.x > maxLimitY)
                {
                    if (Mathf.Abs(transform.eulerAngles.x - minLimitY) < Mathf.Abs(transform.eulerAngles.x - maxLimitY))
                    {
                        if (rotationY < 0)
                        {
                            rotationY = 0;
                            transform.eulerAngles = new Vector3(minLimitY, transform.eulerAngles.y, transform.eulerAngles.z);
                        }
                    }
                    else
                    {
                        if (rotationY > 0)
                        {
                            rotationY = 0;
                            transform.eulerAngles = new Vector3(maxLimitY, transform.eulerAngles.y, transform.eulerAngles.z);
                        }
                    }
                }
            }

            transform.eulerAngles += new Vector3(rotationY, rotationX, 0);   
            //transform.Rotate(rotationY, rotationX, 0, Space.World);

        }

        if (rotationY == prevY && rotationX == prevX)
        { 
            time += Time.deltaTime;
            if (time >= inactiveTime)
            {
                cameraScript.autoRotate = true;
            }
        }
        else
        {
            time = 0;
            if (cameraScript.autoRotate)
            {
                cameraScript.autoRotate = false;
            }
        }
    }
}
