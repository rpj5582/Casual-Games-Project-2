using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;


namespace Network.Act
{
    public class MoveByTime : IAct
    {
        Vector3 m_posFrom, m_posTo;
        float
            m_time,
            m_timeMax;
        public MoveByTime(Vector3 positionMoveTo, float timeItTakes)
        {
            m_posTo = positionMoveTo;
            m_timeMax = timeItTakes;
            m_time = 0;
        }
        public override void init(Actor actor)
        {
            base.init(actor);
            m_posFrom = actor.transform.position;
           
        }
        public override bool isFinished(Actor actor)
        {
            return m_time == m_timeMax;
        }
        public override void update(Actor actor, float timeElapsed)
        {
            m_time = Mathf.Min(m_timeMax, m_time + timeElapsed);
            float ratio = m_time / m_timeMax;
            actor.transform.position = Vector3.Lerp(m_posFrom, m_posTo, ratio);
        }

    }
}
