using UnityEngine;
using System.Collections;

public class replaceToken : MonoBehaviour {
	
	//Let the Game manager know when it is done
	public enum currentStatus{
		notDoneYet,
		taskComplete
	}
	
	GameObject importGameManager;
	
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
	
	
	
	//...........................................................................................................
	
	
	public bool currTurnOver = false;
	int playerSlctColor;
	
	//string newTokenLocation = "";
	
	//...........................................................................................................
	
	//GameObject someOther = new GameObject();
	
	
	// Use this for initialization
	void Start () {
	
		//[IMPORT]		
		//We get the location of where we want to put our token
		
		GameObject getGameMangerObj = GameObject.Find("gameManager");	
		
		//We import the data from the created map 
		createMap theCreatedMap = getGameMangerObj.GetComponent<createMap>();
		
		//We reassign them to here with the same name to avoid confusion.
		theField = theCreatedMap.theField;
		theTileNames = theCreatedMap.theTileNames;
	
		//We import the player color from the gameplay script
		playerSlctColor	= getGameMangerObj.GetComponent<gameplay>().playerSlctColor;
		
	}
	
	
	void OnMouseDown(){
		// Just to check if i can access the object properly.
		//this.renderer.material.color = Color.red;
		switch(playerSlctColor){
			
			case 1:
				this.renderer.material.color = Color.white;
			break;
			
			case 2:
				this.renderer.material.color = Color.black;
			break;
			
		}
		
		//We update the information in the arrays 
		updateGrid(theField, theTileNames);
		
		
		
		
		//We end its turn.
		//If the user touch the token his turn is over, unless he cant do anything.
		currTurnOver = true;
		
		
	}
	
	
	
	void updateGrid(int[,] theField,string[,] theTileNames){
		//We fetch the name of this token.
		
		//Strip down the name and keep the coordinates that was attached to its name
		//print ( "OOOOOO: " + this.name.Replace("indicator", ""));
		string theCoordianate = this.name.Replace("indicator", "");
		
		//We go through the array that contains the coordinates
		
		//Get the number of rows
		int theNumRows = (theField.Length/8); 
		
		//We set the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We populate the rows
			for(int incre = 0; incre < 8; incre++){
				
				if(theTileNames[theRow,incre ] == theCoordianate){
					//print("WE HAVE SOME MATCH");
					
					//We get access the the field array and write the data in it
					GameObject gameManagerObj = GameObject.Find("gameManager");
					createMap theCreatedMap = gameManagerObj.GetComponent<createMap>();
					theCreatedMap.theField[theRow,incre] = playerSlctColor;
					theCreatedMap.displayArray();
					
					//We change the name so that the delete function can not find the token and delete it
					//this.name = "newToken" + playerSlctColor;
					this.name = "token" + playerSlctColor + theTileNames[theRow,incre];
					
					//We remove the indicator tokens
					gameManagerObj.GetComponent<gameplay>().removeIndicatorToken();
					
					//We register the latest token we placed.
					gameManagerObj.GetComponent<calculateReverse>().newTokenLocation = theTileNames[theRow,incre];
					//newTokenLocation =  theTileNames[theRow,incre];
				}
				
				//We print the names for a test.
				//print (theTileNames[theRow,incre ]);
			}
			
		}//End Generating 
		
		
		//Try to find a match. √
		//If we find a match then we print out the information in theField (data) array   √
		//We hide/Remove the possible positions (remove Indicators) √
		//We checks which tokens needs to be reversed.
		//We reverse them in the array, the update the visuals
		//We calculate the number of tokens per players
		//We update that info on the screen.
		//We end the turn
		//We switch to the opponenent's turn.
	}
	
	
	
}
