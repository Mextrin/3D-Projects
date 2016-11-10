using UnityEngine;
using System.Collections;

public interface IDrive
{
    float Acceleration();
    float Brake();
    float Steering();
    void UpdateUI();
}
