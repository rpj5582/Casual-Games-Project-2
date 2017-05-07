using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMOvement : MonoBehaviour {

    // Use this for initialization
    
    void Start () {
        GameEvents.ME.event_player_assigned.Add(ISpawned);
	}
    void ISpawned(int myId)
    {
        //
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
