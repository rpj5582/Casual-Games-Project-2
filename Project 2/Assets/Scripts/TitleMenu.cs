using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TitleMenu : MonoBehaviour
{
    private GameNetworkManager networkManager;

    [SerializeField]
    private GameObject cancelSearchButton;

    private void Start()
    {
        GameObject networkGameObject = GameObject.FindGameObjectWithTag("NetworkManager");
        networkManager = networkGameObject.GetComponent<GameNetworkManager>();
    }

    public void StartHost()
    {
        if(!networkManager.isNetworkActive)
            networkManager.StartHost(null, 4);
    }

    public void StartClient()
    {
        if (!networkManager.isNetworkActive)
        {
            networkManager.StartClient();
            cancelSearchButton.SetActive(true);
        } 
    }

    public void CancelSearch()
    {
        networkManager.StopClient();
        cancelSearchButton.SetActive(false);
    }
}
