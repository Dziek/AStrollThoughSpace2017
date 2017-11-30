using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
	
	public float speedMin = 1;
	public float speedMax = 10;
	
	public float widthMin = 0.1f;
	public float widthMax = 5f;
	
	private float currentSpeed;
	
	void OnEnable () {
		currentSpeed = Random.Range(speedMin, speedMax);
		
		transform.localScale = new Vector2(Random.Range(widthMin, widthMax), transform.localScale.y);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.right * -currentSpeed);
	}
}
