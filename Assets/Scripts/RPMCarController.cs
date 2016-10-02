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
            {
                currentGear++;
            }
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
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentGear > -1)
                currentGear--;
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

        //Calculate resistance forces
        float totalDragResistance, totalRollResistance;

        totalDragResistance = -dragResistance * velocity * Mathf.Abs(velocity);
        totalRollResistance = -rollResistance * velocity;

        //Set brake value
        float brake = Mathf.Abs(velocity) > 0.1f ? brakePower: 0;

        //power = torqueAtRPM * gear[currentGear + 1] * finalDriveAxleRatio * 0.7f;
        //float driveForce = currentRPM / gear[currentGear + 1] * finalDriveAxleRatio * 0.7f;

       
        maxTorque = CalculateTorque(currentRPM);
        torqueAtRPM = (gasInput * maxTorque);
        float driveTorque = torqueAtRPM * gear[currentGear + 1] * finalDriveAxleRatio * 0.7f;
        Debug.Log("                 " + torqueAtRPM);

        float outputForce = (driveTorque - (Mathf.Lerp(0, brake, brakeInput))) + totalDragResistance + totalRollResistance;

        acceleration = outputForce / weight;
        velocity += acceleration;
        speedText.text = ((int)(velocity * 60 * 60) / 1000).ToString();
        //Debug.Log("             " + (velocity * 60 * 60) / 1000 + " km/h");

        float wheelRollSpeed = velocity / wheel[0].wheelRadius;
        currentRPM = (wheelRollSpeed * gear[currentGear + 1] * finalDriveAxleRatio) * (60 / (2 * Mathf.PI));

        if (currentRPM < idleRPM)
        {
            currentRPM = idleRPM;
        }

        RPM.fillAmount = (currentRPM / maxRPM) / 2;

        transform.Translate(transform.forward * velocity * Time.deltaTime, Space.World);
        
        currentAngle = (maxAngle * steeringInput);
        transform.Rotate(transform.up * currentAngle * Time.deltaTime);
        
        //wheel[0].rollSpeed = driveTorque;

    }

    //Torque in lb.ft
    float CalculateTorque(float RPM)
    {
        return ((63.025f * horsePower) / RPM);
    }

    
}
