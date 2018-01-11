using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour {
	
	public Text flavourText;
	public MenuButton continueButton;
	public MenuButton inventoryButton;
	
	public static string storyTrack = "Main";
	
	public static bool hasLetter;
	public static bool hasRock;
	
	private bool hasPosted;
	
	private int letterCountdown = 5;
	
	// private string[,] story = new string[15,15]();
	
	private string[] storyLinear = new string[]{
		"You Trip Over The Door Frame",
		"You Shut The Door Behind You",
		"You Walk Down The Steps",
		"You Wave To A Neighbour",
		"You Walk Past A Postbox", // Can post a letter here
		"You Avoid A Muddy Patch",
		"You Stop To Get Some Lunch", // Dinner on way back
		"You Walk Through A Tunnel",
		// "You Arrive At The Bus Station", // Can board bus
		"You Spy An Open Door", // Can board bus
		"You Cross The Road",
		"You Head Underground",
		"You Stumble In The Dark", // can get torch
		"You Walk Past A Waterfall",
		"You Hop Over A Fence",
		"You Pick Up A Rock",
		"You Nod To A Stranger",
		"You Reach The Top Of The Hill",
		// "You Turn Back Around"
		"You Stop Following The Map"
		
	};
	
	private string[] storySide = new string[]{
		"You Go Through The Door",
		"You Avoid Stepping On A Frog",
		"You Feel A Drip On Your Head",
		"You Slide Down A Pipe",
		"You Pick Up A Letter", 
		"You Wish You Brought A Coat",
		// "You Reach The End"
		"You Go Off The Grid"
	};
	
	private string[] storyOffGrid = new string[]{
		"You test for an echo",
		"You squint in the darkness",
		"You clap your hands",
		"You think about spaghetti",
		"You take the long way round",
		"You fall asleep on a bench",
		"You see a whale",
		"You play with a cat",
		"You tread in gum",
		"You open your favourite book",
		"You sketch a tree",
		"You jump in time to the rhythm",
		"You save a small bug",
		"You step in a puddle",
		"You hear a spooky noise",
		"You shout at a bird",
		"You tie your laces",
		"You practice winking",
		"You do a little dance",
		"You take in the sights"
	};
	
	void UpdateText (int score) {
		string newText = "";
		
		string[] storyLines = new string[1];
		
		if (storyTrack == "Main")
		{
			storyLines = storyLinear;
		}
		
		if (storyTrack == "Side")
		{
			storyLines = storySide;
		}
		
		if (score >= storyLines.Length)
		{
			newText = storyOffGrid[Random.Range(0, storyOffGrid.Length)];
		}else{
			newText = storyLines[score];
		}
		
		// flavourText.text = newText;
		
		//////////////////////////////
		// BAD STUFF LIES HERE
		//////////////////////////////
		
		if (hasPosted == true)
		{
			letterCountdown--;
			
			if (letterCountdown == 0)
			{
				newText = "You Are Approached By A Strange Man, He Thanks You For Delivering The Letter. The END.";
			}
		}
		
		if (score == 4 && storyTrack == "Main" && hasLetter == true)
		{
			newText = "You Post The Letter";
			hasPosted = true;
			hasLetter = false;
		}
		
		if (score == 4 && storyTrack == "Side")
		{
			if (hasLetter == false && hasPosted == false)
			{
				// if ()
				// {
					hasLetter = true;
					inventoryButton.WakeUp();
				// }
			}else{
				///////////
				// Add third case because can post letter and not see man and still get text
				///////////
				
				if (hasPosted == true)
				{
					newText = "You Remember The Stranger";
				}else{
					newText = "You Remember The Letter";
				}
				
				// newText = "You Remember The Letter";
			}
		}
		
		if (score == 14 && storyTrack == "Main")
		{
			if (hasRock == false)
			{
				hasRock = true;
				inventoryButton.WakeUp();
			}else{
				newText = "You Remember The Rock";
			}
		}
		
		if (score == 8 && storyTrack == "Main")
		{
			// storyTrack = "Side";
			continueButton.WakeUp();
		}else{
			continueButton.ShutDown();
		}
		
		flavourText.text = newText;
	}
	
	void OnEnable () {
		Messenger<int>.AddListener("FinalScore", UpdateText);
	}
	
	void OnDisable () {
		Messenger<int>.RemoveListener("FinalScore", UpdateText);
	}
}
