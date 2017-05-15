using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TitleMenu : MonoBehaviour
{
    public GameNetworkManager networkManager;

    public void StartHost()
    {
        networkManager.StartHost(null, 4);
    }

    public void StartClient()
    {
        networkManager.StartClient();
    }
}
