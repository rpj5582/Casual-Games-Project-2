using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroTest : MonoBehaviour
{
    private Quaternion initialRotation;
    private Quaternion initialInverseGyroRotation;

    private void Start()
    {
        // Enables the gyro controls
        Input.gyro.enabled = true;

        // Saves the initial rotation for the cube and device
        initialRotation = transform.rotation;
        initialInverseGyroRotation = Quaternion.Inverse(GyroToUnity(Input.gyro.attitude));
    }

    private void Update()
    {
        // When touching the screen, reset the device's initial rotation
        if(Input.touchCount > 0)
        {
            transform.rotation = initialRotation;
            initialInverseGyroRotation = Quaternion.Inverse(GyroToUnity(Input.gyro.attitude));
        }

        // Rotates the cube based on the gyro controls
        Quaternion offsetRotation = initialInverseGyroRotation * GyroToUnity(Input.gyro.attitude);
        transform.rotation = initialRotation * offsetRotation;
    }

    // Converts the gyroscope's coordinates to unity's coordinates.
    // This is slightly modified from the docs, so it might not be correct for all objects, but it worked better for the cube test.
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
