using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;


namespace Network.Act
{
    public class MoveByMotorForce : IAct
    {
        MotorForce m_motor;
        Vector3 m_posTo;
        public MoveByMotorForce(Vector3 positionMoveTo, MotorForce motor)
        {
            m_posTo = positionMoveTo;
            m_motor = motor;
        }
        public override void init(Actor actor)
        {
            base.init(actor);

        }
        public override bool isFinished(Actor actor)
        {
            return (m_posTo - actor.transform.position).magnitude < 0.3f;
        }
        public override float update(Actor actor, float timeElapsed)
        {
            m_motor.m_direction = (m_posTo - actor.transform.position).normalized;
            //Debug.Log(m_posTo + " , " +m_motor.m_direction);
            if (isFinished(actor))
            {

                m_motor.m_direction = Vector3.zero;
            }
            return 0;
        }

    }
}
