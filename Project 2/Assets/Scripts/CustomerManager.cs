using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour {

	private static CustomerManager instance;
	public static CustomerManager Instance
	{
		get { /*
			if (instance == null) {
				instance = new CustomerManager ();
			}*/
			return instance; 
		}
	}

	private PathMap map;
	public PathMap Map{
		get{
			
			if (Instance.map == null) {
				Instance.map = gameObject.AddComponent<PathMap> ();
				//Instance.map = new PathMap ();
				Instance.map.FindAllTables ();
				Instance.map.FindAllNodes ();
				Instance.map.AutoGenerateOrigin ();
			}
			return Instance.map;
		}
	}


	private List<List<CustomerAI>> customerGroups;

	void Awake(){

		if (instance == null) {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		//spawn customers (in groups?)
		//cluster around entrance and idle for a few seconds
		//send on path to table
		//enter deciding phase for a few seconds
		//enter ordering phase
		//wait for player input
		//enter eating phase
		//leave the way they came


	}

	public static PathMap GetPathMap(){
		return Instance.Map;
	}
}
