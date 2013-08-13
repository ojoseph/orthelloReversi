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
		
		//We set the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We populate the rows
			for(int incre = 0; incre < 8; incre++){
			
				print(theField[theRow,incre]);
				if( theField[theRow,incre] == playerSlctColor){
					//If there is a match  we start looking for what is around
					int nextOnRight = theField[theRow,incre+1];
					
					int prevOnLeft = theField[theRow,incre-1];
					
					
					//CHECK ON RIGHT [0, +1]
						print("check one after : " + nextOnRight + "   " + theTileNames[theRow,incre+1]);
					
					//CHECK ON LEFT [-1, 0]
						print("check one before: " + prevOnLeft + "   " + theTileNames[theRow,incre-1]);
					
					//if its the opponent's token, than we check for an  opporunity
					//We check to see what follows it. +1
					if(nextOnRight == opponentSlctColor){
						//We raise the scope
						print("We check the next next: " + theField[theRow,incre+2] + "   " + theTileNames[theRow,incre+2]);
						
						//If its empty we put an indicator
						if(theField[theRow,incre+2] == 0){
							
							//We get the location of where we want to put our token
							GameObject targetToken = new GameObject();
							targetToken = GameObject.Find(theTileNames[theRow,incre+2]);
						
							
							//We put a indicator Token
							
							//Will serve for the Indicator Token Creation
							GameObject theIndicatorToken;
							
							//We create a token at the location of the target location.
							theIndicatorToken = Instantiate(Resources.Load("tokens/indicatorToken"),  new Vector3(targetToken.transform.position.x,tokenHeight,targetToken.transform.position.z), transform.localRotation) as GameObject;
							print("We put a indicator here: " + theTileNames[theRow,incre+2]);
							
							break;
						}else{
							
							//if not we relaunch the process.
						}
					}
					
				}//End if
				
				//We print the names for a test.
				//print (theTileNames[theRow,incre ]);
				
				
			}//End For
			
			
		//##TASK IS COMPLETE ENDS THIS SCRIPT	
		//We are done creating the map so we give the greenlight
		theCurrentStatus = currentStatus.taskComplete;
			
			
			
		
		}//End Generating 
		
		
	}
	

	// Update is called once per frame
	void Update () {
	
	}
}
