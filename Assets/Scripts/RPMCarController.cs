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
    public AccelerationType driveType;
    public float currentTorque, maxTorqueAtRPM;

    public float maxRPM, idleRPM;
    public float currentRPM;

    public float gasInput, brakeInput;

    public float horsepower;
    public float acceleration;
    public float brakePower;
    public float velocity;

    public int currentGear;
    public float gearRRatio, gear1Ratio, gear2Ratio, gear3Ratio, gear4Ratio, gear5Ratio, gear6Ratio;
    public float finalDriveAxleRatio;
    float[] gear;

    public float maxAngle;
    public float currentAngle;
    public float steeringInput;

    //Unity components
    Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
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
            float currentRotation = transform.rotation.y;
            transform.eulerAngles = new Vector3(0, currentRotation, 0);
        }

        //Calculate RPM
        currentRPM = ((velocity / 0.35f) * gear[currentGear + 1] * finalDriveAxleRatio * 60) / (2 * Mathf.PI);

        if (currentRPM < idleRPM)
        {
            currentRPM = idleRPM;
        }
        else if (currentRPM > maxRPM)
        {
            currentRPM = maxRPM;
        }
        
        //RPM.fillAmount = ((CalculateTorque(maxRPM) / maxTorqueAtRPM)) * 0.75f;

        //Calculate resistance forces
        float totalDragResistance = -dragResistance * velocity * Mathf.Abs(velocity);
        float totalRollResistance = -rollResistance * velocity;

        //Set brake value
        float brake = Mathf.Abs(velocity) > 0.1f ? brakePower : 0;

        //Calculate acceleration

        //Power = Resistance force + velocity / 0.75
        horsepower = ((totalDragResistance + totalRollResistance) + (velocity + 1)) / 0.85f / 0.745699872f;

        //Torque = HP / (2 * PI * Rev/s)
        maxTorqueAtRPM = horsepower / (2 * Mathf.PI * (currentRPM / 60));

        //maxTorqueAtRPM = CalculateTorque(currentRPM);

        currentTorque = (gasInput * maxTorqueAtRPM) / gear[currentGear + 1];

        float driveForce = currentTorque /** gear[currentGear + 1]*/ * finalDriveAxleRatio * 0.7f;// / 0.33f;
        float outputForce = (driveForce - (Mathf.Lerp(0, brake, brakeInput))) + totalDragResistance + totalRollResistance;

        acceleration = outputForce / weight;
        velocity += acceleration;

        float wheelRollSpeed = ((currentTorque * gear[currentGear + 1]) / 60) * 360 * Mathf.Deg2Rad;


        //rigidbody.AddForce(transform.forward * outputForce, ForceMode.Force);
        transform.Translate(transform.forward * velocity * Time.deltaTime, Space.World);

        currentAngle = (maxAngle * steeringInput);
        transform.Rotate(transform.up * currentAngle * Time.deltaTime);

        //wheel[0].rollSpeed = driveTorque;

    }

    //float FindValue(float currentValue, float pointedValue, float peekPoint)
    //{
    //    //RPM to RPM by HP ----- Lerp(RPM, RPM, HP)
    //    //return currentRPM / HPAtRPM;

    //    float t = (currentValue / pointedValue);

    //    float ULerpedValue = (1.0f - t) * 0 + t * peekPoint;

    //    //Debug.Log("                 " + HPAtRPM * t);
    //    //(a + (b - a)) * t;
    //    //(0, HPAtRPM, Mathf.InverseLerp(0, ));
    //    return ULerpedValue;
    //}

    //Torque in N.m
    float CalculateTorque(float RPM)
    {
        //return ((63.025f * currentHP) / RPM) * 1.3559179f;

        //h = (t * r) / 5252
        return horsepower * (5252 / RPM);
    
    }

}