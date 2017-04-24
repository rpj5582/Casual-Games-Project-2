using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

namespace Network {
    public class Actor : NetworkBehaviour
    {
        //assuming that we have 60 ticks per second
        int MIN_INTERPOLATION_TICK = 2;
        //const float MIN_POSITION_CHANGE = 0.001f;
        //const float MIN_POSITION_CHANGE_SQUARE = MIN_POSITION_CHANGE * MIN_POSITION_CHANGE;
        
        public enum ActorState { IDL, ACTING};
        ActorState m_state = ActorState.IDL;
        public List<List<Act.IAct>> m_acts = new List<List<Act.IAct>>();
        List<Act.IAct> m_actProcessed = null;
        //Vector3 m_position = Vector3.zero; 
        //float m_timeBeforeMoved = 0;
        

        //accessor
        ActorState STATE { get { return m_state; } }
        private void Start()
        {
        }
       
        public void processThisActImmediately(List<Act.IAct> act)
        {

        }
        public void stop()
        {
            m_state = ActorState.IDL;
            m_actProcessed = null;
            m_acts.Clear();
        }
        [ClientRpc]
		public void RpcNewPosition(Vector3 position,float time)
		{
            if (isServer) return;
            hdrNewPosition(position, time);

        }
        [TargetRpc]
        public void TargetNewPosition(NetworkConnection conn, Vector3 position, float time)
        {
            hdrNewPosition(position, time);
        }
        public void hdrNewPosition(Vector3 position, float time)
        {
            List<Act.IAct> acts = new List<Act.IAct>();
            acts.Add(new Act.MoveByTime(position, time));
            m_acts.Add(acts);

        }
        

        void fixedUpdateServer()
        {
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
			float timeLeftOver = timeElapsed;
            for (int i = acts.Count - 1; i >= 0; i--)
            {
				timeLeftOver = Mathf.Min(timeLeftOver, acts[i].update(this, timeElapsed) );
                if (acts[i].isFinished(this))
                {
                    acts.RemoveAt(i);
                }
            }
			if (timeLeftOver > 0.0001f) {
				//Debug.Log ("Extra time that's not processed " + timeLeftOver);
				if (hprGetNewAction ()) {
					hprUpdateActProcessed (m_actProcessed, timeLeftOver);
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
            updateClinet(Time.deltaTime);
            if (isServer)
                updateServer();
            else {
                //Debug.Log("Act count "+m_acts.Count);
            }
        }
    }

}
