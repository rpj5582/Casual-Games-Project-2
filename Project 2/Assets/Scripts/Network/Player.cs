using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

namespace Network {

    public class Player : NetworkBehaviour
    {
        const float MAX_POSITION_INPUT_ERROR = .5f;
        public delegate void DEL_PLAYER_MOVED(Vector3 position);

        bool isPlayerActorReady = false;
        Network.Actor m_playerActor = null;
        Rigidbody m_playerActorBody;
        ActorClient m_playerActorClient = null;
        KeyboardInputHandler m_keyboardInputHandlr;
        MotorForce m_playerMotor;
        public List<DEL_PLAYER_MOVED> m_evtPlayerMoved = new List<DEL_PLAYER_MOVED>();
     
        void raiseEvtPlayerMoved(Vector3 position)
        {
            for (int i = m_evtPlayerMoved.Count - 1; i >= 0; i--)
            {
                m_evtPlayerMoved[i](position);
            }
        }

        [SerializeField]
        PlayerName P_PLAYER_NAME;

        bool isPinged = false;
        float m_timeElapsed = 0;



        //UI componenets


        [Command]
        public void CmdPing()
        {
            TargetPing(this.connectionToClient);
        }
        [TargetRpc]
        public void TargetPing(NetworkConnection conn)
        {
            Debug.Log("I just received my ping at time... " + m_timeElapsed);
            isPinged = false;
        }
        [TargetRpc]
        public void TargetServerTalkedTo(NetworkConnection conn)
        {
            Debug.Log("I just received my ping at time... " + m_timeElapsed);
            isPinged = false;
        }
        //used to receive the player's character that just got created from the server
        [TargetRpc]
        public void TargetSpawnActor(NetworkConnection conn, NetworkInstanceId id)
        {
            linkActor(id,true);
        }
        public void linkActor(NetworkInstanceId id, bool isThisMe = false)
        {
            var nametag = Instantiate<PlayerName>(P_PLAYER_NAME);
            var playerActor = ClientScene.FindLocalObject(id).GetComponent<Network.Actor>();

            m_playerActor = playerActor;
            m_playerMotor = m_playerActor.GetComponent<MotorForce>();
            if (isThisMe)
            {
                m_keyboardInputHandlr = new KeyboardInputHandler();
                m_playerActorClient = new ActorClient(playerActor.transform.position.x, playerActor.transform.position.y, playerActor.transform.position.z);

                nametag.setTransform(playerActor.transform);
                m_playerActorBody = m_playerActor.GetComponent<Rigidbody>();
                m_playerActorBody.isKinematic = false;
                isPlayerActorReady = true;
            }

        }

        [TargetRpc]
        public void TargetSetPosition(NetworkConnection conn, Vector3 position)
        {
            m_playerActor.transform.position = position;
        }
        /*
         * Player informed server of its new position
         * 1 Check validity of input
         * 2 Process if valid
         * 3 If invalid, stop any and all current actions then inform the player that it needs to reconsider its own action
         * */
        [Command]
        public void CmdMove(Vector3 position,float interpolationTime)
        {
            
            var positionDifference = (m_playerActor.transform.position - position).magnitude;
            if(positionDifference > MAX_POSITION_INPUT_ERROR)
            {
                //inform the player that the position is wrong
                //Stop all the actions of actor
                Debug.Log("STOP " + positionDifference);
                m_playerActor.stop();
                TargetSetPosition(connectionToClient, m_playerActor.transform.position);
                return;
            }
            //raiseEvtPlayerMoved(position);
            List<Act.IAct> actorActs = new List<Act.IAct>();
            actorActs.Add(new Act.MoveByTime(position, interpolationTime));
            m_playerActor.m_acts.Add(actorActs);

            //Debug.Log("MOVE" + position);
            //m_playerActor.transform.position = position;
            
            //otherwise process it
            //Debug.Log("Player MAG " + (m_playerActor.transform.position - position).magnitude);
            //Debug.Log("received CmdMOve " + m_playerActor.m_acts. Count + " -> ");
            //m_playerActor.processThisActImmediately();
        }
        public void FixedUpdate()
        {
            if (isPlayerActorReady)
            {
                //m_keyboardInputHandlr.update(m_playerMotor);
                if (!isServer) m_playerActorClient.kFixedUpdate(this, m_playerActor);
            }
        }
        private void Update()
        {
            if (isPlayerActorReady)
            {
                m_keyboardInputHandlr.update(m_playerMotor);
                //if (!isServer) m_playerActorClient.update(this, m_playerActor);
            }

        }
    }
}

