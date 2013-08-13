using UnityEngine;
using System.Collections;

public class gameplay : MonoBehaviour {
	
	//Let the Game manager know when it is done
	public enum currentStatus{
		notDoneYet,
		taskComplete
	}
	
	
	// A. 2D array of ints that will be replaced by what Created Map has done
	public int[,] theField = new int[,]
	{
	    {0}, //A
	};
	
	// A. 2D array of strings that will be replaced by what Created Map has done
	public string[,] theTileNames = new string[,]{
	    {""}, //A
	};
	
	
	//The player's color
	enum playerColor{
		black,
		white
	}
	
	
	//We set the turns with this.
	int turns;
	
	//Counts the number of token for each
	int numOfTokenBlack;
	int numOfTokenWhite;
	
	// Use this for initialization
	void Start () {
		
		//[ IMPORT ]
		//We import the data from the created map 
		createMap theCreatedMap = GetComponent<createMap>();
		
		//We reassign them to here with the same name to avoid confusion.
		theField = theCreatedMap.theField;
		theTileNames = theCreatedMap.theTileNames;
		
	}
	
	
	//==========WHAT WILL U DO?===================\\
	//0. By default we will set the player to black
	//1. Scan the map and see where are the token
	//2. Get the location where we can put the tokens
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
