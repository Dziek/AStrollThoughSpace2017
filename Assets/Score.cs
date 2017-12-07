using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	
	public Text scoreText;
	
	private float currentScore;
	private float lastScore;
	private float highScore;
	
	void GameStart () {
		currentScore = 0;
	}
	
	void ScoreUp () {
		currentScore++;
	}
	
	void GameOver () {
		
		lastScore = currentScore;
		if (currentScore > highScore)
		{
			highScore = currentScore;
		}
		
		scoreText.text = "Score: " + currentScore.ToString();
	}
	
	void OnEnable () {
		Messenger.AddListener("GameStart", GameStart);
		Messenger.AddListener("ScoreUp", ScoreUp);
		Messenger.AddListener("GameOver", GameOver);
	}
	
	void OnDisable () {
		Messenger.RemoveListener("GameStart", GameStart);
		Messenger.RemoveListener("ScoreUp", ScoreUp);
		Messenger.RemoveListener("GameOver", GameOver);
	}
}
