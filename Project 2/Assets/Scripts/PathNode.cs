using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour {

    public List<GameObject> subNodes = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < subNodes.Count; i++)
        {
            Debug.DrawLine(transform.position, subNodes[i].transform.position, Color.red);
        }
	}
}
