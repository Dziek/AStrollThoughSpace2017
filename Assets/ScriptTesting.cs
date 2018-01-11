using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTesting : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.KeypadPeriod))
		{
			StopCoroutine("SkipToLevel");
			StartCoroutine("SkipToLevel");
		}
	}
	
	IEnumerator SkipToLevel () {
		
		Debug.Log("TESTING HERE");
		// Messenger.Broadcast("GameStart");
		Messenger.Broadcast("GameStartTesting");
		
		string skipToString = "";
		
		while (!Input.GetKey(KeyCode.KeypadEnter))
		{
			for (int i = 0; i < 10; i++)
			{
				if (Input.GetKeyDown("[" + i.ToString() + "]"))
				{
					skipToString += i.ToString();
					Debug.Log(skipToString);
				}
			}
			
			yield return null;
		}
		
		int skipTo = int.Parse(skipToString);
		for (int i = 0; i < skipTo; i++)
		{
			Messenger.Broadcast("ScoreUp");
		}
		
		Messenger.Broadcast("GameOver");
	}
}
