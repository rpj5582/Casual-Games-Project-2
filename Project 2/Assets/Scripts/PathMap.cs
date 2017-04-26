using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMap : MonoBehaviour {
	/*
	public static PathMap instance
	{
		get { 
			if (instance == null) {
				instance = new PathMap ();
			}
			return instance; 
		}
	}*/

	List<PathNode> nodes; //list of all pathnodes

	List<CustomerTable> tables;//list of all availble tables
	//index 0 is origin node

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//returns the pathnode nearest to position
	public PathNode findNearest(Vector3 position){
		PathNode toReturn = null;
		float min = float.MaxValue;

		foreach(PathNode pn in nodes){
			Vector3 delta = pn.transform.position - position;
			float dist = delta.sqrMagnitude;
			if (dist < min) {
				min = dist;
				toReturn = pn;
			}
		}

		return toReturn;
	}

	//sets table list to all table objects in scene
	//ideally this should only be called once per scene
	public void FindAllTables(){
		tables = new List<CustomerTable>( FindObjectsOfType<CustomerTable> ());
	}

	public void FindAllNodes(){
		nodes = new List<PathNode>(FindObjectsOfType<PathNode> ());
	}

	//attempts to find the origin node and set it to index 0
	//can fail if nodes have parent values set improperly of if there are multiple origins
	public void AutoGenerateOrigin(){
		for (int i = 0; i < nodes.Count; i++) {
			if (nodes [i].parentNode == null) {
				PathNode origin = nodes [i];
				nodes.RemoveAt (i);
				nodes.Insert (0, origin);
			}
		}
	}
}
