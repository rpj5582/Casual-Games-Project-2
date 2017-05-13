using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Interactable : NetworkBehaviour
{
	public Item item;
	public Transform itemPosition;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
            Interactable myInteractable = this;
            Interactable otherInteractable = collision.gameObject.GetComponent<Interactable>();

//            RpcSwapItems(collision.gameObject, myInteractable.item, otherInteractable.item);

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

//    [ClientRpc]
//    private void RpcSwapItems(GameObject other, Item myItem, Item otherItem)
//    {
//        item = otherItem;
//        if(item)
//        {
//            item.transform.SetParent(itemPosition);
//            item.ResetTransform();
//        }
//
//        Interactable otherInteractable = other.GetComponent<Interactable>();
//        otherInteractable.item = myItem;
//        if(otherInteractable.item)
//        {
//            otherInteractable.item.transform.SetParent(otherInteractable.itemPosition);
//            otherInteractable.item.ResetTransform();
//        }
//    }

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
