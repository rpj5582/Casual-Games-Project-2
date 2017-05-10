using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {
	[SerializedField]
	Transform m_target;
	[SerializedField]
	Rigidbody m_body;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 distance = m_target.position-this.transform.position ;
		if (distance.magnitude > 2) {
			this.transform.position = m_target.position;
		}
		distance.Normalize ();//now direction
		m_body.AddForce(distance * Time.deltaTime, ForceMode::Impulsive);
	}
}
