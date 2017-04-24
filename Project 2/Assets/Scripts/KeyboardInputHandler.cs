using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles keyboard input from a player
 * */
public class KeyboardInputHandler  {
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void update (MotorForce m_motor) {
        if (Input.GetKey(KeyCode.W))
        {
            m_motor.m_direction = new Vector3(0, 0, 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            m_motor.m_direction = new Vector3(0, 0, -1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_motor.m_direction = new Vector3(-1, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_motor.m_direction = new Vector3(1, 0, 0);
        }else
        {
            m_motor.m_direction = new Vector3(0, 0, 0);

        }


    }
}
