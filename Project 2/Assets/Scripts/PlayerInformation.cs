using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInformation
{
    Network.Player m_player;
	Network.Actor m_playerActor;

    public void init(Network.Player player, Network.Actor actor)
    {
        m_player = player;
        m_playerActor = actor;
        player.m_evtPlayerMoved.Add(hdrPlayerMoved);
    }
    void hdrPlayerMoved(Vector3 position)
    {

    }
}

