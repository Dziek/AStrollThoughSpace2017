using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	public GameObject playerGO;
	
	public void StartGame () {
		Messenger.Broadcast("GameStart");
		
		// playerGO.Set
	}
	
	public void ChangeTrackStartGame () {
		StoryManager.storyTrack = "Side";
		
		Messenger.Broadcast("GameStart");
	}
}
