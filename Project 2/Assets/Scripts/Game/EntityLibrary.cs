using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLibrary : MonoBehaviour {
    static protected EntityLibrary INSTANCE;
    [SerializeField]
    List<GameObject> 
        m_players,
        m_customers,
        m_others;

    private void Awake()
    {
        INSTANCE = this;
    }

    GameObject getItemAt(List<GameObject> list, int n)
    {
        if (n < 0 || n >= list.Count)
            return null;
        return list[n];
    }
    GameObject getPlayer(int n)
    {
        return getItemAt(m_players, n);
    }
    GameObject getCustomer(int n)
    {
        return getItemAt(m_customers, n);
    }
    GameObject getOther(int n)
    {
        return getItemAt(m_others, n);
    }
    GameObject getGlobal(int n)
    {
        var player = getPlayer(n );
        if (player != null) return player;
        var customer = getCustomer(n - m_players.Count);
        if (customer != null) return customer;
        var other = getOther(n - (m_players.Count + m_customers.Count));
        if (other != null) return other;
        return null;
    }
    public static GameObject GET_PLAYER(int n)
    {
        if (INSTANCE == null) return null;
        return INSTANCE.getPlayer(n);
    }

    public static GameObject GET_CUSTOMER(int n)
    {
        if (INSTANCE == null) return null;
        return INSTANCE.getCustomer(n);
    }
    public static GameObject GET_OTHER(int n)
    {
        if (INSTANCE == null) return null;
        return INSTANCE.getOther(n);
    }
    public static GameObject GET_GLOBAL(int n)
    {
        if (INSTANCE == null) return null;
        return INSTANCE.getGlobal(n);
    }
}