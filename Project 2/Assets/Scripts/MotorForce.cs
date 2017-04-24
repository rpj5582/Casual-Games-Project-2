using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Motor clss that moves a rigidbody with a force to a direction
 * */
public class MotorForce :MonoBehaviour {
    public Rigidbody m_body;
    //public Rigidbody m_body = null;
    public float m_force = 10;
    public Vector3 m_direction;
	// Use this for initialization
	void Start () {
		
	}
    public void FixedUpdate()
    {
        //Debug.Log(m_direction + " , ");
        if (m_direction == Vector3.zero || m_direction.magnitude < 0.1f)
        {
            return;
        }
        if ( m_force == 0) return;
        m_body.AddForce(m_direction * m_force * Time.fixedDeltaTime, ForceMode.Impulse);
    }
    // Update is called once per frame
    void Update () {

        
		
	}
}
