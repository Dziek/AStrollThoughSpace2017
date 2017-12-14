using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskManager : MonoBehaviour {

	public GameObject childSpriteMask;
	public GameObject otherSpriteMask;
	
	public class MaskObject {
		private GameObject maskGO;
		private Transform startParent;
		private Vector2 startPos;
		private Vector2 startScale;
		
		private SpriteMask mask;
		
		private bool following;
		
		public MaskObject (GameObject go)
		{
			maskGO = go;
			
			startParent = maskGO.transform.parent;
			startPos = maskGO.transform.position;
			startScale = maskGO.transform.localScale;
		
			mask = maskGO.GetComponent<SpriteMask>();
		}
		
		public void Reset () {
			mask.enabled = true;
			following = false;
		
			maskGO.transform.SetParent(startParent);
			maskGO.transform.localScale = startScale;
			maskGO.transform.position = startPos;
		}
		
		public void End () {
			if (following == false)
			{
				mask.enabled = false;
			}
		}
		
		public void Follow (Transform toFollow) {
			maskGO.transform.SetParent(toFollow);
			following = true;
		}
	}
	
	private MaskObject childMaskObject;// = new MaskObject();
	private MaskObject otherMaskObject;// = new MaskObject();
	
	void Awake () {
		
		childMaskObject = new MaskObject(childSpriteMask);
		otherMaskObject = new MaskObject(otherSpriteMask);
	}
	
	void OnStart () {
		childMaskObject.Reset();
		otherMaskObject.Reset();
	}
	
	void OnEnd () {	
		childMaskObject.End();
		otherMaskObject.End();
	}
	
	public void Follow (Transform toFollow) {
		childMaskObject.Follow(toFollow);
		otherMaskObject.Follow(toFollow);
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
