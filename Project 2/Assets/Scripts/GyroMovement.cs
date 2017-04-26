﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GyroMovement : MonoBehaviour
{
    private Rigidbody rbody;
    
    private Quaternion initialRotation;
    private Quaternion initialInverseGyroRotation;
	private int torque = 5;
    private GameObject movementCube;

    private Vector3 movementVector = Vector3.zero;

    // Use this for initialization
    void Start()
    {
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.orientation = ScreenOrientation.LandscapeLeft;

        Input.gyro.enabled = true;
		Debug.Log (Input.gyro.enabled);

        rbody = GetComponent<Rigidbody>();

        movementCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        movementCube.GetComponent<Renderer>().enabled = true;
		movementCube.transform.Rotate (Vector3.up, 90);
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

    //    movementVector = Vector3.ProjectOnPlane(movementCube.transform.up, Vector3.up);
       // rbody.velocity = movementVector;
		rbody.AddTorque(movementCube.transform.up * torque * movementCube.transform.forward.x);
		rbody.AddTorque(movementCube.transform.up * torque * movementCube.transform.forward.y);
	    rbody.AddTorque(movementCube.transform.up * torque * movementCube.transform.forward.z);
    }

    // Converts the gyroscope's coordinates to unity's coordinates.
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.z, q.y, -q.w);
    }
}
