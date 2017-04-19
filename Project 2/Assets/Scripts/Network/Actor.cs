using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

namespace Network {


    public class Actor : NetworkBehaviour
    {
        //assuming that we have 60 ticks per second
        int MIN_INTERPOLATION_TICK = 2;
        const float INTERPOLATION_TIME = 1.0f/60f;


        public enum ActorState { IDL, ACTING};
        ActorState m_state = ActorState.IDL;
        List<List<Act.IAct>> m_acts = new List<List<Act.IAct>>();
        List<Act.IAct> m_actProcessed = null;
        Vector3 m_position = Vector3.zero; 


        //accessor
        ActorState STATE { get { return m_state; } }

        [ClientRpc]
        public void RpcNewPosition(Vector3 position)
        {
            //this.transform.position = position;
            //return;
            //Debug.Log("Received NEW POSITON");
            List<Act.IAct> acts = new List<Act.IAct>();
            acts.Add(new Act.MoveByTime(position, INTERPOLATION_TIME));
            m_acts.Add(acts);
        }

        [TargetRpc]
        public void TargetNewPosition(NetworkConnection conn, Vector3 position)
        {
            List<Act.IAct> acts = new List<Act.IAct>();
            acts.Add(new Act.MoveByTime(position, INTERPOLATION_TIME));

        }
        void fixedUpdateServer()
        {

            RpcNewPosition(this.transform.position);
        }
        void updateServer()
        {
        }
        bool hprGetNewAction()
        {
            if (m_acts.Count == 0) return false;
            m_actProcessed = m_acts[0];
            for (int i = m_actProcessed.Count - 1; i >= 0; i--)
            {
                m_actProcessed[i].init(this);
            }
            m_acts.RemoveAt(0);
            return true;
        }
        void hprUpdateActProcessed(List<Act.IAct> acts, float timeElapsed)
        {
            for (int i = acts.Count - 1; i >= 0; i--)
            {
                acts[i].update(this, timeElapsed);
                if (acts[i].isFinished(this))
                {
                    acts.RemoveAt(i);
                }
            }
        }
        void updateClinet(float timeElapsed)
        {

            if (m_state == ActorState.IDL)
            {
                if (m_acts.Count < MIN_INTERPOLATION_TICK) return;
                if (!hprGetNewAction()) return;
               
                m_state = ActorState.ACTING;
                return;
            }
            if (m_state == ActorState.ACTING)
            {
                hprUpdateActProcessed(m_actProcessed, timeElapsed);
                if (m_actProcessed.Count == 0)
                {
                    if (hprGetNewAction())
                    {

                    }
                    else
                    {
                        m_state = ActorState.IDL;
                    }
                }
                return;
            }

        }
        private void FixedUpdate()
        {

            if (isServer)
                fixedUpdateServer();
        }
        private void Update()
        {
            if (isServer)
                updateServer();
            else {
                //Debug.Log("Act count "+m_acts.Count);
                updateClinet(Time.deltaTime);
            }
        }
    }

}
