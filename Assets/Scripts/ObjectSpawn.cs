using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectSpawn : NetworkBehaviour
{
    [SerializeField]
    GameObject objectToChange;

    [SerializeField]
    bool enableObject;

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            objectToChange.SetActive(enableObject);
        }
    }
}
