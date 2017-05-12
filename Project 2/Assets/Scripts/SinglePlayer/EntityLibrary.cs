using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLibrary : MonoBehaviour {
    static protected EntityLibrary INSTANCE;
    List<MonoBehaviour> 
        m_players,
        m_customers,
        m_others;

    private void Awake()
    {
        INSTANCE = this;
    }

    MonoBehaviour getItemAt(List<MonoBehaviour> list, int n)
    {
        if (n < 0 || n >= list.Count)
            return null;
        return list[n];
    }
    MonoBehaviour getPlayer(int n)
    {
        return getItemAt(m_players, n);
    }
    MonoBehaviour getCustomer(int n)
    {
        return getItemAt(m_customers, n);
    }
    MonoBehaviour getOther(int n)
    {
        return getItemAt(m_others, n);
    }
    MonoBehaviour getGlobal(int n)
    {
        var player = getPlayer(n );
        if (player != null) return player;
        var customer = getCustomer(n - m_players.Count);
        if (customer != null) return customer;
        var other = getOther(n - (m_players.Count + m_customers.Count));
        if (other != null) return other;
        return null;
    }
    public static MonoBehaviour GET_PLAYER(int n)
    {
        if (INSTANCE == null) return null;
        return INSTANCE.getPlayer(n);
    }

    public static MonoBehaviour GET_CUSTOMER(int n)
    {
        if (INSTANCE == null) return null;
        return INSTANCE.getCustomer(n);
    }
    public static MonoBehaviour GET_OTHER(int n)
    {
        if (INSTANCE == null) return null;
        return INSTANCE.getOther(n);
    }
    public static MonoBehaviour GET_GLOBAL(int n)
    {
        if (INSTANCE == null) return null;
        return INSTANCE.getGlobal(n);
    }
}