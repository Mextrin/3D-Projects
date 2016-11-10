using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerInput : MonoBehaviour, IDrive
{
    RPMCarController car;

    public Text speedText, gearText;
    public Image RPM;
    public Image gas;
    public Image brake;

    void Start()
    {
        car = GetComponentInParent<RPMCarController>();
    }

    public float Acceleration()
    {
        float input = Input.GetAxis("Gas");
        gas.fillAmount = input;
        return input;
    }

    public float Brake()
    {
        float input = Input.GetAxis("Brake");
        brake.fillAmount = input;
        return input;
    }

    public float Steering()
    {
        return Input.GetAxis("Steering");
    }

    public void UpdateUI()
    {
        speedText.text = ((int)Mathf.Abs((car.velocity * 60 * 60) / 1000)).ToString();
        RPM.fillAmount = (car.currentRPM / car.maxRPM) * 0.75f;

        switch (car.currentGear)
        {
            case -1:
                gearText.text = "R";
                break;

            case 0:
                gearText.text = "N";
                break;

            default:
                gearText.text = car.currentGear.ToString();
                break;
        }
    }
}
