using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerState
{
    IDLE, WALKING, DECIDING, WAITING, EATING, LEAVING
}
public class CustomerAI : MonoBehaviour {
    static float maxSpeed = 1.0f;

    private CustomerState state = CustomerState.IDLE;
    private List<PathNode> path;
    private int pathProgress = 0;
    private PathNode next;
    private string order = "";


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(state = CustomerState.WALKING)
        {
            //seek next
            //if reached next
                pathProgress++;
                next = path[pathProgress];
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
    public void ReversePath()
    {

    }
}
