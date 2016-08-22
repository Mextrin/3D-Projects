using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Assertions;

public class CarController : MonoBehaviour
{

    //Gas = 0 - 1
    //RPMAcceleration = Gas, GearSize
    //RPM = RPMAcceleration, GearSpeed, WheelSpeed
    //GearSpeed = GearSize * RPM

    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public GameObject[] wheels = new GameObject[4];
    Rigidbody rigidbody;
    public float acceleration;
    public float[] gearRatio;
    public float gearPeak;
    public float gearRRatio, gear1Ratio, gear2Ratio, gear3Ratio, gear4Ratio, gear5Ratio, gear6Ratio;
    public float idleFuelIntake, fuelIntake;
    public float accelerationChange, accelerationChangeMax;
    int currentGear = 1;
    public float gearTorque;
    public float RPMAcceleration;
    public float RPM, maxRPM;
    float velocity;
    public bool engineOn;

    Text velocityText, RPMtext, gear;
    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        velocityText = transform.FindChild("Canvas").FindChild("Velocity").GetComponent<Text>();
        gear = transform.FindChild("Canvas").FindChild("Gear").GetComponent<Text>();
        RPMtext = transform.FindChild("Canvas").FindChild("RPM").GetComponent<Text>();

        ShowGear();
    }

    // Update is called once per frame
    void Update()
    {
        if (engineOn)
        {
            UpdateGears();

            acceleration = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.UpArrow))
            {
            }



            RPM = idleFuelIntake + (RPMAcceleration * gearRatio[currentGear + 1]);
            if (RPM > maxRPM)
                RPM = maxRPM;

            gearTorque = RPM / gearRatio[currentGear + 1];

            RPMAcceleration = (acceleration * fuelIntake) / gearRatio[currentGear + 1];

        }
        else if (RPM > 0)
            RPM = 0;

        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentGear < 6)
            {
                currentGear++;
            }

            ShowGear();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentGear > -1)
                currentGear--;

            ShowGear();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            engineOn = !engineOn;
        }

        wheelColliders[2].motorTorque = wheelColliders[3].motorTorque = gearTorque;


        for (int i = 0; i < 4; i++)
        {
            Vector3 position;
            Quaternion rotation;

            Vector3 rot = new Vector3(0, 90, 0);

            wheelColliders[i].GetWorldPose(out position, out rotation);

            //rot = rotation.eulerAngles;
            //rot.x += 90f;
            Debug.Log(rotation.eulerAngles.x);

            rot = rotation.eulerAngles;
            //rot.z += 90f;
            rot.z = rot.x;
            rot.x = 0f;
            rot.y = 90f;
            rotation.eulerAngles = rot;


            wheels[i].transform.position = position;
            wheels[i].transform.rotation = rotation;
        }

        RPMtext.text = "RPM: " + RPM;
        velocity = Mathf.Sqrt(rigidbody.velocity.x * rigidbody.velocity.x + rigidbody.velocity.z * rigidbody.velocity.z);
        velocityText.text = "Velocity: " + velocity;
        
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
        gearRatio = new float[]
        {
            gearRRatio,
            0,
            gear1Ratio,
            gear2Ratio,
            gear3Ratio,
            gear4Ratio,
            gear5Ratio,
            gear6Ratio
        };

    }
}
