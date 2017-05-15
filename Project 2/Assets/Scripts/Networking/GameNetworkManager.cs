using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameNetworkManager : NetworkManager
{
    private int playerIDCount;

    bool sendAddPlayerRPC = false;

    private void Start()
    {
        
    }

    // Called on the client
    public override void OnClientConnect(NetworkConnection conn)
    {
        ClientScene.AddPlayer(conn, 0);
    }

    // Called on the client
    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        // This function needs to be overridden so that the client isn't readied twice.
    }

    // Called on the server
    public override void OnServerSceneChanged(string sceneName)
    {
        EntityLibrary.SendRegisterPlayersRPC();
    }

    // Called on the server
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = Instantiate(playerPrefab, GetStartPosition().position, Quaternion.identity) as GameObject;

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        int playerID = playerIDCount++;
        player.GetComponent<Player>().ID = playerID;

        sendAddPlayerRPC = true;

    }

    // This is needed because calling the RPC on the same frame as the level is loaded won't work properly, needs to be at the end of the frame after everything has spawned.
    private void LateUpdate()
    {
        if(sendAddPlayerRPC)
        {
            sendAddPlayerRPC = false;
            EntityLibrary.SendRegisterPlayersRPC();
        }
    }
}
