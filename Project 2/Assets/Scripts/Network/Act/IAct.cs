using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;


namespace Network.Act
{
    public class IAct
    {
        public virtual void init(Actor actor)
        {

        }
        public virtual bool isFinished(Actor actor)
        {
            return true;
        }
        public virtual void update(Actor actor, float timeElapsed)
        {

        }
    }
}
