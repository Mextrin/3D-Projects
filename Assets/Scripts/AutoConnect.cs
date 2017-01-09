using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AutoConnect : NetworkBehaviour
{
    public string username;
    [SerializeField]
    Text nameField;
    [SerializeField]
    GameObject mainUI;

    public GameObject playerObject;
    public Transform spawnPoint;

    public NetworkConnection conn;

    // Use this for initialization
    void Start()
    {
        NetworkManager network = GetComponent<NetworkManager>();
        if (network != null)
        {
            network.StartClient();
            network.StartHost();
        }
    }

    public void Connect()
    {
        if (nameField.text.Length >= 6f)
        {
            username = nameField.text;
        }

        DisableUI();
        
    }

    void DisableUI()
    {
        if (username.Length > 0)
        {
            mainUI.SetActive(false);

            

            var spawnedPlayer = (GameObject)Instantiate(playerObject, spawnPoint.position, spawnPoint.rotation);
            NetworkServer.AddPlayerForConnection(connectionToServer, spawnedPlayer, playerControllerId);

        }
    }

   

}
