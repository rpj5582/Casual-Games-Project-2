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
			PlayerInventory playerInventory = collision.gameObject.GetComponent<PlayerInventory> ();

			if (item && !playerInventory.item)
			{
				PickUpItem (playerInventory);
			}
			else if(!item && playerInventory.item)
			{
				PutDownItem (playerInventory);
			}
		}
	}

	private void PickUpItem(PlayerInventory inventory)
	{
		inventory.item = item;
		item.transform.SetParent (inventory.HoldPoint);
        item.ResetTransform();
		item = null;
	}

	private void PutDownItem(PlayerInventory inventory)
	{
		item = inventory.item;
		item.transform.SetParent (itemPosition);
		item.ResetTransform ();
        inventory.item = null;
	}
}
