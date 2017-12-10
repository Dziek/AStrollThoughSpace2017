using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingMenu : MonoBehaviour {

	public static bool testVar0 = true;
	
	public void ChangeTestVar0 (Button button) {
		testVar0 = !testVar0;
		
		Text buttonText = button.GetComponentInChildren<Text>();
		
		buttonText.text = "TV0: " + testVar0.ToString();
	}
}
