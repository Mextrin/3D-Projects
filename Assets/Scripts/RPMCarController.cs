using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RPMCarController : MonoBehaviour
{
    //Car details
    public float weight;
    public float dragResistance, rollResistance;
    public WheelRolling[] wheel = new WheelRolling[4];
    public AccelerationType driveType;
    public float currentTorque, maxTorqueAtRPM;
    public float torqueAtRPM, torquePoint;

    public float maxRPM, idleRPM;
    public float currentRPM;

    public float gasInput, brakeInput;

    public float currentHP;
    public float horsepowerPoint;
    public float HPAtRPM;
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
    public Text speedText, gearText;
    public Image RPM;
    public Image gas;
    public Image brakeImage;

    // Use this for initialization
    void Start()
    {
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
        steeringInput = Input.GetAxis("Steering");
        gas.fillAmount = gasInput = Input.GetAxis("Gas");
        brakeImage.fillAmount = brakeInput = Input.GetAxis("Brake");

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentGear < 6)
                currentGear++;
            UpdateGear();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentGear > -1)
                currentGear--;
            UpdateGear();
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


        RPM.fillAmount = (currentRPM / maxRPM) * 0.75f;
        //RPM.fillAmount = ((CalculateTorque(maxRPM) / maxTorqueAtRPM)) * 0.75f;

        //Calculate resistance forces
        float totalDragResistance = -dragResistance * velocity * Mathf.Abs(velocity);
        float totalRollResistance = -rollResistance * velocity;

        //Set brake value
        float brake = Mathf.Abs(velocity) > 0.1f ? brakePower : 0;

        //Calculate acceleration
        //horsepower = (maxTorqueAtRPM * currentRPM) / 5252;
        currentHP = FindValue(currentRPM, HPAtRPM, horsepowerPoint);
        maxTorqueAtRPM = FindValue(currentRPM, torqueAtRPM, torquePoint);
        //maxTorqueAtRPM = CalculateTorque(currentRPM);
        currentTorque = (gasInput * maxTorqueAtRPM) / gear[currentGear + 1];

        float driveForce = currentTorque /** gear[currentGear + 1]*/ * finalDriveAxleRatio * 0.7f;// / 0.33f;
        float outputForce = (driveForce - (Mathf.Lerp(0, brake, brakeInput))) + totalDragResistance + totalRollResistance;

        acceleration = outputForce / weight;
        velocity += acceleration;
        speedText.text = ((int)Mathf.Abs((velocity * 60 * 60) / 1000)).ToString();

        float wheelRollSpeed = ((currentTorque * gear[currentGear + 1]) / 60) * 360 * Mathf.Deg2Rad;


        //rigidbody.AddForce(transform.forward * outputForce, ForceMode.Force);
        //transform.Translate(transform.forward * velocity * Time.deltaTime, Space.World);

        currentAngle = (maxAngle * steeringInput);
        transform.Rotate(transform.up * currentAngle * Time.deltaTime);

        //wheel[0].rollSpeed = driveTorque;

    }

    float FindValue(float currentValue, float pointedValue, float peekPoint)
    {
        //RPM to RPM by HP ----- Lerp(RPM, RPM, HP)
        //return currentRPM / HPAtRPM;

        float t = (currentValue / pointedValue);

        float ULerpedValue = (1.0f - t) * 0 + t * peekPoint;

        //Debug.Log("                 " + HPAtRPM * t);
        //(a + (b - a)) * t;
        //(0, HPAtRPM, Mathf.InverseLerp(0, ));
        return ULerpedValue;
    }

    //Torque in N.m
    float CalculateTorque(float RPM)
    {
        //return ((63.025f * currentHP) / RPM) * 1.3559179f;

        //h = (t * r) / 5252
        return currentHP * (5252 / RPM);
    
    }

    void UpdateGear()
    {
        switch (currentGear)
        {
            case -1:
                gearText.text = "R";
                break;

            case 0:
                gearText.text = "N";
                break;

            default:
                gearText.text = currentGear.ToString();
                break;
        }

    }

}