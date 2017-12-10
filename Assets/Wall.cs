using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
	
	public Range speed;
	public Range width;
	
	// 3.5f, 6f are good ones
	public Range wallYValues = new Range(3.5f, 6f);
	
	// what the scale of the middle bit will be when the wall is closest together
	public float minimumScale = 0.2f;
	
	// the larger the number, the smaller the gap between wall and middle bit will be
	public float scaleFactor = 0.8f;
	
	public GameObject wallTopGO;
	public GameObject wallMiddleGO;
	public GameObject wallBottomGO;
	
	private float currentSpeed;
	
	private float deathSlowDown = 2; // divides currentSpeed by this on GameOver 
	
	void OnEnable () {
		currentSpeed = Random.Range(speed.min, speed.max);
		
		transform.localScale = new Vector2(Random.Range(width.min, width.max), transform.localScale.y);
		
		float newY = Random.Range(wallYValues.min, wallYValues.max);
		// float newY = Random.Range(4, 6);
		
		wallTopGO.transform.position = new Vector2(wallTopGO.transform.position.x, newY);
		wallBottomGO.transform.position = new Vector2(wallBottomGO.transform.position.x, -newY);
		
		float scaleValue = ((newY - wallYValues.min) * scaleFactor)+ minimumScale;
		// float scaleValue = ((newY - 4) * 0.8f)+ 1;
		
		wallMiddleGO.transform.localScale = new Vector2(wallMiddleGO.transform.localScale.x, scaleValue);
		
		wallMiddleGO.GetComponent<SpriteRenderer>().enabled = true;
		
		// float translateValue = Random.Range(0, 1.5f);
		// float translateValue = 1;
		
		// wallTopGO.transform.Translate(Vector2.up * translateValue);
		// wallBottomGO.transform.Translate(Vector2.up * -translateValue);
		
		// float scaleValue = Random.Range(0.5f, 1.5f);
		// wallTopGO.transform.localScale = new Vector2(wallTopGO.transform.localScale.x, wallTopGO.transform.localScale.y * scaleValue);
		// wallBottomGO.transform.localScale = new Vector2(wallBottomGO.transform.localScale.x, wallBottomGO.transform.localScale.y * scaleValue);
		
		// float midScaleValue = 1;
		
		// if (scaleValue > 0)
		// {
			
		// }
		
		// if (scaleValue < 0)
		// {
			
		// }
		
		// wallMiddleGO.transform.localScale = new Vector2(wallMiddleGO.transform.localScale.x, wallMiddleGO.transform.localScale.y * midScaleValue);
		
		EnableMessenger();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.right * -currentSpeed * Time.deltaTime);
	}
	
	void SlowDown () {
		
		// currentSpeed = currentSpeed / deathSlowDown;
	}
	
	void EnableMessenger () {
		// Messenger.AddListener("GameStart", SpeedUpField);
		Messenger.AddListener("GameOver", SlowDown);
	}
	
	void OnDisable () {
		// Messenger.RemoveListener("GameStart", SpeedUpField);
		Messenger.RemoveListener("GameOver", SlowDown);
	}
}
