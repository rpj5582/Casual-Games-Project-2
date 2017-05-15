﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TitleMenu : MonoBehaviour
{
    private GameNetworkManager networkManager;

    private void Start()
    {
        networkManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<GameNetworkManager>();
    }

    public void StartHost()
    {
        networkManager.StartHost(null, 4);
    }

    public void StartClient()
    {
        networkManager.StartClient();
    }
}
