using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AutoConnect : NetworkBehaviour
{
    public string localIP = "127.0.0.1";
    public string remoteIP = "192.168.0.3";
    public int port = 7777;

    // Use this for initialization
    void Start()
    {
        Network.Connect(localIP, port);

        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            NetworkServer.Listen(7777);
            NetworkClient client = ClientScene.ConnectLocalServer();
            client.RegisterHandler(MsgType.Connect, OnConnected);
        }

        //if (Network.peerType == NetworkPeerType.Disconnected)
        //{
        //    Network.Connect(remoteIP, port);
        //}


    }

    private void OnConnected(NetworkMessage msg)
    {
        Debug.Log("Player connected");
    }



}
