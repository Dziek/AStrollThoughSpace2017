using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHeart : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {
		if (other.name == "WallClear")
		{
			transform.parent.gameObject.SetActive(false);
		}
	}
}
