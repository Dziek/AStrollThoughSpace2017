using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour {
	
	public GameObject rockGO;
	public GameObject letterGO;
	
	void OnEnable () {
		rockGO.SetActive(StoryManager.hasRock);
		letterGO.SetActive(StoryManager.hasLetter);
	}
}
