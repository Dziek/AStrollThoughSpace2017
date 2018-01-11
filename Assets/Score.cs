using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	
	public Text scoreText;
	
	private int currentScore;
	private int lastScore;
	private int highScore;
	
	void GameStart () {
		currentScore = 0;
	}
	
	void ScoreUp () {
		currentScore++;
		// Debug.Log(currentScore);
	}
	
	void GameOver () {
		
		lastScore = currentScore;
		if (currentScore > highScore)
		{
			highScore = currentScore;
		}
		
		scoreText.text = "LAST: " + currentScore.ToString();
		Messenger<int>.Broadcast("FinalScore", currentScore);
	}
	
	void OnEnable () {
		Messenger.AddListener("GameStart", GameStart);
		Messenger.AddListener("GameStartTesting", GameStart);
		Messenger.AddListener("ScoreUp", ScoreUp);
		Messenger.AddListener("GameOver", GameOver);
	}
	
	void OnDisable () {
		Messenger.RemoveListener("GameStart", GameStart);
		Messenger.RemoveListener("GameStartTesting", GameStart);
		Messenger.RemoveListener("ScoreUp", ScoreUp);
		Messenger.RemoveListener("GameOver", GameOver);
	}
}
