using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
	
	public GameObject mainMenuGO;
	public GameObject gameOverGO;
	
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
