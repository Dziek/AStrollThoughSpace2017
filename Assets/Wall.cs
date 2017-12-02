using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
	
	public Range speed;
	public Range width;
	
	public float scaleFactor = 4;
	
	public GameObject wallTopGO;
	public GameObject wallMiddleGO;
	public GameObject wallBottomGO;
	
	private float currentSpeed;
	
	void OnEnable () {
		currentSpeed = Random.Range(speed.min, speed.max);
		
		transform.localScale = new Vector2(Random.Range(width.min, width.max), transform.localScale.y);
		
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
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.right * -currentSpeed);
	}
}
