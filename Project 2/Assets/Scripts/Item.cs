using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Item : NetworkBehaviour {

	private Quaternion defaultRotation;

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

	protected virtual void Start ()
	{
		defaultRotation = transform.rotation;
	}

	public void ResetTransform()
	{
		transform.localPosition = Vector3.zero;
		transform.rotation = defaultRotation;
	}
}
