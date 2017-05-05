using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	private Quaternion defaultRotation;

	private void Start ()
	{
		defaultRotation = transform.rotation;
	}

	public void ResetTransform()
	{
		transform.localPosition = Vector3.zero;
		transform.rotation = defaultRotation;
	}
}
