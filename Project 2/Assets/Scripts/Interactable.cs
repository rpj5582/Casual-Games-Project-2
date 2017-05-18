using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Interactable : NetworkBehaviour
{
	public Item item;
	public Transform itemPosition;

    [SyncVar]
    private int id = -1;
    public int ID
    {
        get { return id; }
        set
        {
            if(value > -1)
                id = value;
        }
    }

    private GUI gui;

    void Start()
    {
        gui = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUI>();
    }

	void OnCollisionEnter(Collision collision)
	{
        if(isServer)
        {
            if (collision.gameObject.tag == "Player")
            {
                Interactable otherInteractable = collision.gameObject.GetComponent<Interactable>();
                if(item && otherInteractable.item)
                {
                    RpcSwapItems(otherInteractable.ID, item.ID, otherInteractable.item.ID);
                }
                else if (item && !otherInteractable.item)
                {
                    RpcSwapItems(otherInteractable.ID, item.ID, -1);
                }
                else if(!item && otherInteractable.item)
                {
                    RpcSwapItems(otherInteractable.ID, -1, otherInteractable.item.ID);
                }
//
//                PlayerInventory playerInventory = collision.gameObject.GetComponent<PlayerInventory> ();
//
//                if (item && !playerInventory.item)
//                {
//                    PickUpItem (playerInventory);
//                }
//                else if(!item && playerInventory.item)
//                {
//                    PutDownItem (playerInventory);
//                }
            }
        }
	}

    [ClientRpc]
    private void RpcSwapItems(int otherInteractableID, int myItemID, int otherItemID)
    {
        if(otherItemID > -1)
        {
            item = EntityLibrary.GET_ITEM(otherItemID).GetComponent<Item>();
            if(item)
            {
                item.transform.SetParent(itemPosition);
                item.ResetTransform();
            }
        }
        else
        {
            item = null;
        }
            
        Interactable otherInteractable = EntityLibrary.GET_INTERACTABLE(otherInteractableID).GetComponent<Interactable>();
        if(myItemID > -1)
        {
            otherInteractable.item = EntityLibrary.GET_ITEM(myItemID).GetComponent<Item>();
            if(otherInteractable.item)
            {
                otherInteractable.item.transform.SetParent(otherInteractable.itemPosition);
                otherInteractable.item.ResetTransform();
            }
        }
        else
        {
            otherInteractable.item = null;
        }

        Interactable interactable = null;
        if(gameObject.tag == "Player")
        {
            interactable = this;
        }
        else if(otherInteractable.tag == "Player")
        {
            interactable = otherInteractable;
        }

        if (interactable.item && interactable.item is OrderSlipItem)
        {
            OrderSlipItem orderSlip = (OrderSlipItem)interactable.item;
            gui.ShowOrderSlip(orderSlip.ORDER);
        }
        else if (!interactable.item)
        {
            gui.HideOrderSlip();
        }
    }

    [ClientRpc]
    public void RpcSetItem(int itemID)
    {
        this.item = EntityLibrary.GET_ITEM(itemID).GetComponent<Item>();
        item.transform.SetParent(itemPosition);
        item.ResetTransform();
    }

//	private void PickUpItem(PlayerInventory inventory)
//	{
//		inventory.item = item;
//		item.transform.SetParent (inventory.HoldPoint);
//        item.ResetTransform();
//		item = null;
//	}
//
//	private void PutDownItem(PlayerInventory inventory)
//	{
//		item = inventory.item;
//		item.transform.SetParent (itemPosition);
//		item.ResetTransform ();
//        inventory.item = null;
//	}
}
