using UnityEngine;
using System.Collections;

public enum AccelerationType { FrontWheelDrive, RearWheelDrive, FourWheelDrive };

public class YetAnotherCarController : MonoBehaviour
{
    public WheelRolling[] wheel = new WheelRolling[4];
    public AccelerationType driveType;
    //public float finalTorque;
    //public float maxRPM, idleRPM;
    //public float currentRPM;
    public float acceleration, currentAcceleration;
    public float brakePower;
    public float velocity, prevVelocity;
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


        //Speed in m/s
        //Acceleration in m/s^2

        //Time = sq(v / a)
        float accelerationPercentage = 0;
        switch (driveType)
        {
            case AccelerationType.FrontWheelDrive:
                for (int i = 0; i < 2; i++)
                {
                    if (wheel[i].isGrounded)
                    {
                        accelerationPercentage++;
                    }
                }
                accelerationPercentage /= 2;
                break;

            case AccelerationType.RearWheelDrive:
                for (int i = 2; i < 4; i++)
                {
                    if (wheel[i].isGrounded)
                    {
                        accelerationPercentage++;
                    }
                }
                accelerationPercentage /= 2;
                break;

            case AccelerationType.FourWheelDrive:
                for (int i = 0; i < 4; i++)
                {
                    if (wheel[i].isGrounded)
                    {
                        accelerationPercentage++;
                    }
                }
                accelerationPercentage /= 4;
                break;
        }

        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    RefreshTime();
                }

                currentAcceleration = acceleration * accelerationPercentage;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    RefreshTime();
                }

                if (velocity > 0.1f)
                {
                    currentAcceleration = (-brakePower);
                }
                else
                {
                    RefreshTime();
                    currentAcceleration = 0;
                    prevVelocity = 0;
                }

            }

        }
        else
        {
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                RefreshTime();
            }

            if (velocity > 0.1f)
            {
                currentAcceleration = (-acceleration * accelerationPercentage);
            }
            else
            {
                RefreshTime();
                currentAcceleration = 0;
                prevVelocity = 0;
            }

        }



        float t = (Time.time - timeStart);
        velocity = (currentAcceleration * t) + prevVelocity;
        Debug.Log("             " + (velocity * 60 * 60) / 1000 + " km/h " + (prevVelocity * 60 * 60) / 1000);
        
        currentAngle = (maxAngle * steeringInput) * Mathf.Lerp(0, 1, velocity * 0.1f);

        wheel[0].rollSpeed = velocity;
        wheel[1].rollSpeed = -velocity;
        wheel[2].rollSpeed = velocity;
        wheel[3].rollSpeed = -velocity;

        wheel[0].currentAngle = currentAngle / 2;
        wheel[1].currentAngle = currentAngle / 2;

        transform.Rotate(transform.up * currentAngle * Time.deltaTime);
        transform.Translate(transform.forward * velocity * Time.deltaTime, Space.World);
    }

    void RefreshTime()
    {
        currentTime = Mathf.Sqrt(velocity / acceleration);
        timeStart = Time.time;
        prevVelocity = velocity;
    }

}
