using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RPMCarController : MonoBehaviour
{
    //Input control
    IDrive[] controller;
    int currentController;

    //Car details
    public float weight;
    public float dragResistance, rollResistance;
    public WheelRolling[] wheel = new WheelRolling[4];
    public ParticleSystem[] skidSmoke;
    public AccelerationType driveType;
    public float engineTorque, maxTorqueAtRPM;

    public float maxRPM, idleRPM;
    public float currentRPM;

    public float gasInput, brakeInput;

    public float horsepower;
    public float acceleration;
    public float brakePower;
    public float velocity;
    public float wheelAngularVelocity;

    public int currentGear;
    public float gearRRatio, gear1Ratio, gear2Ratio, gear3Ratio, gear4Ratio, gear5Ratio, gear6Ratio;
    public float finalDriveAxleRatio;
    float[] gear;

    public float maxAngle;
    public float wheelAngle;
    public float steeringInput;

    float wheelBase;
    public Transform centerofGravity;
    Transform forcesOnCar;

    //Unity components
    Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        forcesOnCar = transform.parent;
        controller = GetComponentsInChildren<IDrive>();

        //Torque in lb.ft
        rigidbody = GetComponent<Rigidbody>();

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

        wheelBase = Vector3.Distance(wheel[0].transform.position, wheel[3].transform.position);
    }

    void Update()
    {

        if (controller.Length > 0)
        {
            controller[currentController].UpdateUI();
            steeringInput = controller[currentController].Steering();
            gasInput = controller[currentController].Acceleration();
            brakeInput = controller[currentController].Brake();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentGear < 6)
                currentGear++;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentGear > -1)
                currentGear--;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Vector3 currentRotation = transform.eulerAngles;
            transform.eulerAngles = new Vector3(0, currentRotation.y + 2f, currentRotation.z);
        }

        //Calculate RPM
        currentRPM = (wheelAngularVelocity * gear[currentGear + 1] * finalDriveAxleRatio * 60);

        if (currentRPM < idleRPM)
        {
            currentRPM = idleRPM;
        }
        else if (currentRPM > maxRPM)
        {
            currentRPM = maxRPM;
        }

        //Calculate resistance forces
        float dragResistanceForce = -dragResistance * velocity * Mathf.Abs(velocity);
        float rollResistanceForce = -rollResistance * velocity;

        //Set brake value
        float maxTractionForceF;
        float maxTractionForceR;

        float b = Mathf.Abs(centerofGravity.position.z - wheel[0].transform.position.z); //Front wheels
        float c = Mathf.Abs(centerofGravity.position.z - wheel[3].transform.position.z); //Back wheels
        float h = Mathf.Abs(centerofGravity.position.y - (wheel[0].transform.position.y - 0.35f));

        if (velocity < 0.1f)
        {
            //W = (a / wheelBase) * W
            //FMax = 1.2f * ((a / wheelBase) * W)
            maxTractionForceF = 1.2f * ((c / wheelBase) * weight * 9.8f);
            maxTractionForceR = 1.2f * ((b / wheelBase) * weight * 9.8f);
        }
        else
        {
            //FMax = 1.2f * ((a / wheelBase) * weight * 9.8f +- (1.6f / wheelBase) * weight * acceleration)
            maxTractionForceF = 1.2f * (c / wheelBase) * weight * 9.8f - (h / wheelBase) * weight * acceleration;
            maxTractionForceR = 1.2f * (b / wheelBase) * weight * 9.8f + (h / wheelBase) * weight * acceleration;
        }

        maxTorqueAtRPM = (5252 * horsepower) / currentRPM;
        horsepower = (maxTorqueAtRPM * currentRPM) / 5252;
        engineTorque = (Mathf.Sqrt(gasInput) * maxTorqueAtRPM);
        float brakeTorque = (Mathf.Abs(velocity) > 0.25f ? brakePower : 0) * brakeInput;

        float driveTorque = (engineTorque / (gear[currentGear + 1] * finalDriveAxleRatio)) * 0.7f;

        //P = T * 2 * pi * rps
        //rps = (T * 2 * pi) / P
        float wheelRollSpeed = velocity / (2 * Mathf.PI * 0.35f); //Always rolling


        wheelAngularVelocity = (((driveTorque - brakeTorque) / horsepower) * 2 * Mathf.PI) + wheelRollSpeed;
        //float slipRatio = (wheelAngularVelocity * (0.35f - velocity)) / Mathf.Abs(velocity + 0.1f);
        float slipRatio = wheelRollSpeed / wheelAngularVelocity;
        //float slipRatio = ((wheelAngularVelocity * 0.35f) - wheelRollSpeed) / wheelRollSpeed != 0 ? Mathf.Abs(wheelRollSpeed) : 1f;

        //Debug.Log("                      " + (1 - slipRatio) + " %");
        float totalForce = (((driveTorque - brakeTorque) / 0.35f) * (slipRatio > 0 ? slipRatio + 0.1f : 1)) + dragResistanceForce + rollResistanceForce;

        //wheelAngularVelocity = driveTorque - (totalForce * 0.35f);
        


        acceleration = totalForce / weight;
        velocity += acceleration;

        //rigidbody.AddForce(transform.forward * outputForce, ForceMode.Force);
        forcesOnCar.Translate(transform.forward * velocity * Time.deltaTime, Space.World);


        for (int i = 0; i < skidSmoke.Length; i++)
        {
            skidSmoke[i].emissionRate = 250f * (0.8f - slipRatio);
        }


        //Input
        wheelAngle = (maxAngle * steeringInput);


        //Basic steering
        float raduisToMidPoint = wheelBase / (wheelAngle);
        float rotationMovement = velocity / raduisToMidPoint;

        //Advanced Steering
        float tireHeading = forcesOnCar.eulerAngles.y + wheelAngle;
        float alpha = transform.eulerAngles.y / tireHeading; //Angle

        Debug.Log("                " + tireHeading + " " + transform.eulerAngles.y + " >> " + alpha); 

        forcesOnCar.Rotate(transform.up * rotationMovement * Time.deltaTime);

        wheel[0].rollSpeed = wheelRollSpeed;
        wheel[1].rollSpeed = -wheelRollSpeed;
        wheel[2].rollSpeed = wheelAngularVelocity;
        wheel[3].rollSpeed = -wheelAngularVelocity;

        wheel[0].currentAngle = wheelAngle;
        wheel[1].currentAngle = wheelAngle;
    }

}