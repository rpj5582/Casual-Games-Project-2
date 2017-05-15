using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Player : NetworkBehaviour
{
    [SyncVar]
    private int id = -1;
    public int ID
    {
        get { return id; }
        set
        {
            if(value > -1)
                id = value;
        }
    }

    public bool DEBUG_USE_GYRO = true;

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

        if (!isLocalPlayer)
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;
        
        if (!DEBUG_USE_GYRO)
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
        if (!isLocalPlayer)
            return;

        if (DEBUG_USE_GYRO)
        {
			Vector3 movementVector = Vector3.ProjectOnPlane(GyroToUnity(Input.gyro.gravity) - calibrationGravity , Vector3.up);
            Vector3 force = movementVector * movementSpeed;
            if (force.sqrMagnitude > sqForceDeadzone)
            {
                rbody.AddForce(force);
            }
        }

//        CmdSyncRigidbody(rbody.velocity);
    }

//    [Command]
//    private void CmdSyncRigidbody(Vector3 velocity)
//    {
//        rbody.velocity = velocity;
//    }

    // Converts the gyroscope's coordinates to unity's coordinates.
    private static Vector3 GyroToUnity(Vector3 v)
    {
        return new Vector3(v.x, v.z, v.y);
    }

	public void reCalibrate(){
		calibrationGravity = GyroToUnity (Input.gyro.gravity);
	}
}
