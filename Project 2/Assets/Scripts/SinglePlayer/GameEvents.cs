using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour {
    public static GameEvents ME;

    public delegate void DEL_PLAYER_ASSIGNED(int playerId);
    public delegate void DEL_PLAYER_MOVE_INSTANCE(int playerId, Vector3 position);
    public delegate void DEL_CUSTOMER_MOVED((int customerId, int entityId);
    public List<DEL_PLAYER_ASSIGNED> event_player_assigned = new List<DEL_PLAYER_ASSIGNED>();
    public List<DEL_PLAYER_MOVE_INSTANCE> event_player_moved_instance = new List<DEL_PLAYER_MOVE_INSTANCE>();
    public List<DEL_CUSTOMER_MOVED> event_customer_mvoed = new List<DEL_CUSTOMER_MOVED>();

    void Awake()
    {
        ME = this;
    }
	// Use this for initialization      
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //When the game starts, game assigns player entity to the player
    public void mePlayerAssigned(int playerId)
    {
        for(int i = 0; i < event_player_assigned.Count;i ++)
        {
            event_player_assigned[i](playerId);
        }

    }
    public void meMovedTo(int playerId, Vector3 position, float timeTaken)
    {

    }
    //Somehow game, server, told client to snap back to position  without any interpolation
    public void playeMoveInstance(int playerId, Vector3 position)
    {

        for (int i = 0; i < event_player_moved_instance.Count; i++)
        {
            event_player_moved_instance[i](playerId, position);
        }
    }
    
    //Game, server, told the player to move this entity to position X within T amount of time.
    public void playerMove(int playerId, Vector3 position, float timeTaken)
    {

    }
    //Item A is moved onto top of entity B for some reason
    public void itemMovedTo(int itemId, int entityId)
    {

    }
    //customer C is moved to an entity; that entity could be a chair or bench or bathroom toilet. Don't care how the movement is handled
    public void customerMoved(int customerId, int entityId)
    {

        for (int i = 0; i < event_customer_mvoed.Count; i++)
        {
            event_customer_mvoed[i](customerId,entityId);
        }
    }
    public void customerAction(int customerId, int state)
    {

    }
    
}
