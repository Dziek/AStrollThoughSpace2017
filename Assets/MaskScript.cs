using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskScript : MonoBehaviour {
	
	private Transform startParent;
	private Vector2 startPos;
	private Vector2 startScale;
	
	private SpriteMask mask;
	
	void Awake () {
		startParent = transform.parent;
		startPos = transform.position;
		startScale = transform.localScale;
		
		mask = GetComponent<SpriteMask>();
	}
	
	void OnStart () {
		mask.enabled = true;
		
		transform.localScale = startScale;
	}
	
	void OnEnd () {
		mask.enabled = false;
	}
	
	void OnEnable () {
		Messenger.AddListener("GameStart", OnStart);
		Messenger.AddListener("GameOver", OnEnd);
	}
	
	void OnDisable () {
		Messenger.RemoveListener("GameStart", OnStart);
		Messenger.RemoveListener("GameOver", OnEnd);
	}
}
