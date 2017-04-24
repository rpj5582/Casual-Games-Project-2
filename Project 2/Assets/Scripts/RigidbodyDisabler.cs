using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyDisabler : NetworkBehaviour {

    void Awake()
    {
        var rigidbody = GetComponent<Rigidbody>();
        if (!isServer)
            rigidbody.isKinematic = true;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
