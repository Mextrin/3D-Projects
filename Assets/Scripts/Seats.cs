using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seats : MonoBehaviour
{
    public List<Transform> seats;

    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            seats.Add(child);
        }
    }
}
