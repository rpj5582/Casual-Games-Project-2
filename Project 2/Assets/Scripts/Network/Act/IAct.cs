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
		//return time not used
        public virtual float update(Actor actor, float timeElapsed)
        {
			return 0;
        }
    }
}
