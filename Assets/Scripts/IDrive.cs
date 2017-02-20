using UnityEngine;
using System.Collections;

public interface IDrive
{
    float Acceleration(WheelColliderController vehicle);
    float Brake(WheelColliderController vehicle);
    float Steering(WheelColliderController vehicle);
}
