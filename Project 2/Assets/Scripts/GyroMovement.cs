using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GyroMovement : MonoBehaviour
{
    private Rigidbody rbody;

    private Vector3 movementVector = Vector3.zero;

	public float movementSpeed = 5f;
	public float sqForceDeadzone = 0.5f;

    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		movementVector = Vector3.ProjectOnPlane (GyroToUnity(Input.gyro.gravity), Vector3.up);
		Vector3 force = movementVector * movementSpeed;
		if (force.sqrMagnitude > sqForceDeadzone)
		{
			rbody.AddForce (force);
		}
    }

    // Converts the gyroscope's coordinates to unity's coordinates.
	private static Vector3 GyroToUnity(Vector3 v)
    {
		return new Vector3 (v.x, v.z, v.y);
    }
}
