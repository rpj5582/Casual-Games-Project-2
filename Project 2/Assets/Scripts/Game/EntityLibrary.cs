using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EntityLibrary : NetworkBehaviour {
    static protected EntityLibrary INSTANCE;

    List<GameObject> players;
    [SyncVar]
    int playerIDCount;

    [SerializeField]
    List<GameObject> 
        m_customers,
        m_items,
        m_interactables;

    private void Awake()
    {
        INSTANCE = this;
        players = new List<GameObject>();
    }

    private void Start()
    {
        // Player IDs are set in the GameNetworkManager, don't set them here.

        for (int i = 0; i < m_customers.Count; i++)
        {
            //TODO
        }

        for (int i = 0; i < m_items.Count; i++)
        {
            m_items[i].GetComponent<Item>().ID = i;
        }

        for (int i = 0; i < m_interactables.Count; i++)
        {
            m_interactables[i].GetComponent<Interactable>().ID = i;
        }

        RegisterPlayers();
    }

    public static void SendRegisterPlayersRPC()
    {
        if(INSTANCE)
            INSTANCE.RpcRegisterPlayers();
    }

    [ClientRpc]
    private void RpcRegisterPlayers()
    {
        RegisterPlayers();
    }

    private void RegisterPlayers()
    {
        players = new List<GameObject>();
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        players.Sort((item1, item2) =>
            {
                if (item1.GetComponent<Player>().ID > item2.GetComponent<Player>().ID)
                {
                    return 1;
                }
                else if (item1.GetComponent<Player>().ID == item2.GetComponent<Player>().ID)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            });

        for (int i = 0; i < players.Count; i++)
        {
            RegisterPlayer(players[i]);
        }
    }

    private void RegisterPlayer(GameObject player)
    {
        int playerID = player.GetComponent<Player>().ID;
        if (playerID < 0 || playerID >= players.Count)
            return;
        
        if (INSTANCE.m_interactables.Contains(player))
            return;

        INSTANCE.m_interactables.Add(player);
        player.GetComponent<Interactable>().ID = INSTANCE.m_interactables.Count - 1;
    }

    GameObject getObjectAt(List<GameObject> list, int n)
    {
        if (n < 0 || n >= list.Count)
            return null;
        return list[n];
    }
    GameObject getCustomer(int n)
    {
        return getObjectAt(m_customers, n);
    }
    GameObject getItem(int n)
    {
        return getObjectAt(m_items, n);
    }
    GameObject getInteractable(int n)
    {
        return getObjectAt(m_interactables, n);
    }

    public static GameObject GET_CUSTOMER(int n)
    {
        if (INSTANCE == null) return null;
        return INSTANCE.getCustomer(n);
    }
    public static GameObject GET_ITEM(int n)
    {
        if (INSTANCE == null) return null;
        return INSTANCE.getItem(n);
    }
    public static GameObject GET_INTERACTABLE(int n)
    {
        if (INSTANCE == null) return null;
        return INSTANCE.getInteractable(n);
    }
}