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
            var playerObj = NetworkServer.FindLocalObject(networkObjectID).GetComponent<Player>();
            if (playerObj == null) continue;
			playerInformation.m_player = playerObj;
			SpawnPlayer (conn, playerObj);
        }
    }
	//Spawn a player's character.
	//Call this function only once?
	void SpawnPlayer(NetworkConnection conn, Player player){
		var playerCharacter = Instantiate<Network.Actor> (P_PLAYER_CHARACTER);
		playerCharacter.transform.position = new Vector3 (0, 10, 0);
		playerCharacter.gameObject.AddComponent<Rigidbody>();
		NetworkServer.Spawn (playerCharacter.gameObject);
		player.TargetPlayerCharacter (conn, playerCharacter.netId);

	}
	void spawnPlayerActors(){
		for (int i = 0; i < m_dicPlayerInformation.Count; i++) {
			if (m_dicPlayerInformation [i].m_playerActor != null)
				continue;
			var playerActor = Instantiate<Network.Actor> (P_PLAYER_CHARACTER);
			m_dicPlayerInformation [i].m_playerActor = playerActor;
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
		if (Input.GetKeyDown (KeyCode.B)) {
			spawnPlayerActors ();
		}
    }

}
