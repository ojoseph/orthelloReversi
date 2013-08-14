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
	
	//set the player color to black
	int playerSlctColor = 2;
	
	int opponentSlctColor = 1;
	
	//We set the turns with this.
	int turns;
	
	//Counts the number of token for each
	int numOfTokenBlack;
	int numOfTokenWhite;
	
	
	
	//Decides how high does the token stands [FIX]
	private float tokenHeight = 0.45f; 
	
	
	//used to assign which direction we want to look at
	enum lookDirection{
		left,
		right,
		up,
		down
	}
	
	
	//.........................................................................................................................................................................................................................
	
	
	
	
	// Use this for initialization
	public void initMe () {
	
	//void Start () {	
		//[ IMPORT ]
		//We import the data from the created map 
		createMap theCreatedMap = GetComponent<createMap>();
		
		//We reassign them to here with the same name to avoid confusion.
		theField = theCreatedMap.theField;
		theTileNames = theCreatedMap.theTileNames;
		
		//We look for possible positions to move to.
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
		
		//We check the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We check each elm in that row
			for(int incre = 0; incre < 8; incre++){
			
				
				//We look for a player Token, if there is one we start looking for  possible positions where to put your token.
				if( theField[theRow,incre] == playerSlctColor){
					
					availablePos(lookDirection.right, theField, incre, theRow, opponentSlctColor);	
					availablePos(lookDirection.left, theField, incre, theRow, opponentSlctColor);	
					availablePos(lookDirection.up, theField, incre, theRow, opponentSlctColor);	
					availablePos(lookDirection.down, theField, incre, theRow, opponentSlctColor);	
					
					
				}//End if find user's token
				
				
				
				
				
				
				
				//We print the names for a test.
				//print (theTileNames[theRow,incre ]);
				
				
			}//End For
			
			
			
			
			
		//##TASK IS COMPLETE ENDS THIS SCRIPT	
		//We are done creating the map so we give the greenlight
		theCurrentStatus = currentStatus.taskComplete;
			
			
			
		
		}//End Generating 
		
		
	}
	
	
	
	
	
	
void availablePos(lookDirection theWantedDirection, int[,]  theField, int incre, int theRow,  int opponentSlctColor){

		int indexCaseCheckHorizontal = 0;
		int indexCaseCheckVertical = 0;
		
		switch(theWantedDirection){
			case lookDirection.right:
				//int nextCaseCheck  = theField[theRow,incre + 1];
				indexCaseCheckHorizontal = +1;
			break;
			case lookDirection.left:
				//int nextCaseCheck  = theField[theRow,incre - 1];
				indexCaseCheckHorizontal = -1;
			break;
			case lookDirection.up:
				//int nextCaseCheck  = theField[theRow,incre + 1];
				indexCaseCheckVertical = +1;
			break;
			case lookDirection.down:
				//int nextCaseCheck  = theField[theRow,incre - 1];
				indexCaseCheckVertical = -1;
			break;
		}
		
		
	
		//if the token is the opponent's we check too see what follows by raising the scope by one
		if( theField[theRow, incre + indexCaseCheckHorizontal] == opponentSlctColor){
			
			//We raise the scope in what so ever direction it is going.
			indexCaseCheckHorizontal += indexCaseCheckHorizontal;
	
			//We check to see if it is empty, if so we add a indicator Token
			if(theField[theRow, incre + indexCaseCheckHorizontal] == 0){
	
				//We get the location of where we want to put our token
				GameObject targetToken = new GameObject();
				targetToken = GameObject.Find(theTileNames[theRow, incre + indexCaseCheckHorizontal]);
				
				//Will serve for the Indicator Token Creation
				GameObject theIndicatorToken;
								
				//We create a token at the location of the target location.
				theIndicatorToken = Instantiate(Resources.Load("tokens/indicatorToken"),  new Vector3(targetToken.transform.position.x,tokenHeight,targetToken.transform.position.z), transform.localRotation) as GameObject;
				print("We put a indicator here: " + theTileNames[theRow, incre + indexCaseCheckHorizontal]);
								
				//break;
	
			}else{
				//If it is not empty we raise the scope and check for more info.	
	
			}
	
		}//End opponentToken
		
		
		/////////////////
		//UP DOWN
		//if the token is the opponent's we check too see what follows by raising the scope by one
		if( theField[theRow +indexCaseCheckVertical, incre] == opponentSlctColor){
			
			//We raise the scope in what so ever direction it is going.
			indexCaseCheckVertical += indexCaseCheckVertical;
	
			//We check to see if it is empty, if so we add a indicator Token
			if(theField[theRow + indexCaseCheckVertical, incre] == 0){
	
				//We get the location of where we want to put our token
				GameObject targetToken = new GameObject();
				targetToken = GameObject.Find(theTileNames[theRow + indexCaseCheckVertical, incre]);
				
				//Will serve for the Indicator Token Creation
				GameObject theIndicatorToken;
								
				//We create a token at the location of the target location.
				theIndicatorToken = Instantiate(Resources.Load("tokens/indicatorToken"),  new Vector3(targetToken.transform.position.x,tokenHeight,targetToken.transform.position.z), transform.localRotation) as GameObject;
				print("We put a indicator here: " + theTileNames[theRow + indexCaseCheckVertical, incre]);
								
				//break;
	
			}else{
				//If it is not empty we raise the scope and check for more info.	
	
			}
	
		}//End opponentToken
		
		
		
		


}
	
	
	
	
	

	// Update is called once per frame
	void Update () {
	
	}
}
