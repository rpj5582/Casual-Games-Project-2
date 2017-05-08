using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour {
    public static GameEvents Instance;

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
		if (Instance == null)
		{
			Instance = this;
		}
    }

    //When the game starts, game assigns player entity to the player
    public void mePlayerAssigned(int playerId)
    {
        for(int i = 0; i < event_player_assigned.Count; i++)
        {
            event_player_assigned[i](playerId);
        }
    }

	//Tells the server to move the player
    public void myPlayerMoved(int playerId, Vector3 position, float timeTaken)
    {
		for (int i = 0; i < event_my_player_moved.Count; i++)
		{
			event_my_player_moved[i](playerId, position, timeTaken);
		}
    }

    //Somehow game, server, told client to snap back to position  without any interpolation
    public void playeMoveInstant(int playerId, Vector3 position)
    {
        for (int i = 0; i < event_player_moved_instant.Count; i++)
        {
            event_player_moved_instant[i](playerId, position);
        }
    }
    
    //Game, server, told the player to move this entity to position X within T amount of time.
    public void otherPlayerMove(int playerId, Vector3 position, float timeTaken)
    {
		for (int i = 0; i < event_other_player_moved.Count; i++)
		{
			event_other_player_moved[i](playerId, position, timeTaken);
		}
    }

    //Item A is moved onto top of entity B for some reason
    public void itemMovedTo(int itemId, int entityId)
    {
		for (int i = 0; i < event_item_moved.Count; i++)
		{
			event_item_moved[i](itemId, entityId);
		}
    }

    //customer C is moved to an entity; that entity could be a chair or bench or bathroom toilet. Don't care how the movement is handled
    public void customerMoved(int customerId, int entityId)
    {
        for (int i = 0; i < event_customer_moved.Count; i++)
        {
            event_customer_moved[i](customerId, entityId);
        }
    }

	//Sets the state of the customer (ie. Satisfied, Unhappy, Angry, etc.)
    public void customerState(int customerId, int state)
    {
		for (int i = 0; i < event_customer_state.Count; i++)
		{
			event_customer_state[i](customerId, state);
		}
    }
}
