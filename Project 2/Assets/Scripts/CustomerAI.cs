using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerState
{
    IDLE, WALKING, DECIDING, WAITING, EATING, LEAVING
}
public class CustomerAI : MonoBehaviour {
    static float maxSpeed = 1.0f;
	private const float nodeRange = 0.1f; //distance a unit must be from a node in order to update path

    private CustomerState state = CustomerState.IDLE;
    private List<PathNode> path;
    private int pathProgress = 0;
    private PathNode next;
    private string order = "";

	private bool xFocused = true; //unit is moving along x-axis and not z

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(state == CustomerState.WALKING)
        {
			
            //seek next
			Vector3 distance = next.transform.position - transform.position;
            //if reached next
			if (distance.magnitude < nodeRange) {
				pathProgress++;
				next = path [pathProgress];
			} 
			//if haven't reached, move closer
			else {
				//move along one axis at a time
				Vector3 pos = transform.position;

				//favor axis with the most distnace needed to be covered?
				if (xFocused) {
					//stub for movement
					//pos.x += Mathf.Max (maxSpeed, distance.x);

					float deltaX = maxSpeed; //attempt to move at maximum speed
					//limit speed if goal is not that far away
					if (maxSpeed > Mathf.Abs (distance.x)) {
						deltaX = distance.x;
					}
					//move in negative direction if node is in negative direction
					else if (distance.x < 0) {
						deltaX *= -1;
					}

					pos.x += deltaX;


					if (next.transform.position.x - transform.position.x < nodeRange) { //has covered all distance necessary on this axis
						xFocused = false;
					}
				} else { //zFocused
					pos.z += Mathf.Max(maxSpeed,distance.z);
				}

				transform.position = pos;


				//transform.position += distance.normalized * maxSpeed;
			}
		}
	}

    //tells customer to follow path to its end
    public void FollowPath(List<PathNode> p)
    {
        state = CustomerState.WALKING;
        path = p;

        pathProgress = 0;
        next = path[pathProgress];
    }
    
	//tells customer to leave the way they came
	public void Leave(){
		path.Reverse ();
		FollowPath(path);
	}

	//private helper, returns the distance unit should move
	//assumes moves along one axis
	private float stepDistance(float distance){

	}
}
