using UnityEngine;
using System.Collections;

public class AnotherCarController : MonoBehaviour
{
    public AccelerationType driveType;

    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public GameObject[] wheels = new GameObject[4];

    [Range(0f, 1f)]
    public float acceleration;

    public float brakePower;
    [Range(0, 1f)] public float brakeBalance;
    float frontBrake, rearBrake;
    float torque, brake;
    public float RPM;

    // Update is called once per frame
    void Update()
    {
        UpdateWheelVisuals();

        UpdateVisuals();

        if (Input.GetKey(KeyCode.UpArrow))
        {
            torque = RPM;
        }
        else
        {
            torque = 0;
        }

        torque = RPM * acceleration;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            brake = brakePower;
        }
        else
        {
            brake = 0;
        }
    }

    void UpdateWheelVisuals()
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 position;
            wheelColliders[i].GetWorldPose(out position, out quat);
            wheels[i].transform.position = position;
            wheels[i].transform.rotation = quat;
        }

    }

    void GearChange()
    {

    }

    void UpdateVisuals()
    {
        float thrust;
        switch (driveType)
        {
            case AccelerationType.FrontWheelDrive:
                thrust = torque / 4f;
                for (int i = 0; i < 2; i++)
                {
                    wheelColliders[i].motorTorque = thrust;
                }
                break;

            case AccelerationType.RearWheelDrive:
                thrust = torque / 2f;
                for (int i = 2; i < 4; i++)
                {
                    wheelColliders[i].motorTorque = thrust;
                }
                break;

            case AccelerationType.FourWheelDrive:
                thrust = torque / 2f;
                for (int i = 0; i < 4; i++)
                {
                    wheelColliders[i].motorTorque = thrust;
                }
                break;
        }

        for (int i = 0; i < 2; i++)
        {
            wheelColliders[i].brakeTorque = brake * (1 - brakeBalance);
        }

        for (int i = 2; i < 4; i++)
        {
            wheelColliders[i].brakeTorque = brake * brakeBalance;
        }
    }
}
