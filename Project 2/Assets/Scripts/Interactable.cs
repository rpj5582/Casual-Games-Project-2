using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	public Item item;
	public Transform itemPosition;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Inventory playerInventory = collision.gameObject.GetComponent<Inventory> ();

			if (item)
			{
				PickUpItem (playerInventory, collision.transform);
			}
			else
			{
				PutDownItem (playerInventory);
			}
		}
	}

	private void PickUpItem(Inventory inventory, Transform otherTransform)
	{
		inventory.item = item;
		item.transform.SetParent (otherTransform);
		item = null;

		Debug.Log ("Picked up item");
	}

	private void PutDownItem(Inventory inventory)
	{
		item = inventory.item;
		item.transform.SetParent (itemPosition);
		item.ResetTransform ();

		Debug.Log ("Put down item");
	}
}
