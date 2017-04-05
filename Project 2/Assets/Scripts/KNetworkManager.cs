using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class KNetworkManager : NetworkManager {
    List<Player> playerObjects = new List<Player>(); 
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
        foreach (var networkObjectID in conn.clientOwnedObjects)
        {
            var playerObj = NetworkServer.FindLocalObject(networkObjectID).GetComponent<Player>();
            if (playerObj == null) continue;
            playerObjects.Add(playerObj);
        }
    }
    private void Update()
    {
       
    }

}
