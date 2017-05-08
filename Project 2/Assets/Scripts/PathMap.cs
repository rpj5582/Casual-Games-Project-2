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

    //returns list of pathnodes to given node
    public List<PathNode> GetPath(PathNode node)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(node);

        //insert all node's ancestors in list before it
        PathNode n = node;
        while (n.parentNode != null)
        {
            n = n.parentNode;
            path.Insert(0, n);
        }

        return path;
    }

    public CustomerTable GetTable(int index)
    {
        if (index < 0 || index >= tables.Count)
        {
            Debug.LogError("Invalid table index");
            return null;
        }
        return tables[index];
    }

	//get the number of occupied tables
	public int OccupiedTables{
		get { 
			int occ = 0;
			for(int i =0; i< tables.Count;i++){
				if (tables [i].occupied)
					occ++;
			}
			return occ; 
		}
	}
	//get the number of open tables
	public int OpenTables{
		get { 
			int op = 0;
			for(int i =0; i< tables.Count;i++){
				if (!tables [i].occupied)
					op++;
			}
			return op; 
		}
	}

	public CustomerTable GetRandomOpenTable(){
		int r = Random.Range (0, OpenTables);


		for (int i = 0; i < tables.Count; i++) {
			if (!tables [i].occupied) {
				r--;
				if (r == 0) {
					return tables[i];
				}
			}
		}
		//if unction returns null, there is an error in random number generation
		Debug.LogError ("Error in random number generation");
		return null; 
	}
}
