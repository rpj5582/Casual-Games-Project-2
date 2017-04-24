using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class KNetworkManager : NetworkManager {
	Dictionary<int, PlayerInformation> m_dicPlayerInformation = new Dictionary<int, PlayerInformation>();


	[SerializeField]
	Network.Actor P_EXAMPLE;
	[SerializeField]
	Network.Actor P_PLAYER_CHARACTER;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
		var playerInformation = new PlayerInformation ();
		m_dicPlayerInformation.Add (conn.connectionId, playerInformation);

        foreach (var networkObjectID in conn.clientOwnedObjects)
        {
            var playerObj = NetworkServer.FindLocalObject(networkObjectID).GetComponent<Network.Player>();
            if (playerObj == null) continue;
            
            playerInformation.init(playerObj, SpawnPlayer(conn, playerObj));
        }
    }

    //Spawn a player's character.
    //Call this function only once?
    Network.Actor SpawnPlayer(NetworkConnection conn, Network.Player player){
		var playerCharacter = Instantiate<Network.Actor> (P_PLAYER_CHARACTER);
		playerCharacter.transform.position = new Vector3 (0, 10, 0);



		var actorServerUpdate = playerCharacter.gameObject.AddComponent<Network.ActorServerUpdate>();
        actorServerUpdate.init(playerCharacter);
        actorServerUpdate.setMethodExceptParticular(conn.connectionId);


        //player object is prepared. Initiate linking
        NetworkServer.Spawn(playerCharacter.gameObject);
        player.linkActor(playerCharacter.netId);
        player.TargetSpawnActor(conn, playerCharacter.netId);

        return playerCharacter;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            var actor = Instantiate<Network.Actor>(P_EXAMPLE);
            NetworkServer.Spawn(actor.gameObject);
            actor.transform.position = new Vector3(0, 10, 0);
            actor.gameObject.AddComponent<Rigidbody>();
            var actorServerUpdate = actor.gameObject.AddComponent<Network.ActorServerUpdate>();
            actorServerUpdate.init(actor);
        }
    }

}
