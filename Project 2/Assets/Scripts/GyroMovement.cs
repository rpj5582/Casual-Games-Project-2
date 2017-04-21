using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GyroMovement : MonoBehaviour
{
	public float moveSpeed;
	public float movementDeadzone;

    private Rigidbody rbody;
    
    private Quaternion initialRotation;
    private Quaternion initialInverseGyroRotation;

    private GameObject movementCube;

    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;

        rbody = GetComponent<Rigidbody>();

        movementCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        movementCube.GetComponent<Renderer>().enabled = false;
        movementCube.name = "Player Movement Cube";

        // Saves the initial rotation for the cube and device
        initialRotation = movementCube.transform.rotation;
        initialInverseGyroRotation = Quaternion.Inverse(GyroToUnity(Input.gyro.attitude));
    }

    // Update is called once per frame
    void Update()
    {
        // Rotates the cube based on the gyro controls
        Quaternion offsetRotation = initialInverseGyroRotation * GyroToUnity(Input.gyro.attitude);
        movementCube.transform.rotation = initialRotation * offsetRotation;

        Vector3 movementVector = Vector3.ProjectOnPlane(movementCube.transform.up, Vector3.up);
		if (movementVector.sqrMagnitude >= movementDeadzone * movementDeadzone)
		{
			rbody.velocity = movementVector * moveSpeed;
		}
		else
		{
			rbody.velocity = Vector3.zero;
		}

		float rotationAngle = Vector3.Dot (movementVector, transform.right);
		transform.RotateAround(transform.position, Vector3.up, rotationAngle);

    }

    // Converts the gyroscope's coordinates to unity's coordinates.
	// This function is modified to work for the cube's top face specifically so the up vector can be used to move the player
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.z, q.y, -q.w);
    }
}
