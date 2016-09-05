using UnityEngine;
using System.Collections;

public enum AccelerationType { FrontWheelDrive, RearWheelDrive, FourWheelDrive };

public class YetAnotherCarController : MonoBehaviour
{
    public WheelRolling[] wheelTorque = new WheelRolling[4];
    //public AccelerationType driveType;
    //public float finalTorque;
    //public float maxRPM, idleRPM;
    //public float currentRPM;
    public float acceleration, inputAcceleration;
    public float velocity;
    //public bool clutch;
    //
    //public int currentGear;
    //public float gearRRatio, gear1Ratio, gear2Ratio, gear3Ratio, gear4Ratio, gear5Ratio, gear6Ratio;
    //public float finalDriveAxleRatio;
    //float[] gear;
    //
    public float maxAngle;
    public float currentAngle;
    public float steeringInput;
    //
    //Rigidbody rigidbody;

    float timeStart, currentTime;
    bool stillholding;

    // Use this for initialization
    void Start()
    {
        //rigidbody = GetComponent<Rigidbody>();
        //
        //gear = new float[]
        //    {
        //        gearRRatio,
        //        0,
        //        gear1Ratio,
        //        gear2Ratio,
        //        gear3Ratio,
        //        gear4Ratio,
        //        gear5Ratio,
        //        gear6Ratio,
        //    };
    }

    // Update is called once per frame
    void Update()
    {

        inputAcceleration = Input.GetAxis("Acceleration");
        steeringInput = Input.GetAxis("Steering");

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if (currentGear < 6)
        //    {
        //        currentGear++;
        //    }
        //
        //}
        //
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    if (currentGear > -1)
        //        currentGear--;
        //
        //}

        //Focus point
        //Find a way to slow down acceleration / gear without affecting the RPM
        //acceleration = (inputAcceleration * (maxRPM - idleRPM)) / (gear[currentGear + 1]);
        //currentRPM = idleRPM + (acceleration * gear[currentGear + 1]);
        //finalTorque = currentRPM / gear[currentGear + 1] * finalDriveAxleRatio;

        currentAngle = maxAngle * steeringInput;

        //Speed in m/s
        //Acceleration in m/s^2

        //Time = sq(v / a)
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (!stillholding)
            {
                stillholding = true;
                currentTime = Mathf.Sqrt(velocity / acceleration);
                timeStart = Time.time;
                
            }
            float t = (Time.time - timeStart) + currentTime;
            velocity = acceleration * (t * t);
            Debug.Log("             " + (velocity * 60 * 60) / 1000 + " km/h");
        }
        else
        {
            if (stillholding)
            {
                stillholding = false;
                currentTime = Mathf.Sqrt(velocity / acceleration);
                timeStart = Time.time;
            }
            if (velocity > .01f)
            {
                float t = Time.time - (timeStart + currentTime);
                velocity = acceleration * (t * t);

            }
        }



        //if (!clutch && inputAcceleration > 0 && currentGear != 1)
        //{
        //    //Speed = RPM
        //}
        //else
        //{
        //    //RPM = Speed
        //}

        //switch (driveType)
        //{
        //    case AccelerationType.FrontWheelDrive:
        //        for (int i = 0; i < 2; i++)
        //        {
        //            wheelTorque[i].spinRate = finalTorque / 2;
        //        }
        //        break;
        //
        //    case AccelerationType.RearWheelDrive:
        //        for (int i = 2; i < 4; i++)
        //        {
        //            wheelTorque[i].spinRate = finalTorque / 2;
        //        }
        //        break;
        //
        //    case AccelerationType.FourWheelDrive:
        //        for (int i = 0; i < 4; i++)
        //        {
        //            wheelTorque[i].spinRate = finalTorque / 4;
        //        }
        //        break;
        //}

        //outputForce = 0;
        //for (int i = 0; i < wheelTorque.Length; i++)
        //{
        //    outputForce += wheelTorque[i].spinTorque;
        //}
        

        wheelTorque[0].rollSpeed = velocity;
        wheelTorque[1].rollSpeed = -velocity;
        wheelTorque[2].rollSpeed = velocity;
        wheelTorque[3].rollSpeed = -velocity;

        transform.Rotate(transform.up * currentAngle * Time.deltaTime);
        transform.Translate(transform.forward * velocity * Time.deltaTime, Space.World);
    }

}
