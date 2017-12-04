using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	public GameObject playerGO;
	
	public void StartGame () {
		Messenger.Broadcast("GameStart");
		
		// playerGO.Set
	}
}
