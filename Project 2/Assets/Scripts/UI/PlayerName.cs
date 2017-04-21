using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerName : MonoBehaviour {
	public TextMesh m_text;

	public Transform m_transformFollow;
	// Use this for initialization
	public string Text{
		set{
			m_text.text = value;
		}
		get{
			return m_text.text;
		}
	}
	public void setTransform(Transform transform){
		m_transformFollow = transform;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (m_transformFollow == null)
			return;
		this.transform.position = m_transformFollow.position;
		
	}
}
