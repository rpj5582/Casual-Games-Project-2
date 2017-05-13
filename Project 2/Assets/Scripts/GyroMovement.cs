using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GyroMovement : MonoBehaviour
{
    public bool USE_GYRO = true;

    private Rigidbody rbody;

	public float movementSpeed = 5f;
	public float sqForceDeadzone = 0.5f;

	public Vector3 calibrationGravity = new Vector3(0.0f, 0.0f, 0.0f);
    // Use this for initialization
    private void Start()
    {
        Input.gyro.enabled = true;
        rbody = GetComponent<Rigidbody>();

		calibrationGravity = GyroToUnity (Input.gyro.gravity);
    }

    private void Update()
    {
        if(!USE_GYRO)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 movementVector = new Vector3(h, 0, v);
            Vector3 force = movementVector * movementSpeed;
            rbody.AddForce(force);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(USE_GYRO)
        {
			Vector3 movementVector = Vector3.ProjectOnPlane(GyroToUnity(Input.gyro.gravity) - calibrationGravity , Vector3.up);
            Vector3 force = movementVector * movementSpeed;
            if (force.sqrMagnitude > sqForceDeadzone)
            {
                rbody.AddForce(force);
            }
        }
    }

    // Converts the gyroscope's coordinates to unity's coordinates.
	private static Vector3 GyroToUnity(Vector3 v)
    {
		return new Vector3 (v.x, v.z, v.y);
    }

	public void reCalibrate(){
		calibrationGravity = GyroToUnity (Input.gyro.gravity);
	}
}
