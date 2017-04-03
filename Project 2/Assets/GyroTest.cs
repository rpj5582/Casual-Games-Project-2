using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroTest : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = GyroToUnity(Input.gyro.attitude);
        Debug.Log(transform.rotation);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
