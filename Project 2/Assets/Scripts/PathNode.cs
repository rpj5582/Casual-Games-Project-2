using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour {

	public GameObject parentNode = null;
    private List<GameObject> subNodes = new List<GameObject>();

	// Use this for initialization
	void Start () {
		//double check our parent knows we are child
		if (parentNode != null) {
			PathNode parent = parentNode.GetComponent<PathNode> ();
			if (parent.ContainsSubNode (this) == false) {
				parent.AddSubNode (this);
			}
		}

		//double check our children know we are parent
		for(int i = 0; i < subNodes.Count; i++)
		{
			PathNode child = subNodes [i].GetComponent<PathNode> ();
			if (child.parentNode == null) {
				child.parentNode = this.gameObject;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
		//debug code
		if (parentNode != null) {
			Debug.DrawLine (transform.position, parentNode.transform.position, Color.red);
		}
		/*
		for(int i = 0; i < subNodes.Count; i++)
        {
            Debug.DrawLine(transform.position, subNodes[i].transform.position, Color.red);
			if (subNodes [i].GetComponent<PathNode> ().parentNode == null) {
				subNodes [i].GetComponent<PathNode> ().parentNode = this.gameObject;
			}
        }*/
	}

	public void AddSubNode(PathNode node){
		subNodes.Add (node.gameObject);
	}

	public bool ContainsSubNode(PathNode node){
		//loop through all subnodes and return if desired node is found
		for (int i = 0; i < subNodes.Count; i++) {
			if (subNodes [i] == node) {
				return true;
			}
		}
		//end of loop, node was not found, return does not contain
		return false;
	}
}
