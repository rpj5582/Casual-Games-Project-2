using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_Pathing : MonoBehaviour {
    //public const int seatCount = 4;
    const int seatCount = 4; //currently does not support more than 4-seat square tables
                             //may come back to that if needed

    public PathNode seat;

    PathNode nearest; //the nearest possible pathnode

    List<PathNode> seats; //pathnodes representing places a customer may sit

    List<PathNode> path; //how to get from origin node to here, should end on [nearest]


	// Use this for initialization
	void Start () {

        seats = new List<PathNode>();	

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
