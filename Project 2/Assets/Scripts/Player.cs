using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	[SerializeField]
	PlayerName P_PLAYER_NAME;

    bool isPinged = false;
    float m_timeElapsed = 0;


	GameObject m_playerObject = null;

	//UI componenets


    [Command]
    public void CmdPing()
    {
        TargetPing(this.connectionToClient);
    }
    [TargetRpc]
    public void TargetPing(NetworkConnection conn)
    {
        Debug.Log("I just received my ping at time... " + m_timeElapsed);
        isPinged = false;
    }
    [TargetRpc]
    public void TargetServerTalkedTo(NetworkConnection conn)
    {
        Debug.Log("I just received my ping at time... " + m_timeElapsed);
        isPinged = false;
    }
	//used to receive the player's character that just got created from the server
	[TargetRpc]
	public void TargetPlayerCharacter(NetworkConnection conn, NetworkInstanceId id){
		var nametag = Instantiate<PlayerName> (P_PLAYER_NAME);
		var playerObj = ClientScene.FindLocalObject (id);
		//Debug.Log (playerObj);
		nametag.setTransform (playerObj.transform);
	}
    private void Update()
    {
         
    }
}
