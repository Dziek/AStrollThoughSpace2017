using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {
	
	public Image image;
	public Button button;
	public Text text;
	
	public void WakeUp () {
		image.color = Color.white;
		button.enabled = true;
		text.enabled = true;
	}
	
	public void ShutDown () {
		image.color = new Color(0.2F, 0.3F, 0.4F, 0.0F);
		button.enabled = false;
		text.enabled = false;
	}
}
