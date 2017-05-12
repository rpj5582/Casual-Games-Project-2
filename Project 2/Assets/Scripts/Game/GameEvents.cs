using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour {
    protected static GameEvents INSTANCE;

    public delegate void DEL_PLAYER_ASSIGNED(int playerId);
	public delegate void DEL_MY_PLAYER_MOVED(int playerId, Vector3 position, float timeTaken);
	public delegate void DEL_OTHER_PLAYER_MOVED(int playerId, Vector3 position, float timeTaken);
    public delegate void DEL_PLAYER_MOVE_INSTANT(int playerId, Vector3 position);
	public delegate void DEL_ITEM_MOVED(int itemId, int entityId);
    public delegate void DEL_CUSTOMER_MOVED(int customerId, int entityId);
	public delegate void DEL_CUSTOMER_STATE(int customerID, int state);

    public List<DEL_PLAYER_ASSIGNED> event_player_assigned = new List<DEL_PLAYER_ASSIGNED>();
	public List<DEL_MY_PLAYER_MOVED> event_my_player_moved = new List<DEL_MY_PLAYER_MOVED>();
	public List<DEL_OTHER_PLAYER_MOVED> event_other_player_moved = new List<DEL_OTHER_PLAYER_MOVED>();
	public List<DEL_PLAYER_MOVE_INSTANT> event_player_moved_instant = new List<DEL_PLAYER_MOVE_INSTANT>();
	public List<DEL_ITEM_MOVED> event_item_moved = new List<DEL_ITEM_MOVED>();
    public List<DEL_CUSTOMER_MOVED> event_customer_moved = new List<DEL_CUSTOMER_MOVED>();
	public List<DEL_CUSTOMER_STATE> event_customer_state = new List<DEL_CUSTOMER_STATE>();

    void Awake()
    {
		if (INSTANCE == null)
		{
			INSTANCE = this;
		}
    }

    //When the game starts, game assigns player entity to the player
    void mePlayerAssigned(int playerId)
    {
        for(int i = 0; i < event_player_assigned.Count; i++)
        {
            event_player_assigned[i](playerId);
        }
    }

	//Tells the server to move the player
    void myPlayerMoved(int playerId, Vector3 position, float timeTaken)
    {
		for (int i = 0; i < event_my_player_moved.Count; i++)
		{
			event_my_player_moved[i](playerId, position, timeTaken);
		}
    }
    //Game, server, told the player to move this entity to position X within T amount of time.
    void playerMove(int playerId, Vector3 position, float timeTaken)
    {
        for (int i = 0; i < event_other_player_moved.Count; i++)
        {
            event_other_player_moved[i](playerId, position, timeTaken);
        }
    }
    //Somehow game, server, told client to snap back to position  without any interpolation
    void playeMoveInstant(int playerId, Vector3 position)
    {
        for (int i = 0; i < event_player_moved_instant.Count; i++)
        {
            event_player_moved_instant[i](playerId, position);
        }
    }
    
  

    //Item A is moved onto top of entity B for some reason
    void itemMovedTo(int itemId, int entityId)
    {
		for (int i = 0; i < event_item_moved.Count; i++)
		{
			event_item_moved[i](itemId, entityId);
		}
    }

    //customer C is moved to an entity; that entity could be a chair or bench or bathroom toilet. Don't care how the movement is handled
    void customerMoved(int customerId, int entityId)
    {
        for (int i = 0; i < event_customer_moved.Count; i++)
        {
            event_customer_moved[i](customerId, entityId);
        }
    }

	//Sets the state of the customer (ie. Satisfied, Unhappy, Angry, etc.)
    void customerState(int customerId, int state)
    {
		for (int i = 0; i < event_customer_state.Count; i++)
		{
			event_customer_state[i](customerId, state);
		}
    }

    //Static methods so that you don't need to grasp the "instance"


    public static void MY_PLAYER_ASSIGNED(int playerId)
    {
        if (INSTANCE == null) return;
        INSTANCE.mePlayerAssigned(playerId);
    }

    //Tells the server to move the player
    public static void MY_PLAYER_MOVED(int playerId, Vector3 position, float timeTaken)
    {

        if (INSTANCE == null) return;
        INSTANCE.myPlayerMoved(playerId, position,timeTaken);
    }
    

    //Somehow game, server, told client to snap back to position  without any interpolation
    public static void PLAYER_MOVED_INSTANT(int playerId, Vector3 position)
    {
       
        if (INSTANCE == null) return;
        INSTANCE.playeMoveInstant(playerId, position);

    }

    //Game, server, told the player to move this entity to position X within T amount of time.
    public static void PLAYER_MOVED(int playerId, Vector3 position, float timeTaken)
    {

        if (INSTANCE == null) return;
        INSTANCE.playerMove(playerId, position, timeTaken);
        
    }

    //Item A is moved onto top of entity B for some reason
    public static void ITEM_MOVE_TO(int itemId, int entityId)
    {
        if (INSTANCE == null) return;
        INSTANCE.itemMovedTo(itemId, entityId);
    }

    //customer C is moved to an entity; that entity could be a chair or bench or bathroom toilet. Don't care how the movement is handled
    public static void CUSTOMER_MOVED_TO(int customerId, int entityId)
    {
        if (INSTANCE == null) return;
        INSTANCE.customerMoved(customerId, entityId);
    }

    //Sets the state of the customer (ie. Satisfied, Unhappy, Angry, etc.)
    public static void CUSTOMER_STATE(int customerId, int state)
    {
        if (INSTANCE == null) return;
        INSTANCE.customerState(customerId, state);
    }
}
