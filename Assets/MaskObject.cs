using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskObject : MonoBehaviour {
	
	private Transform startParent;
	private Vector2 startPos;
	private Vector2 startScale;
	
	private SpriteMask mask;
	
	private bool following;
	
	void Awake () {
		startParent = transform.parent;
		startPos = transform.position;
		startScale = transform.localScale;
		
		mask = GetComponent<SpriteMask>();
	}
	
	void OnStart () {
		mask.enabled = true;
		
		transform.SetParent(startParent);
		transform.localScale = startScale;
		transform.position = startPos;
		
		Debug.Log("OnStart MASK " + transform.parent);
	}
	
	void OnEnd () {
		if (following == false)
		{
			mask.enabled = false;
		}
	}
	
	public void Follow (Transform toFollow) {
		transform.SetParent(toFollow);
		following = true;
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
