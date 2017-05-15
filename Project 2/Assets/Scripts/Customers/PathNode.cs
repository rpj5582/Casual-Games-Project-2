using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour {

	public PathNode parentNode = null;
    private List<GameObject> subNodes = new List<GameObject>();

	public int layer = 0; //determines how many connections this node is from origin

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
				child.parentNode = this;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
		//debug code
		if (parentNode != null) {
			Vector3 corner= transform.position;

			Vector3 dist = parentNode.transform.position - transform.position;
			if (dist.x * dist.x > dist.z * dist.z) {
				corner.z += dist.z;
			} else {
				corner.x += dist.x;
			}
			Debug.DrawLine (transform.position, corner, Color.red);
			Debug.DrawLine (corner, parentNode.transform.position, Color.red);
		}
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
