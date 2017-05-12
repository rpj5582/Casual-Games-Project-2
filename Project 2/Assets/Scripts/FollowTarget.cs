using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {
	[SerializeField]
	Transform m_target;
	[SerializeField]
	Rigidbody m_body;

	// Use this for initialization
	void Start () {
		//m_body = GetComponent<RigidBody> ();

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 distance = m_target.position-this.transform.position ;
		if (distance.magnitude > 0.1) {
			this.transform.position = m_target.position;
		}
		distance.Normalize ();//now direction
		m_body.AddForce(distance*Time.deltaTime/100);
	//	m_body.AddForce(distance * Time.deltaTime, ForceMode::Impulsive);
	}
}
