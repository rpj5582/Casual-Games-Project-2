using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This object moves toward the given transform's position constantly at a steady speed
//Use this for a smooth visual representation of an object that teleports around
public class FollowTarget : MonoBehaviour {
	[SerializeField]
	Transform m_target;
	[SerializeField]
	Rigidbody m_body;
    
    public float minDistance = 0.1f;
    public float maxSpeed = 10.0f;

    public CustomerAI ai;

	// Use this for initialization
	void Awake () {
        ai = m_target.GetComponent<CustomerAI>();
        if(ai != null)
        {
            ai.SetFollower(gameObject);
            gameObject.SetActive (false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        

        Vector3 distance = m_target.position - m_body.position;

        //if distance is very small, forget about math, just teleport there and hold still
        if (distance.magnitude < minDistance)
        {
            m_body.position = m_target.position;
            m_body.velocity = Vector3.zero;
            return;
        }
        else //move as fast as possible in the desired direction
        {
            m_body.velocity = distance.normalized * maxSpeed;
        }

        //stall the ai if it's getting too far ahead
        if(distance.magnitude > 5.0f)
        {
            if (ai != null)
            {
               // Debug.Log("stalling");
                ai.stall();
            }
        }
    }
}
