using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	public GameObject item;

	void Start()
	{
		
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if (item)
			{
				item.transform.SetParent (collision.transform);
				item = null;
			}
			else
			{
				Inventory playerInventory = collision.gameObject.GetComponent<Inventory> ();
				if (playerInventory)
				{
					item = playerInventory.item;
					item.transform.SetParent (transform);
				}
			}
		}
	}
}
