using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryScript : MonoBehaviour {
	
	public Text flavourText;
	public MenuButton continueButton;
	
	private string[,] script = new string[20,20];
	private bool[,] hasVisited = new bool[20,20];
	private int[,] locationVisitTimes = new int[20,20];
	
	private Vector2 startingPoint = Vector2.zero;
	private Vector2 journeyDirection = Vector2.right;
	
	private Dictionary<string, bool> inventoryLookUp = new Dictionary<string, bool>();
	
	public struct ScriptInstruction {
		public string condition;
		public string[] actions;
		
		// public ScriptInstruction (string c, string[] a) {
			// string condition = c;
			// string[] actions = a;
		// }
	}
	
	/*
		////SYNTAX
		# new instruction
		? condition
		| action separator
		* additional action
		
		fT= flavourText
		iP+ pick up item
		iP- put down item
		bI= button Inventory (Button Text -> Item -> updated flavourText)
	
		////CODES
		?hVL - if has Visited Location (==T is true else false)
		?iLU - if inventoryLookUp
		?cJD - check Journey Direction
	*/
	
	void Awake () {
		script[0,0] = "You Trip Over The Door";
		script[1,0] = "You Close The Gate"
						+ "#?hVL==T" + "|fT=You Close The Gate Again";
		script[2,0] = "You Take A Shortcut"
						+ "#?iLU==F -Letter" + "|fT=You Pick Up A Letter" + "*iP+Letter";
		script[3,0] = "You Walk Past The Postbox"
						+ "#?iLU==T -Letter" + "|bI=POST LETTER -> -Letter -> You Post The Letter";
		script[4,0] = "You Spot A Hidden Path"
						+ "#?cJD==up" + "|fT=You Go Up The Path"
						+ "#?cJD==right" + "|bCD=CONTINUE -> up"
						+ "#?cJD==left" + "|bCD=CONTINUE -> up";
		script[5,0] = "You See An Abandoned Shop";
						// five days after posting shop opens
		script[6,0] = "You Arrive At The Bus Stop";
						// Do bus stop stuff
		
		
		// script[4,0] = "You Go Up The Path";
		script[4,1] = "You Pass A Pond";
		script[4,2] = "You Enter Hallowed Grounds";
		script[4,3] = "You Do A Handstand";
		script[4,4] = "You Light A Fire";
		
		script[2,7] = "";
		script[3,7] = "";
		script[4,7] = "";
		script[5,7] = "";
		
		script[0,7] = "";
		script[0,8] = "";
		script[0,9] = "";
		script[0,10] = "";
		
		script[3,10] = "";
		script[4,10] = "";
		script[5,10] = "";
		
		inventoryLookUp.Add("Letter", false);
	}
	
	Vector2 newLocation;
	
	void UpdateText (int score) {
		string scriptString = "";
		
		// Get newLocation
		newLocation = startingPoint + journeyDirection * score;
		int x = (int)newLocation.x;
		int y = (int)newLocation.y;
		
		// Debug.Log(newLocation);
		
		// Get string
		scriptString = script[x, y];
		
		// Parse string
		string[] splitArray =  scriptString.Split(new string[] {"#"}, StringSplitOptions.RemoveEmptyEntries);
		string defaultText = splitArray[0];
		
		ScriptInstruction[] instructions = new ScriptInstruction[splitArray.Length-1];
		
		for (int i = 0; i < splitArray.Length-1; i++)
		{
			string[] toParse = splitArray[i+1].Split(new string[] {"|"}, StringSplitOptions.RemoveEmptyEntries);
			
			if (toParse[0].Contains("?"))
			{
				instructions[i].condition = toParse[0];
				instructions[i].actions = toParse[1].Split(new string[] {"*"}, StringSplitOptions.RemoveEmptyEntries);
			}else{
				instructions[i].condition = "";
				instructions[i].actions = toParse[0].Split(new string[] {"*"}, StringSplitOptions.RemoveEmptyEntries);
			}
		}
		
		flavourText.text = defaultText;
		
		
		
		
		
		
		
		
		
		
		// Check instructions
		
		for (int i = 0; i < instructions.Length; i++)
		{
			// visited current location
			if (instructions[i].condition.Contains("?hVL"))
			{
				bool v = instructions[i].condition.Contains("==T") ? true : false; 
				
				if (hasVisited[x, y] == v)
				{
					CheckActions(instructions[i].actions);
				}
			}
			
			// inventory look up
			if (instructions[i].condition.Contains("?iLU"))
			{
				bool v = instructions[i].condition.Contains("==T") ? true : false; 
				string itemToLookUp = instructions[i].condition.Split(new string[] {"-"}, StringSplitOptions.RemoveEmptyEntries)[1];
				
				if (inventoryLookUp[itemToLookUp] == v)
				{
					CheckActions(instructions[i].actions);
				}
			}
			
			// check Journey Direction
			if (instructions[i].condition.Contains("?cJD"))
			{
				Vector2 v = Vector2.zero;
				
				switch (instructions[i].condition.Replace("?cJD==", ""))
				{
					case "up":
						v = Vector2.up;
					break;
					case "down":
						v = Vector2.down;
					break;
					case "right":
						v = Vector2.right;
					break;
					case "left":
						v = Vector2.left;
					break;
				}
				
				if (journeyDirection == v)
				{
					CheckActions(instructions[i].actions);
				}
			}
			
			if(instructions[i].condition == "")
			{
				CheckActions(instructions[i].actions);
			}
		}
		
		
		
		// Update Text
		// flavourText.text = newText;
		
		// Update Metrics
		hasVisited[x, y] = true;
		locationVisitTimes[x, y]++;
	}
	
	void CheckActions (string[] actions) {
		for (int i = 0; i < actions.Length; i++)
		{
			if (actions[i].Contains("fT="))
			{
				flavourText.text = actions[i].Split(new string[] {"fT="}, StringSplitOptions.RemoveEmptyEntries)[0];
			}
			
			if (actions[i].Contains("iP"))
			{
				bool itemValue = actions[i].Contains("iP+") ? true : false;
				
				string separatorString = itemValue == true ? "+" : "-";
				
				string itemToChange = actions[i].Split(new string[] {separatorString}, StringSplitOptions.RemoveEmptyEntries)[1];
				inventoryLookUp[itemToChange] = itemValue;
			}
			
			if (actions[i].Contains("bI"))
			{
				string[] buttonParse = actions[i].Split(new string[] {" -> "}, StringSplitOptions.RemoveEmptyEntries);
				
				string buttonText = buttonParse[0].Replace("bI=", "");
				
				bool itemSetTo = buttonParse[1].Contains("+") ? true : false;	
				string separatorString = itemSetTo == true ? "+" : "-";
				
				string item = buttonParse[1].Split(new string[] {separatorString}, StringSplitOptions.RemoveEmptyEntries)[0];
				
				string flavourTextOnClick = buttonParse[2];
				
				// Add button
				continueButton.WakeUp();
				
				// Change button Text
				continueButton.text.text = buttonText;
				
				// Give button OnClick 
				continueButton.button.onClick.AddListener(() => InventoryOnClick(item, itemSetTo, flavourTextOnClick));
			}
			
			if (actions[i].Contains("bCD"))
			{	
				string[] buttonParse = actions[i].Split(new string[] {" -> "}, StringSplitOptions.RemoveEmptyEntries);
				
				string buttonText = buttonParse[0].Replace("bCD=", "");
				
				Vector2 newDirection = Vector2.zero;
				
				switch (buttonParse[1])
				{
					case "up":
						newDirection = Vector2.up;
					break;
					case "down":
						newDirection = Vector2.down;
					break;
					case "right":
						newDirection = Vector2.right;
					break;
					case "left":
						newDirection = Vector2.left;
					break;
				}
				
				// Add button
				continueButton.WakeUp();
				
				// Change button Text
				continueButton.text.text = buttonText;
				
				// Give button OnClick 
				continueButton.button.onClick.AddListener(() => ContinueOnClick(newDirection));
			}
		}
	}
	
	
	void InventoryOnClick (string item, bool setTo, string flavourTextOnClick) {
		inventoryLookUp[item] = setTo;
		flavourText.text = flavourTextOnClick;
		
		continueButton.ShutDown();
		continueButton.button.onClick.RemoveAllListeners();
	}
	
	void ContinueOnClick (Vector2 newDir) {
		
		startingPoint = newLocation;
		journeyDirection = newDir;
		
		continueButton.ShutDown();
		continueButton.button.onClick.RemoveAllListeners();
		
		Messenger.Broadcast("GameStart");
	}
	
	void OnEnable () {
		Messenger<int>.AddListener("FinalScore", UpdateText);
	}
	
	void OnDisable () {
		Messenger<int>.RemoveListener("FinalScore", UpdateText);
	}
}
