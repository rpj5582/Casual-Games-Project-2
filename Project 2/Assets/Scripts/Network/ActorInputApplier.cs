using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Recevies an input from a player then apply it to Actor
 * However if input is desynchrnoized then it tells the client-player that input was not processed correctly
 * then try to compensate as much as possible for desynchrnozied input
 * */
public class ActorInputApplier : MonoBehaviour {
    const float MAX_SYNCH_DELAY = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
