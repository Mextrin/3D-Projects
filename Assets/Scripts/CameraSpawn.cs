using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraSpawn : NetworkBehaviour
{
    [SerializeField]
    GameObject camera;

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            Camera.main.gameObject.SetActive(false);
            if (camera != null)
            {
                FollowCarCamera spawnedCamera = Instantiate(camera, transform.position, transform.rotation).GetComponent<FollowCarCamera>();
                spawnedCamera.car = transform;
            }
        }
    }
}
