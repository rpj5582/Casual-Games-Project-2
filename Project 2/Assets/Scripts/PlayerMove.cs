using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//very simple helper script.  May move into a player manager class upon creation of said class.
//sets player location to target location upon update, likely will be used to set the player's
//models position at their controller's location in world space.

public class PlayerMove : MonoBehaviour {
	public GameObject playerController;
	public GameObject player;
	public float torque;
	private Rigidbody playerBody;

	// Use this for initialization
	void Start () {
		playerBody = player.GetComponentInChildren<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		playerBody.AddTorque(playerController.transform.up * torque * playerController.transform.forward.x * -1);
		playerBody.AddTorque(playerController.transform.up * torque * playerController.transform.forward.y * -1);
		playerBody.AddTorque(playerController.transform.up * torque * playerController.transform.forward.z * -1);
	}
}
