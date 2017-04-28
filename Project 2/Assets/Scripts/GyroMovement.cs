using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GyroMovement : MonoBehaviour
{
    private Rigidbody rbody;

	private Vector3 initialGravity = Vector3.zero;
    private Vector3 movementVector = Vector3.zero;

	public float movementSpeed = 5;
	public float sqForceDeadzone = 0.1f;

    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;

        rbody = GetComponent<Rigidbody>();

		// Saves the initial gravity vector so that the gyro's initial rotation is the "zero" rotation
		initialGravity = GyroToUnity(Input.gyro.gravity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		movementVector = Vector3.ProjectOnPlane (GyroToUnity(Input.gyro.gravity), initialGravity);
		Vector3 force = movementVector * movementSpeed;
		if (force.sqrMagnitude > sqForceDeadzone)
		{
			Debug.Log (force.sqrMagnitude);
			rbody.AddForce (force);
		}
    }

    // Converts the gyroscope's coordinates to unity's coordinates.
	private static Vector3 GyroToUnity(Vector3 v)
    {
		return new Vector3 (v.x, v.z, v.y);
    }
}
