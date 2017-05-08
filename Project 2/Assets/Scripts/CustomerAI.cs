using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerState
{
    IDLE, WALKING, DECIDING, WAITING, EATING, LEAVING
}
public class CustomerAI : MonoBehaviour {
    static float maxSpeed = 5.0f;
	private const float nodeRange = 0.5f; //distance a unit must be from a node in order to update path

    private CustomerState state = CustomerState.IDLE;
    private List<PathNode> path;
    private int pathProgress = 0;
    private PathNode next;
    private string order = "";

	public bool xFocused = true; //unit is moving along x-axis and not z

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(state == CustomerState.WALKING) {
			move_NoPhysics ();
		}
	}

    //tells customer to follow path to its end
    public void FollowPath(List<PathNode> p) {
        state = CustomerState.WALKING;
        path = p;

        pathProgress = 0;
        next = path[pathProgress];
    }
    
	//tells customer to leave the way they came
	public void Leave() {
		path.Reverse ();
		FollowPath(path);
	}

	//moves this object's coordinates directly without any use of physics
	//strategy mainly used for debugging, but if physics are not deemed necessary may continue using
	private void move_NoPhysics() {

		//seek next
		Vector3 distance = next.transform.position - transform.position;
        distance.y = 0;

		//if reached next
		if (distance.magnitude < nodeRange) {

			pathProgress++;
			if (pathProgress >= path.Count) {
				state = CustomerState.IDLE;
			} else {
				next = path [pathProgress];

				distance = next.transform.position - transform.position;
				// focus on the axis with more ground to cover
				xFocused = (distance.x * distance.x > distance.z * distance.z);
			}
		} 

		//if haven't reached, move closer
		else {
			//move along one axis at a time
			Vector3 pos = transform.position;

			//favor axis with the most distnace needed to be covered?
			if (xFocused) {

				pos.x += stepDistance(distance.x); //get distance traveleable in one step

				if (next.transform.position.x - pos.x < nodeRange) { //has covered all distance necessary on this axis
					xFocused = false;
				}
			} else { //zFocused
				pos.z += stepDistance(distance.z); //get distance traveleable in one step

				if (next.transform.position.z - pos.z < nodeRange) { //has covered all distance necessary on this axis
					xFocused = true;
				}
			}

			transform.position = pos;
		}
	}

	//private helper, returns the distance unit should move
	//add return value directly to coordinates
	//assumes moves along one axis
	private float stepDistance(float distance) {
		float toReturn = 1.0f;

		//float tDist = distance * Time.deltaTime;
		float tSpd = maxSpeed * Time.deltaTime;
		if (distance*distance > tSpd * tSpd) {
			if (distance < 0) {
				tSpd *= -1;
			}
			return tSpd;
		}
		return distance;
		/*
		//get the smaller of two: distance or maxspeed
		if (distance * distance > maxSpeed * maxSpeed) {
			toReturn = maxSpeed;
			//if distance was in negative direction, should return negative
			if (distance < 0) {
				toReturn *= -1;
			}

			//apply time scaling
			//"arrive" in 1/4 of a second
			return toReturn * Time.deltaTime;
		} 
			
		return distance;*/
	}
}
