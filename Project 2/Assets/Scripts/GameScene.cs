using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {
	[SerializeField]
	List<Transform> m_playerSpawnPoints;
	int m_playerSpawnPointRecent = 0;

	public Vector3 getSpawnLocation(bool isRandom = false){
		return Vector3.zero;
	}
}
