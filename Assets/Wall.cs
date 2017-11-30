using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
	
	public Range speed;
	public Range width;
	
	private float currentSpeed;
	
	void OnEnable () {
		currentSpeed = Random.Range(speed.min, speed.max);
		
		transform.localScale = new Vector2(Random.Range(width.min, width.max), transform.localScale.y);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.right * -currentSpeed);
	}
}
