using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace Network
{

    public class ActorServerUpdate : MonoBehaviour
    {
        public enum UpdateMethod {GLOBAL, EXCEPT_PARTICULAR }
        const float MIN_POSITION_CHANGE = 0.01f;
        const float MIN_POSITION_CHANGE_SQUARE = MIN_POSITION_CHANGE * MIN_POSITION_CHANGE;

        Vector3 m_positionTrue;
        float m_timeBefore = 0;
        Actor m_actor;
        UpdateMethod m_updteMethod = UpdateMethod.GLOBAL;
        public List<int> m_idsExcludeFrom = new List<int>();

        public void setMethodGlobal()
        {
            m_updteMethod = UpdateMethod.GLOBAL;
        }

        public void setMethodExceptParticular(params int[] values)
        {
            m_updteMethod = UpdateMethod.EXCEPT_PARTICULAR;
            m_idsExcludeFrom.Clear();
            foreach (int v in values)
            {
                m_idsExcludeFrom.Add(v);
            }
        }

        public void init(Actor actor)
        {
            m_actor = actor;
            m_positionTrue = actor.transform.position;
        }
        // Use this for initialization
        void Start()
        {
            m_timeBefore = Time.time;

        }
        void fixedUpdateServer()
        {
            bool sendNewUpdate = false;
            if ((m_positionTrue - transform.position).sqrMagnitude > MIN_POSITION_CHANGE_SQUARE)
            {
                m_positionTrue = transform.position;
                sendNewUpdate = true;
            }
            if (sendNewUpdate)
            {
                float interpolationTime = Time.time - m_timeBefore;
                switch (m_updteMethod) {
                    case UpdateMethod.GLOBAL:
                        m_actor.RpcNewPosition(transform.position, interpolationTime);
                        break;
                    case UpdateMethod.EXCEPT_PARTICULAR:
                        for (int i = 0; i < NetworkServer.connections.Count; i++) {
                            bool isSkip = false;
                            //is this connection the one we need to skip?
                            foreach (var id in m_idsExcludeFrom)
                            {
                                if (NetworkServer.connections[i].connectionId == id)
                                {
                                    isSkip = true;
                                }
                            }
                            if (isSkip) continue;
                            if (NetworkServer.connections[i].connectionId == 0) continue;
                            m_actor.TargetNewPosition(NetworkServer.connections[i], transform.position, interpolationTime);
                        } 
                        break;
                }

            }
            m_timeBefore = Time.time;
        }
        private void FixedUpdate()
        {
            fixedUpdateServer();

        }
        // Update is called once per frame
        void Update()
        {

        }
    }

}