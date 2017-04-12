using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class KNetworkManager : NetworkManager {
    List<Player> playerObjects = new List<Player>();
    [SerializeField]
    Network.Actor P_EXAMPLE;
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            var actor = Instantiate<Network.Actor>(P_EXAMPLE);
            NetworkServer.Spawn(actor.gameObject);
            actor.transform.position = new Vector3(0, 10, 0);
            actor.gameObject.AddComponent<Rigidbody>();

        }
    }

}
