using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Order
{
    SPAGHETTI,
    HOTDOG,
    DRINK
}

public class OrderSlipItem : Item
{
    private Order order;
    public Order ORDER { get { return order; } }

    protected override void Start()
    {
        base.Start();

        float rand = Random.value;
        if (rand > 0.4f)
        {
            order = Order.SPAGHETTI;
        }
        else if(rand > 0.8f)
        {
            order = Order.HOTDOG;
        }
        else
        {
            order = Order.DRINK;
        }
    }
}
