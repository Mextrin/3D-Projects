using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class carControls : MonoBehaviour
{
    /*
        Order:
        - Object
        - Int
        - Double
        - Float
        - String
        - Char
        - Bool
    */

    //Public
    public bool engineOn;
    public float RPM, maxRPM, idleRPM;
    public float RPMChange, RPMStartupChangeMax, RPMGearChangeMax, RPMCooldownChangeMax, RPMShutdownChangeMax;
    public float RPMStartAcceleration, RPMGearAcceleration;
    public int currentGear;

    public float gear1Torque;
    public float gear2Torque;
    public float gear3Torque;
    public float gear4Torque;
    public float gear5Torque;
    public float gear6Torque;
    public float gearRTorque;
    public float[] gearTorque;

    public float timePressed;

    //Private
    Rigidbody rigidbody;
    public Text velocity, gear, revPerMin, RPMPercent;
    public Image RPMMeter;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        ShowGear();
    }

    void Update()
    {
        UpdateGears();

        if (engineOn)
        {

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    if (currentGear == 0 && RPMChange < RPMGearChangeMax && maxRPM > RPM)
                        RPMChange += RPMGearAcceleration;

                    if (currentGear != 0)
                    {   
                        if (RPMChange < RPMGearChangeMax && maxRPM > RPM)
                            RPMChange += gearTorque[currentGear + 1] * RPMGearAcceleration;

                        rigidbody.AddForce(transform.forward * gearTorque[currentGear + 1] * (1 / RPM + 650));
                    }

                }

                if (Input.GetKey(KeyCode.DownArrow))
                {

                }

            }
            
            else
            {
                if (RPM > idleRPM && RPMChange > -RPMCooldownChangeMax)
                    RPMChange -= RPMStartAcceleration;

                else if (RPM < idleRPM && RPMChange < RPMStartupChangeMax)
                    RPMChange += RPMStartAcceleration;

            }


            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentGear < 6)
                    currentGear++;

                ShowGear();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (currentGear > -1)
                    currentGear--;

                ShowGear();
            }
        }
        else if (!engineOn)
        {
            if (RPM < 1)
            {
                RPM = 0;
                RPMChange = 0;
            }

            else if (RPMChange > -RPMShutdownChangeMax)
                RPMChange -= RPMStartAcceleration;

        }

        if (RPMChange != 0)
            RPM += RPMChange;

        revPerMin.text = "RPM: " + RPM;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
        }


        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!engineOn || RPM < 1000)
                engineOn = !engineOn;
        }

        velocity.text = "Velocity: " + Mathf.Sqrt(rigidbody.velocity.x * rigidbody.velocity.x + rigidbody.velocity.z * rigidbody.velocity.z);

    }

    void ShowGear()
    {
        if (currentGear >= 1)
            gear.text = "Gear: " + currentGear.ToString();

        else if (currentGear == 0)
            gear.text = "Gear: N";

        else if (currentGear < 0)
            gear.text = "Gear: R";

    }
    void UpdateGears() //Only for edit mode
    {
        gearTorque = new float[]
        {
            gearRTorque,
            0,
            gear1Torque,
            gear2Torque,
            gear3Torque,
            gear4Torque,
            gear5Torque,
            gear6Torque
        };

    }

}

