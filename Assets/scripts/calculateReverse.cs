using UnityEngine;
using System.Collections;

public class calculateReverse : MonoBehaviour {
	
	
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
	
	public bool currTurnOver = false;
	int playerSlctColor;
	
	
	//Holds the latest token
	public string newTokenLocation = "";
	
	//...........................................................................................................
	
	
	
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
		
		
		
		reverseTokens();
	
	}
	
	void reverseTokens(){
		
		
		
		//Get the number of rows
		int theNumRows = (theField.Length/8); 
		
		//We set the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We populate the rows
			for(int incre = 0; incre < 8; incre++){
				
				//Check for our tokens
				if( theField[theRow, incre] == playerSlctColor ){
					
					
					
			
					
					
					
				}	
					
				//We print the names for a test.
				print ("So I lived: " + theTileNames[theRow,incre ]);
			}
			
		}//End Generating 
		
		
		
		//We get the position of the latest token placed by the user by accessing the replaceTokenScript
		///We loopthrough each case and check the infront and behind the tokens. √
		//We check for patterns like these:   B W B  and change it for  B B B .
		
		//We also need to check for patterns like that too:   B W W W B    ->  B B B B B
		
		//Actually we need to chek for the first Token in the chain  [B]  W W W W B   until we get an empty space [_] (or  until we moved to the next row?)
		//Then, we go back one case [B]  W W W W (B)  [_]
		//Everything within this line becomes [B] :   [B]  B B B B (B)  [_]
		
		//Once complete we use the same algorithm for up and down.
	}
	
	
	
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
