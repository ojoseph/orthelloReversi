using UnityEngine;
using System.Collections;

public class gameplay : MonoBehaviour {
	
	//Let the Game manager know when it is done
	public enum currentStatus{
		notDoneYet,
		taskComplete
	}
	
	
	//This process progression
	public currentStatus theCurrentStatus = currentStatus.notDoneYet;
	
	
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
	public void initMe () {
	
	//void Start () {	
		//[ IMPORT ]
		//We import the data from the created map 
		createMap theCreatedMap = GetComponent<createMap>();
		
		//We reassign them to here with the same name to avoid confusion.
		theField = theCreatedMap.theField;
		theTileNames = theCreatedMap.theTileNames;
		
		
		startCheckingForPosition();
	}
	
	//==========WHAT WILL U DO?===================\\
	//0. By default we will set the player to black
	//1. Scan the map and see where are the token
	//2. Get the location where we can put the tokens
		//> CHECK ON THE RIGHT   X _
 		//> CHECK ON THE LEFT  _X
		
	
		//> CHECK ON THE LEFT  _X
		
		//>                                         _
		//> CHECK ON TOP             X
	
		
		//> CHECK ON  BOTTOM     X
		//>                                         _
	
		//We return the positions avaialbele
	
	
	
	void startCheckingForPosition(){
		
		//Get the number of rows
		int theNumRows = (theField.Length/8); 
		
		//We set the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We populate the rows
			for(int incre = 0; incre < 8; incre++){
			
				print(theField[theRow,incre]);
				
				//We print the names for a test.
				//print (theTileNames[theRow,incre ]);
			}
			
		}//End Generating 
		
		//We are done creating the map so we give the greenlight
		theCurrentStatus = currentStatus.taskComplete;
	}
	
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
