using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	void Start () {
		CustomerManager.MoveCustomer (0, 0);
		CustomerManager.MoveCustomer (1, 1);
		CustomerManager.MoveCustomer (2, 2);
        CustomerManager.MoveCustomer (3, 3);
        
    }
}
