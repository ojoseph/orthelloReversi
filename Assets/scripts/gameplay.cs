using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	
	
	//set the player color to black
	public int playerSlctColor = 2;
	public int opponentSlctColor = 1;
	
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
		down,
		diagUpLeft,
		diagUpRight,
		diagDownLeft,
		diagDownRight
	}
	
	//Will hold the Indicator tokens that we pulled put
	public static List<string> registerIndicator = new List<string>();
	
	//.........................................................................................................................................................................................................................
	
	
	
	
	// Use this for initialization
	public void initMe () {
	
	//void Start () {	
		//[ IMPORT ]
		//We import the data from the created map 
		//createMap theCreatedMap = GetComponent<createMap>();
		
		//We reassign them to here with the same name to avoid confusion.
		theField = createMap.theField;
		theTileNames = createMap.theTileNames;
		
		//..............................................................................
		
		//We look for possible positions to move to.
		startCheckingForPosition();
	}
	
	//==========WHAT WILL U DO?===================\\
	//0. By default we will set the player to black  √
	//1. Scan the map and see where are the token   √
	//2. Get the location where we can put the tokens  √
		
	
		//> CHECK ON THE RIGHT   X _   √
 		
	
		//> CHECK ON THE LEFT  _X  √

		
		//>                                         _
		//> CHECK ON TOP             X      √
	
		
		//> CHECK ON  BOTTOM     X    √
		//>                                         _
	
		//We return the positions available.
	
	
	//////////////////////////////////////
	//  [ STEP 1 ] CHECK FOR  AVAILABLE POSITIONS : checks if there is some available positions, then place token to the available places,
	public void startCheckingForPosition(){
		//print ("CALLED ME player : " + playerSlctColor);
		//Get the number of rows
		int theNumRows = (theField.Length/8); 
		
		//We check the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We check each elm in that row
			for(int incre = 0; incre < 8; incre++){
			
				
				//We look for a player Token, if there is one we start looking for  possible positions where to put your token.
				if( theField[theRow,incre] == playerSlctColor){
					
					int firstTkenLocation = theField[theRow,incre]; 
					
					//print("Found Token at: " + theTileNames[theRow,incre] + "  " + theField[theRow,incre] );
					
					//we are jsut looking on the right right now
					lookForAvailable(lookDirection.right ,theRow,incre, firstTkenLocation);
					lookForAvailable(lookDirection.left ,theRow,incre, firstTkenLocation);
					lookForAvailable(lookDirection.up ,theRow,incre, firstTkenLocation);
					lookForAvailable(lookDirection.down ,theRow,incre, firstTkenLocation);
					
					
					lookForAvailable(lookDirection.diagUpRight ,theRow,incre, firstTkenLocation);
					lookForAvailable(lookDirection.diagUpLeft ,theRow,incre, firstTkenLocation);
					lookForAvailable(lookDirection.diagDownRight ,theRow,incre, firstTkenLocation);
					lookForAvailable(lookDirection.diagDownLeft ,theRow,incre, firstTkenLocation);
					
				}//End if find user's token
				
			}//End For
			
			
			
		//##TASK IS COMPLETE ENDS THIS SCRIPT	
		//We are done creating the map so we give the greenlight
		theCurrentStatus = currentStatus.taskComplete;
			
			
			
		
		}//End Generating 
		
		
	}//End StartChecking
	
	
	void lookForAvailable(lookDirection theWantedDirection ,int theRow, int incre, int firstTkenLocation){
		
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
				
				//Diagonal
				case lookDirection.diagUpRight:
					//int nextCaseCheck  = theField[theRow,incre - 1];
					indexCaseCheckHorizontal = +1;
					indexCaseCheckVertical = +1;
					
				break;
				case lookDirection.diagUpLeft:
					//int nextCaseCheck  = theField[theRow,incre - 1];
					indexCaseCheckHorizontal = -1;
					indexCaseCheckVertical = +1;
					
				break;
				case lookDirection.diagDownRight:
					//int nextCaseCheck  = theField[theRow,incre - 1];
					indexCaseCheckHorizontal = +1;
					indexCaseCheckVertical = -1;
					
				break;
				case lookDirection.diagDownLeft:
					//int nextCaseCheck  = theField[theRow,incre - 1];
					indexCaseCheckHorizontal = -1;
					indexCaseCheckVertical = -1;
					
				break;
			
			}
				
			
			int posTempVertical =  indexCaseCheckVertical;
			int posTempHorizontal = indexCaseCheckHorizontal;
		
			/*print ("VEC: " + (theRow + posTempVertical));
			print ("HOR: " + (incre + posTempHorizontal));*/
				
		
		
			//////////////////////////
			//<!> Safety
			//////////////////////////
			//print ();
			if((incre + posTempHorizontal) > 7 || (incre + posTempHorizontal) < 0){
				
				
				//print ("H BIGGER THAN 8");
				posTempHorizontal = 0;
			}
			
			if((theRow + posTempVertical) > 7 || (theRow + posTempVertical) < 0){
				
				//print ("V BIGGER THAN 8");
				posTempVertical = 0;
			}	
			
			//////////////////////////
			//Its a good patch
			//////////////////////////
		
		
		
		
		
		
			//OK we found a token first we need to check what is behind this token, with a while loop
			while( theField[theRow + posTempVertical ,incre + posTempHorizontal] != 0 ){
				
				if(theField[theRow + posTempVertical ,incre + posTempHorizontal] == playerSlctColor){
					break;
				}
			
				//We loop backward to see what is there.
				posTempVertical +=  indexCaseCheckVertical;
				posTempHorizontal += indexCaseCheckHorizontal;
				
				/*print ("<!> posVertical" + (theRow + posTempVertical));
				print ("<!> posHorizontal" + (incre + posTempHorizontal));	*/		
				
				
				//////////////////////////
				//<!> Safety
				//////////////////////////
				//print ();
				if((incre + posTempHorizontal) > 7 || (incre + posTempHorizontal) < 0){
					
					break;
					print ("H BIGGER THAN 8");
					posTempHorizontal = 0;
				
				}
				
				if((theRow + posTempVertical) > 7 || (theRow + posTempVertical) < 0){
					
					break;
					print ("V BIGGER THAN 8");
					posTempVertical = 0;
				
				}	
				
				//////////////////////////
				//Its a good patch
				//////////////////////////
			
			
				//If the spot is  not empty we leav it as it is.			
				if(theField[theRow + posTempVertical ,incre + posTempHorizontal] != 0){
				
				}else{
					//print("<!>Potential Pos: " +  theTileNames[theRow + posTempVertical ,incre + posTempHorizontal] + " " + theField[theRow + posTempVertical ,incre + posTempHorizontal]);
					registerToken(theTileNames[theRow + posTempVertical ,incre + posTempHorizontal]);
				}
				
			
			}//End While
		
	}
	
	 
	
	//////////////////////////////////////
	//Creates the token at a target location
	void registerToken(string targetLocName){
 
		
		//before giving it a name we check if there is token with the same name. If so we destroy the duplcation
		string futureTokenName = "indicator" + targetLocName;
		
		
		
//		print ("Going to Register: " + futureTokenName  );
		
		
		if (GameObject.Find(futureTokenName) != null){
			
//			print("does NOT exist");
			bool locationFound = false;
			foreach(string tiems in registerIndicator){
				if(tiems == futureTokenName){
					locationFound = true;
				}
			}
			
			if(locationFound == true){
				
			}else{
				
			//We get the location of where we want to put our token
			GameObject targetToken = GameObject.Find(targetLocName);
			
			//We create it
			GameObject theIndicatorToken = Instantiate(Resources.Load("tokens/indicatorToken") ,  new Vector3(targetToken.transform.position.x,tokenHeight,targetToken.transform.position.z), transform.localRotation) as GameObject;
			theIndicatorToken.name = futureTokenName;
		
			//We register the entry
			registerIndicator.Add(futureTokenName);
				
			}
		}else{
			
//			print("does NOT exist");
			
			//We get the location of where we want to put our token
			GameObject targetToken = GameObject.Find(targetLocName);
			
			//We create it
			GameObject theIndicatorToken = Instantiate(Resources.Load("tokens/indicatorToken") ,  new Vector3(targetToken.transform.position.x,tokenHeight,targetToken.transform.position.z), transform.localRotation) as GameObject;
			theIndicatorToken.name = futureTokenName;
		
			//We register the entry
			registerIndicator.Add(futureTokenName);
//			print ( "  registered: " + registerIndicator.Count);
		}

		
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	 
	
	
	public void removeIndicatorToken(){
		
		 
		//Does it make sense?
		foreach(string tokenName in registerIndicator){
			 
			Destroy(GameObject.Find(tokenName));
//			print("WE DELETE: " +tokenName);
			
		}
		
		
		//We clean this after use	
		registerIndicator.Clear();
	}
	

	// Update is called once per frame
	void Update () {
	
	}
}
