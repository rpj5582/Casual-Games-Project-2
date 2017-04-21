using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_Pathing : MonoBehaviour {
    //public const int seatCount = 4;
    const int seatCount = 4; //currently does not support more than 4-seat square tables
                             //may come back to that if needed

    public GameObject seat;

    PathNode nearest; //the nearest possible pathnode

    List<PathNode> seats; //pathnodes representing places a customer may sit

    List<PathNode> path; //how to get from origin node to here, should end on [nearest]

	bool occupied=false; //table is in use by customers

	float tableRadius = 4.0f;

	// Use this for initialization
	void Start () {

        seats = new List<PathNode>();	

		//Vector3 up = transform.up;
		Vector3 spawn = transform.forward * tableRadius;
		for (int i = 0; i < seatCount; i++) {
			GameObject go = Instantiate(seat, transform.position + spawn, transform.rotation) as GameObject;
			go.transform.parent = transform;

			spawn = Quaternion.AngleAxis(360.0f /seatCount,transform.up)*spawn;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
