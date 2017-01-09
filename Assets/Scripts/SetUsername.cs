using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUsername : MonoBehaviour
{
    AutoConnect networkManager;

    // Use this for initialization
    void Start()
    {
        networkManager = FindObjectOfType<AutoConnect>();
        Text visibleName = GetComponent<Text>();
        visibleName.text = networkManager.username;
    }
}
