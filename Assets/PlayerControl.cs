using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float scaleUpRate = 1.1f; // 1.08f;
	public float scaleDownRate = 0.9f; // 1.94f;
	
	public GameObject childSpriteMask;
	public GameObject otherSpriteMask;
	
	// public MaskObject childMask;
	// public MaskObject otherMask;
	
	public MaskManager maskManager;
	
	public GameObject midWallDeathGO;
	public ParticleSystem shrinkGO;
	public ParticleSystem wallDeathGO;
	public ParticleSystem wallNibDeathGO;
	public ParticleSystem swallowSystem;
	public ParticleSystem nibCrumbs;
	
	private bool isDead;
	
	private Vector2 startingScale;
	
	//T
	
	private Collider2D playerCol;
	
	// private Transform touchedNib; // the nib that is currently colliding with player
	private Transform lastTouchedNib; // the nib that last collided with the player
	
	// private float maxSize = 4;
	
	private bool touchingNib = false;
	
	private Range size = new Range(0.1f, 8);
	// private Range scaleUp = new Range(2.5f, 4.5f);
	private Range scaleUp = new Range(0, 4.5f);
	private Range scaleDown = new Range(1.5f, 3);

	// Use this for initialization
	void Start () {
		Disable();
		startingScale = transform.localScale;
		
		playerCol = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead == false)
		{
			if (Input.GetMouseButton(0) || Input.GetKey("space"))
			{
				
				if (TestingMenu.testVar0 == true)
				{
					//LOTSALERPS
					float parameter = 1 - Mathf.InverseLerp(size.min, size.max, transform.localScale.x);
					float scaleRate = Mathf.Lerp(scaleUp.min, scaleUp.max, parameter) * transform.localScale.x * Time.deltaTime;
					
					transform.localScale = new Vector2(transform.localScale.x + scaleRate, transform.localScale.y + scaleRate);
				}else{
					// ORIGINAL
					transform.localScale = new Vector2(transform.localScale.x * scaleUpRate, transform.localScale.y * scaleUpRate);
				}
				
				
				//ORIGINAL
				// transform.localScale = new Vector2(transform.localScale.x * scaleUpRate, transform.localScale.y * scaleUpRate);
				
				//CAPS SIZE
				// float scaleRate = scaleUpRate * (maxSize - transform.localScale.x) * Time.deltaTime;
				// float scaleRate = scaleUpRate * transform.localScale.x * Time.deltaTime;
				
				//LOTSALERPS
				// float parameter = 1 - Mathf.InverseLerp(size.min, size.max, transform.localScale.x);
				// float scaleRate = Mathf.Lerp(scaleUp.min, scaleUp.max, parameter) * transform.localScale.x * Time.deltaTime;
				
				// transform.localScale = new Vector2(transform.localScale.x + scaleRate, transform.localScale.y + scaleRate);
			}else{
				if (TestingMenu.testVar0 == true)
				{
					//LOTSALERPS
					float parameter = 1 - Mathf.InverseLerp(size.min, size.max, transform.localScale.x);
					float scaleRate = Mathf.Lerp(scaleDown.min, scaleDown.max, parameter) * transform.localScale.x * Time.deltaTime;
					
					transform.localScale = new Vector2(transform.localScale.x - scaleRate, transform.localScale.y - scaleRate);
				}else{
					// ORIGINAL
					transform.localScale = new Vector2(transform.localScale.x * scaleDownRate, transform.localScale.y * scaleDownRate);
				}
				
				// // ORIGINAL
				// transform.localScale = new Vector2(transform.localScale.x * scaleDownRate, transform.localScale.y * scaleDownRate);
			}
			
			if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
			{
				if (touchingNib == true)
				{
					nibCrumbs.Play();
					// Messenger<float>.Broadcast("Screenshake", transform.localScale.x / 10);
				}
			}
			
			if (transform.localScale.x <= 0.05f)
			{
				// otherSpriteMask.SetActive(false);
				// childSpriteMask.SetActive(false);
				Debug.Log("Shrink Death");
				shrinkGO.Play();
				Dead();
			}
		}
	}
	
	void OnTriggerEnter2D (Collider2D other) {
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
			
			// MaskNib(other.transform);
			MaskNib();
			
			Debug.Log("Wall Death");
			
			var sh = wallDeathGO.shape;
			sh.scale = transform.localScale;
			
			var shn = wallNibDeathGO.shape;
			shn.scale = transform.localScale;
			
			if (touchingNib == true)
			{
				wallNibDeathGO.Play();
			}else{
				wallDeathGO.Play();
			}
			
			Dead();
		}
		
		if (other.gameObject.tag == "WallMiddle")
		{
			lastTouchedNib = other.transform;
			swallowSystem.Play();
		}
	}
	
	void OnTriggerStay2D (Collider2D other) {
		
		if (other.gameObject.tag == "WallMiddle")
		{
			if (transform.localScale.y < other.gameObject.transform.localScale.y && playerCol.bounds.max.x < other.bounds.max.x)
			{
				// Debug.Break();
				
				// otherSpriteMask.transform.SetParent(other.transform);
				// childSpriteMask.transform.SetParent(other.transform);
				
				childSpriteMask.transform.localScale = new Vector2(childSpriteMask.transform.localScale.x, transform.localScale.y + 1);
				
				// midWallDeathGO.transform.SetParent(null);
				// midWallDeathGO.SetActive(true);
				midWallDeathGO.GetComponent<ParticleSystem>().Play();
				
				other.GetComponent<SpriteRenderer>().enabled = false;
				
				Debug.Log("Swallow Death");
				Dead();
			}
			
			touchingNib = true;
		}
	}
	
	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "WallMiddle")
		{
			// if (transform.localScale.y > other.gameObject.transform.localScale.y && playerCol.bounds.min.x > other.bounds.max.x)
			if (playerCol.bounds.min.x > other.bounds.max.x)
			{
				if (isDead == false)
				{
					// Debug.Log("ScoreUP");
					Messenger.Broadcast("ScoreUp");
				}
			}
			
			touchingNib = false;
		}
		
		if (other.gameObject.tag == "WallMiddle")
		{
			// touchedNib = null;
			swallowSystem.Stop();
		}
	}
	
	void MaskNib () {
		
		// if (touchedNib != null)
		// {
			// otherSpriteMask.transform.SetParent(touchedNib);
			// childSpriteMask.transform.SetParent(touchedNib);
			
			// otherMask.Follow(touchedNib);
			// childMask.Follow(touchedNib);
			
			maskManager.Follow(lastTouchedNib);
			// GameObject.Find("Managers").GetComponent<MaskManager>().Follow(lastTouchedNib);
		// }
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
			// Messenger<float>.Broadcast("Screenshake", transform.localScale.x / 5);
			
			// Debug.Break();
		}
	}
	
	void Disable () {
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		gameObject.GetComponentsInChildren<Collider2D>()[0].enabled = false;
		// gameObject.GetComponentsInChildren<Collider2D>()[1].enabled = false;
		
		isDead = true;
	}
	
	void Enable () {
		gameObject.GetComponent<SpriteRenderer>().enabled = true;
		gameObject.GetComponentsInChildren<Collider2D>()[0].enabled = true;
		// gameObject.GetComponentsInChildren<Collider2D>()[1].enabled = true;
		
		isDead = false;
		touchingNib = false;
		
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
