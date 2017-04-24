using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network{

    public class ActorClient
    {
        const float MIN_POSITION_CHANGE = 0.01f;
        const float MIN_POSITION_CHANGE_SQUARE = MIN_POSITION_CHANGE * MIN_POSITION_CHANGE;

        float m_time;
        public Vector3 m_positionTrue;
        public ActorClient(float x = 0, float y= 0, float z = 0)
        {
            m_time = Time.time;
            m_positionTrue = new Vector3(x, y, z);
        }
       
        public void kFixedUpdate(Player player, Network.Actor actor)
        {
            float magnitude = (actor.transform.position - m_positionTrue).sqrMagnitude;


            if (magnitude < MIN_POSITION_CHANGE_SQUARE) { m_time = Time.time;  return; }
            float timeElasped = Time.time - m_time;
            Debug.Log("SENDING UPDATE TO SERVER");
            m_positionTrue = actor.transform.position;
            player.CmdMove(m_positionTrue, timeElasped);
            m_time = Time.time;

        }
    }

}