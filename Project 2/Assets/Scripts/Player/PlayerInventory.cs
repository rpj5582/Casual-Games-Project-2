using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public Item item;

    [SerializeField]
    private Transform holdPoint;
    public Transform HoldPoint { get { return holdPoint; } }
}
