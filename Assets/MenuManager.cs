using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
	
	public GameObject mainMenuGO;
	public GameObject gameOverGO;
	public GameObject inventoryGO;
	
	void ClearMenu () {
		mainMenuGO.SetActive(false);
		gameOverGO.SetActive(false);
	}
	
	void LoadGameOverMenu () {
		gameOverGO.SetActive(true);
	}
	
	public void LoadMainMenu () {
		mainMenuGO.SetActive(true);
		gameOverGO.SetActive(false);
		inventoryGO.SetActive(false);
		
		StoryManager.storyTrack = "Main";
	}
	
	public void LoadInventory () {
		inventoryGO.SetActive(true);
		mainMenuGO.SetActive(false);
	}
	
	void OnEnable () {
		Messenger.AddListener("GameStart", ClearMenu);
		Messenger.AddListener("LimboOver", LoadGameOverMenu);
		// Messenger.AddListener("GameOver", LoadGameOverMenu);
	}
	
	void OnDisable () {
		Messenger.RemoveListener("GameStart", ClearMenu);
		Messenger.RemoveListener("LimboOver", LoadGameOverMenu);
		// Messenger.RemoveListener("GameOver", LoadGameOverMenu);
	}
}
