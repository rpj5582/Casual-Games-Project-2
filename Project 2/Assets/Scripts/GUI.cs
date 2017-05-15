using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GUI : MonoBehaviour
{
    public Canvas optionsCanvas;
    private GameNetworkManager networkManager;

    private void Start()
    {
        networkManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<GameNetworkManager>();
    }

    public void ShowOptions()
    {
        optionsCanvas.enabled = true;
    }

    public void HideOptions()
    {
        optionsCanvas.enabled = false;
    }

    public void Recalibrate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            Player player = players[i].GetComponent<Player>();
            if (player.isLocalPlayer)
            {
                player.reCalibrate();
                break;
            }
        }
    }

    public void Disconnect()
    {
        networkManager.StopClient();
    }
}
