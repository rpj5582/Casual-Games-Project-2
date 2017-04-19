using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    bool isPinged = false;
    float m_timeElapsed = 0;

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

	}
    private void Update()
    {
         
    }
}
