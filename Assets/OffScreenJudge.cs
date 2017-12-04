using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenJudge : MonoBehaviour {
	
	private int noOfWalls = 0;
	
	// void FixedUpdate () {
		// noOfWalls = 0;
	// }
	
	// void OnTriggerStay2D (Collider2D other) {
		// noOfWalls++;
	// }
	
	void OnTriggerEnter2D (Collider2D other) {
		noOfWalls++;
	}
	
	void OnTriggerExit2D (Collider2D other) {
		noOfWalls--;
	}
	
	void StartLimbo () {
		StartCoroutine("WaitForNoObjects");
	}
	
	IEnumerator WaitForNoObjects () {
		while (noOfWalls > 0) 
		{
			// Debug.Log(noOfWalls);
			yield return null;
		}
		
		// Debug.Log(noOfWalls + " better be equal to 0");
		Messenger.Broadcast("LimboOver");
	}
	
	void OnEnable () {
		Messenger.AddListener("GameOver", StartLimbo);
	}
	
	void OnDisable () {
		Messenger.RemoveListener("GameOver", StartLimbo);
	}
}
