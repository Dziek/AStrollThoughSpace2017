using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryScript : MonoBehaviour {
	
	public Text flavourText;
	
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
	
		////CODES
		?hVL - if has Visited Location (=T is true else false)
	*/
	
	void Awake () {
		script[0,0] = "You Trip Over The Door";
		script[1,0] = "You Close The Gate"
						+ "#?hVL==T" + "|fT=You Close The Gate Again";
		script[2,0] = "You Take A Shortcut"
						+ "#?iLU==F -Letter" + "|fT=You Pick Up A Letter" + "*iP+Letter";
		script[3,0] = "You Walk Past The Postbox";
		script[4,0] = "You Arrive At A Crossroads";
		script[5,0] = "You See An Abandoned Shop";
		
		
		script[4,0] = "";
		script[4,1] = "";
		script[4,2] = "";
		script[4,3] = "";
		script[4,4] = "";
		
		inventoryLookUp.Add("Letter", false);
	}
	
	void UpdateText (int score) {
		string scriptString = "";
		
		// Get newLocation
		Vector2 newLocation = startingPoint + journeyDirection * score;
		int x = (int)newLocation.x;
		int y = (int)newLocation.y;
		
		// Get string
		scriptString = script[x, y];
		
		// Parse string
		string[] splitArray =  scriptString.Split(new string[] {"#"}, StringSplitOptions.RemoveEmptyEntries);
		string defaultText = splitArray[0];
		
		ScriptInstruction[] instructions = new ScriptInstruction[splitArray.Length-1];
		
		for (int i = 0; i < splitArray.Length-1; i++)
		{
			string[] toParse = splitArray[i+1].Split(new string[] {"|"}, StringSplitOptions.RemoveEmptyEntries);
			
			instructions[i].condition = toParse[0];
			instructions[i].actions = toParse[1].Split(new string[] {"*"}, StringSplitOptions.RemoveEmptyEntries);
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
					// newText = instructions[i].actions[0];
					
					// if (instructions[i].actions[1].Contains("i"))
					// {
						// bool itemValue = instructions[i].actions[1].Contains("i+") ? true : false;
						
						// string itemToChange = instructions[i].actions[1].Split(new string[] {"+"}, StringSplitOptions.RemoveEmptyEntries)[1];
						// inventoryLookUp[itemToChange] = itemValue;
					// }
					
					CheckActions(instructions[i].actions);
				}
			}
		}
		
		
		
		// Update Text
		// flavourText.text = newText;
		
		// Update Metrics
		hasVisited[(int)newLocation.x, (int)newLocation.y] = true;
		locationVisitTimes[(int)newLocation.x, (int)newLocation.y]++;
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
				
				string itemToChange = actions[i].Split(new string[] {"+"}, StringSplitOptions.RemoveEmptyEntries)[1];
				inventoryLookUp[itemToChange] = itemValue;
			}
		}
	}
	
	void OnEnable () {
		Messenger<int>.AddListener("FinalScore", UpdateText);
	}
	
	void OnDisable () {
		Messenger<int>.RemoveListener("FinalScore", UpdateText);
	}
}
