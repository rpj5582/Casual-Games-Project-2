using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    float timer = 30.0f;

	void Start () {
        //customer ai demonstration
        //Deploy 16 customers, give them 30 seconds to find their seats, 
        //then tell them to leave and send in another 16 customers simultaneously
        for(int i = 0; i < 9; i++)
        {
            CustomerManager.MoveCustomer(i, i);
        }
    }

    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            if(timer < 0) { timer = 0; }
        }

        /*
        if(timer == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                CustomerManager.RemoveCustomer(i);

                CustomerManager.MoveCustomer(i+0, i);
            }
            timer--;
        }*/
    }
}
