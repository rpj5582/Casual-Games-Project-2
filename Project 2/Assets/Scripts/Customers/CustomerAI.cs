using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerState
{
    IDLE, WALKING, DECIDING, WAITING, EATING, LEAVING
}
public class CustomerAI : MonoBehaviour {
    static float maxSpeed = 5.0f;
	private const float nodeRange = 1.0f; //distance a unit must be from a node in order to update path

    private CustomerState state = CustomerState.IDLE;
    private List<PathNode> path;
    private int pathProgress = 0;
    private PathNode next;

	public bool xFocused = true; //unit is moving along x-axis and not z

    private List<CustomerAI> others;
    private float seperationDistance=1.0f;

    private Vector3 prevPos;

    private GameObject follower = null;

	// Use this for initialization
	void Start () {
        //store a referance to all customers who are not this, for collision checking
        others = new List<CustomerAI>();
        foreach (CustomerAI o in FindObjectsOfType(typeof(CustomerAI)))
        {
            if (o != this)
            {
                others.Add(o);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        prevPos = transform.position;

        if (state == CustomerState.WALKING || state == CustomerState.LEAVING) {
			move_NoPhysics ();
		}

	}

    //tells customer to follow path to its end
    public void FollowPath(List<PathNode> p) {
        state = CustomerState.WALKING;
        path = p;

        pathProgress = 0;
        next = path[pathProgress];

        //if we have a follower object, activate it now
        if (follower != null)
        {
            follower.SetActive(true);
        }
    }
    
	//tells customer to leave the way they came
	public void Leave() {
		path.Reverse ();
		FollowPath(path);
        state = CustomerState.LEAVING;
    }
        
	//moves this object's coordinates directly without any use of physics
	//strategy mainly used for debugging, but if physics are not deemed necessary may continue using
	private void move_NoPhysics() {

		//seek next
		Vector3 distance = next.transform.position - transform.position;
        Vector3 pos = transform.position; //temp variable stored new position until ready to move

        distance.y = 0;

        //the minimum distance unit must be from a node to register it as reached
        //the final node has a much stricter minimum, (cutting corners is fine, but you need to end in your chair)
        float range = nodeRange;
        if(pathProgress == path.Count-1 && state==CustomerState.WALKING){
            range = 0.1f;
        }

        //if reached next
        if (distance.magnitude < range) {

            //move on to the next pathnode, or idle if there are no nodes left
			pathProgress++;
			if (pathProgress >= path.Count) {
                if (state == CustomerState.LEAVING) //custoemr has left the building //no longer in use
                {
                    if (follower != null) //hide a follower object
                    {
                        follower.SetActive(false);
                    }

                    gameObject.SetActive(false);
                }
                state = CustomerState.IDLE;
                return;
			} else {
				next = path [pathProgress];

				distance = next.transform.position - transform.position;

				// focus on the axis with more ground to cover first
				xFocused = (distance.x * distance.x > distance.z * distance.z);

                if(state == CustomerState.LEAVING) //reverse axis priority when leaving (go out the way you came in)
                {
                    xFocused = !xFocused;
                }
			}
		}

        // check for a potential collision with another customer
        foreach (CustomerAI other in others)
        {
            if (!other.gameObject.activeSelf) { continue; } //skip inactive objects

            Vector3 dist = other.gameObject.transform.position - transform.position;
            dist.y = 0;

            //if collision is found, move away from object
            if (dist.magnitude < seperationDistance)
            {
                dist.Normalize();
          
                //if colliding object is literally on top of you, default to move away on Z-axis
                if (dist == Vector3.zero) { dist = new Vector3(0, 0, 1); }
              
                transform.position = transform.position - dist;
                return;
            }
        }

		//if haven't reached the end, or collided with anyone, move a little closer

		//move along one axis at a time
        if (xFocused) {

			pos.x += stepDistance(distance.x); //get distance traveleable in one step

			float toGo = (next.transform.position.x - pos.x);
			if (toGo*toGo < range*range) { //has covered all distance necessary on this axis
				xFocused = false;
			}
		} else { //zFocused
			pos.z += stepDistance(distance.z); //get distance traveleable in one step

			float toGo = (next.transform.position.z - pos.z);
			if (toGo*toGo < range*range) { //has covered all distance necessary on this axis
				xFocused = true;
			}
		}

        //apply new position
        transform.position = pos;
    }

    //private helper, returns the distance unit should move
    //add return value directly to coordinates
    //assumes moves along one axis
    private float stepDistance(float distance)
    {

        float tSpd = maxSpeed * Time.deltaTime;
        if (distance * distance > tSpd * tSpd)
        {
            if (distance < 0)
            {
                tSpd *= -1;
            }
            return tSpd;
        }

        return distance;
    }

    //stalls movement for one frame, used by follow when it needs to catch up
    public void stall()
    {
        transform.position = prevPos;
    }
    //Follower object, mesh that smoothly follows AI object
    public void SetFollower(GameObject f)
    {
        follower = f;
    }
}
