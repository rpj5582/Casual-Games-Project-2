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

    public GameObject customerPref;

	[SerializeField]
	List<CustomerAI> m_customers;

	private List<List<CustomerAI>> customerGroups;

	void Awake(){

		customerGroups = new List<List<CustomerAI>> ();
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

		/*
		if (customerGroups.Count == 0) {
			//Vector3 spawnPos = new Vector3(transform.position.x, 0, transform.position.y);
			CustomerAI c = Instantiate (customerPref, transform).GetComponent<CustomerAI> ();
			c.FollowPath (map.GetPath (map.GetTable (1).GetSeat (3)));

			customerGroups.Add (new List<CustomerAI> ());
			customerGroups [0].Add (c);
		}*/
	}

	public static PathMap GetPathMap(){
		return Instance.Map;
	}
	public void moveCustomer(int customerId, int chairId){
		m_customers [customerId].FollowPath (map.GetPath(map.GetTable (1).GetSeat (chairId)));
	}
	public static void MoveCustomer(int customerId, int chairId){
		if(instance != null)
			instance.moveCustomer (customerId,chairId);
	}
}
