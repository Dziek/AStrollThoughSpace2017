using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float scaleUpRate = 1.1f;
	public float scaleDownRate = 0.9f;
	
	public GameObject childSpriteMask;
	public GameObject otherSpriteMask;
	
	public GameObject midWallDeathGO;
	
	private bool isDead;
	
	private Vector2 startingScale;

	// Use this for initialization
	void Start () {
		Disable();
		startingScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead == false)
		{
			if (Input.GetMouseButton(0) || Input.GetKey("space"))
			{
				transform.localScale = new Vector2(transform.localScale.x * scaleUpRate, transform.localScale.y * scaleUpRate);
			}else{
				transform.localScale = new Vector2(transform.localScale.x * scaleDownRate, transform.localScale.y * scaleDownRate);
			}
			
			if (transform.localScale.x <= 0.05f)
			{
				// otherSpriteMask.SetActive(false);
				// childSpriteMask.SetActive(false);
				Debug.Log("Shrink Death");
				Dead();
			}
		}
	}
	
	void OnTriggerStay2D (Collider2D other) {
		if (other.gameObject.tag == "Border")
		{
			// otherSpriteMask.SetActive(false);
			// childSpriteMask.SetActive(false);
			Debug.Log("Border Death");
			Dead();
		}
		
		if (other.gameObject.tag == "Wall")
		{
			// otherSpriteMask.SetActive(false);
			// childSpriteMask.SetActive(false);
			Debug.Log("Wall Death");
			Dead();
		}
		
		if (other.gameObject.tag == "WallMiddle")
		{
			if (transform.localScale.y < other.gameObject.transform.localScale.y)
			{
				// Debug.Break();
				
				// otherSpriteMask.transform.SetParent(other.transform);
				// childSpriteMask.transform.SetParent(other.transform);
				
				childSpriteMask.transform.localScale = new Vector2(childSpriteMask.transform.localScale.x, transform.localScale.y + 1);
				
				// midWallDeathGO.transform.SetParent(null);
				// midWallDeathGO.SetActive(true);
				midWallDeathGO.GetComponent<ParticleSystem>().Play();
				
				Debug.Log("Swallow Death");
				Dead();
			}
		}
	}
	
	void Dead () {
		
		if (isDead == false)
		{
			Debug.Log("OVER");
			// otherSpriteMask.SetActive(false);
			
			// gameObject.SetActive(false);
			// gameObject.GetComponent<SpriteRenderer>().enabled = false;
			// gameObject.GetComponentsInChildren<Collider2D>()[0].enabled = false;
			// gameObject.GetComponentsInChildren<Collider2D>()[1].enabled = false;
			
			// isDead = true;
			
			Disable();
			
			Messenger.Broadcast("GameOver");
			
			// Debug.Break();
		}
	}
	
	void Disable () {
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		gameObject.GetComponentsInChildren<Collider2D>()[0].enabled = false;
		gameObject.GetComponentsInChildren<Collider2D>()[1].enabled = false;
		
		isDead = true;
	}
	
	void Enable () {
		gameObject.GetComponent<SpriteRenderer>().enabled = true;
		gameObject.GetComponentsInChildren<Collider2D>()[0].enabled = true;
		gameObject.GetComponentsInChildren<Collider2D>()[1].enabled = true;
		
		isDead = false;
		
		transform.localScale = startingScale;
	}
	
	void OnEnable () {
		Messenger.AddListener("GameStart", Enable);
		// Messenger.AddListener("GameOver", StopSpawning);
	}
	
	void OnDisable () {
		Messenger.RemoveListener("GameStart", Enable);
		// Messenger.RemoveListener("GameOver", StopSpawning);
	}
}
