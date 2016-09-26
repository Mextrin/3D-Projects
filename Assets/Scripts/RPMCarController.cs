using UnityEngine;
using System.Collections;

public class RPMCarController : MonoBehaviour
{
    public float weight;
    public float dragResistance, rollResistance;
    public WheelRolling[] wheel = new WheelRolling[4];
    public AccelerationType driveType;
    public float torqueAtRPM, maxTorque;
    public float maxRPM, idleRPM;
    public float currentRPM;

    public float gasInput, brakeInput;

    public float horsePower;
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
    //
    Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        //Torque in lb.ft
        maxTorque = CalculateTorque(maxRPM);
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
        gasInput = Input.GetAxis("Gas");
        brakeInput = Input.GetAxis("Brake");

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentGear < 6)
            {
                currentGear++;
            }
        
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentGear > -1)
                currentGear--;
        
        }

        //Calculate resistance forces
        float totalDragResistance, totalRollResistance;

        totalDragResistance = -dragResistance * velocity * Mathf.Abs(velocity);
        totalRollResistance = -rollResistance * velocity;

        //Set brake value
        float brake = Mathf.Abs(velocity) > 0.1f ? brakePower: 0;

        //power = torqueAtRPM * gear[currentGear + 1] * finalDriveAxleRatio * 0.7f;
        //float driveForce = currentRPM / gear[currentGear + 1] * finalDriveAxleRatio * 0.7f;
        
        
        float engineTorque = gasInput * maxTorque;
        float driveTorque = engineTorque * gear[currentGear + 1] * finalDriveAxleRatio * 0.7f;
        Debug.Log("                 " + engineTorque);


        torqueAtRPM = CalculateTorque(currentRPM);
        float outputForce = (driveTorque - (Mathf.Lerp(0, brake, brakeInput))) + totalDragResistance + totalRollResistance;

        acceleration = outputForce / weight;
        velocity += acceleration;
        //Debug.Log("             " + (velocity * 60 * 60) / 1000 + " km/h");

        float wheelRollSpeed = velocity / wheel[0].wheelRadius;
        currentRPM = (wheelRollSpeed * gear[currentGear + 1] * finalDriveAxleRatio * 60) / (2 * Mathf.PI);

        if (currentRPM < idleRPM)
        {
            currentRPM = idleRPM;
        }

        transform.Translate(transform.forward * velocity * Time.deltaTime, Space.World);
        
        currentAngle = (maxAngle * steeringInput);
        transform.Rotate(transform.up * currentAngle * Time.deltaTime);



    }

    //Torque in lb.ft
    float CalculateTorque(float RPM)
    {
        return ((63.025f * horsePower) / RPM);
    }


}
