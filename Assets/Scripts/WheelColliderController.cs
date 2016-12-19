using UnityEngine;
using System.Collections;

public class WheelColliderController : MonoBehaviour
{
    //Input
    IDrive[] controller;
    int currentController;

    //CarInfo
    WheelCollider[] wheels;

    public AccelerationType driveType;
    public float engineTorque, maxTorqueAtRPM;

    public float maxRPM, idleRPM;
    public float currentRPM;

    float gasInput, brakeInput;
    public float velocity;
    public float horsepower;
    public float brakePower, handbrakePower;

    public int currentGear;
    public float gearRRatio, gear1Ratio, gear2Ratio, gear3Ratio, gear4Ratio, gear5Ratio, gear6Ratio;
    public float finalDriveAxleRatio;
    float[] gear;

    public float maxAngle;
    float steeringInput;

    Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        controller = GetComponentsInChildren<IDrive>();
        rigidbody = GetComponent<Rigidbody>();
        wheels = GetComponentsInChildren<WheelCollider>();

        gear = new float[]
            {
                gearRRatio,
                0,
                gear1Ratio,
                gear2Ratio,
                gear3Ratio,
                gear4Ratio,
                gear5Ratio,
                gear6Ratio,
            };
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.Length > 0)
        {
            controller[currentController].UpdateUI();
            steeringInput = controller[currentController].Steering();
            gasInput = controller[currentController].Acceleration();
            brakeInput = controller[currentController].Brake();
        }

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if (currentGear < 6)
        //        currentGear++;
        //}
        //
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    if (currentGear > -1)
        //        currentGear--;
        //}

        float RPMSum = 0;
        for (int i = 0; i < wheels.Length; i++)
        {
            RPMSum += wheels[i].rpm;
        }
        currentRPM = ((RPMSum * Mathf.Deg2Rad) / wheels.Length) * gear[currentGear + 1] * finalDriveAxleRatio;

        if (currentRPM < idleRPM)
        {
            currentRPM = idleRPM;
        }

        if (currentRPM > maxRPM)
        {
            currentRPM = 100000f;
        }

        float brake = brakeInput * brakePower;

        maxTorqueAtRPM = (5252 * horsepower) / currentRPM;
        horsepower = (maxTorqueAtRPM * currentRPM) / 5252;
        engineTorque = (gasInput * maxTorqueAtRPM);

        float driveTorque = (engineTorque * (gear[currentGear + 1] * finalDriveAxleRatio)) * 0.7f;
        velocity = rigidbody.velocity.magnitude;












        for (int i = 0; i < 4; i++)
        {
            Transform wheel = wheels[i].transform.GetChild(0);

            Vector3 pos;
            Quaternion rot;
            wheels[i].GetWorldPose(out pos, out rot);

            wheel.transform.position = pos;
            wheel.transform.rotation = rot;
        }

        wheels[0].motorTorque = driveTorque;
        wheels[1].motorTorque = driveTorque;
        wheels[2].motorTorque = driveTorque;
        wheels[3].motorTorque = driveTorque;

        wheels[0].brakeTorque = brake;
        wheels[1].brakeTorque = brake;

        wheels[0].steerAngle = steeringInput * maxAngle;
        wheels[1].steerAngle = steeringInput * maxAngle;
    }

}
