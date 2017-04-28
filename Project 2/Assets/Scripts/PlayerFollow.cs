using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour {
    public GameObject target;
    public GameObject player;

    private Rigidbody targetRB;

	// Use this for initialization
	void Start () {
		if(target != null) {
            targetRB = target.GetComponent<Rigidbody>();
        };
	}
	
	// Update is called once per frame
	void Update () {
        player.transform.position = targetRB.transform.position;
	}
}
