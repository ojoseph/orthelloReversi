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
	List<string> registerIndicator = new List<string>();
	
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
					
					print("Found Token at: " + theTileNames[theRow,incre] + "  " + theField[theRow,incre] );
					
					//we are jsut looking on the right right now
					lookForAvailable(lookDirection.right ,theRow,incre, firstTkenLocation);
					lookForAvailable(lookDirection.left ,theRow,incre, firstTkenLocation);
					lookForAvailable(lookDirection.up ,theRow,incre, firstTkenLocation);
					lookForAvailable(lookDirection.down ,theRow,incre, firstTkenLocation);
					
					
					lookForAvailable(lookDirection.diagUpRight ,theRow,incre, firstTkenLocation);
					lookForAvailable(lookDirection.diagUpLeft ,theRow,incre, firstTkenLocation);
					lookForAvailable(lookDirection.diagDownRight ,theRow,incre, firstTkenLocation);
					lookForAvailable(lookDirection.diagDownLeft ,theRow,incre, firstTkenLocation);
					
					
					
					
					
					
					
					//  [ STEP 1a ]
				/*	availablePos(lookDirection.right, theField, incre, theRow, opponentSlctColor);	
					availablePos(lookDirection.left, theField, incre, theRow, opponentSlctColor);	
					availablePos(lookDirection.up, theField, incre, theRow, opponentSlctColor);	
					availablePos(lookDirection.down, theField, incre, theRow, opponentSlctColor);
					
					
					
					
					//availablePos(lookDirection.diagUpRight, theField, incre, theRow, opponentSlctColor);	
					availablePos(lookDirection.diagUpLeft, theField, incre, theRow, opponentSlctColor);	
					availablePos(lookDirection.diagDownRight, theField, incre, theRow, opponentSlctColor);	
					//availablePos(lookDirection.diagDownLeft, theField, incre, theRow, opponentSlctColor);
					*/
					
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
				
			
			int backTempVertical =  indexCaseCheckVertical;
			int backTempHorizontal = indexCaseCheckHorizontal;
		
			//OK we found a token first we need to check what is behind this token, with a while loop
			while( theField[theRow + backTempVertical ,incre + backTempHorizontal] != 0 ){
				
			if(theField[theRow + backTempVertical ,incre + backTempHorizontal] == playerSlctColor){
					break;
				}/*	else{*/
					
					
					
					
					
			//	}
			
				//We loop backward to see what is there.
				backTempVertical +=  indexCaseCheckVertical;
				backTempHorizontal += indexCaseCheckHorizontal;
				//Si c'est autre ke soit afiche moi ca.
			
				if(	theField[theRow + backTempVertical ,incre + backTempHorizontal] != 0){
				
				}else{
					print("We loop backward: " +  theTileNames[theRow + backTempVertical ,incre + backTempHorizontal] + " " + theField[theRow + backTempVertical ,incre + backTempHorizontal]);
					createToken(theTileNames[theRow + backTempVertical ,incre + backTempHorizontal]);
				}
			}
		//print("<!>Place Here: " +  theTileNames[theRow + backTempVertical ,incre + backTempHorizontal] + " " + theField[theRow + backTempVertical ,incre + backTempHorizontal]);
		///print("<!>Place Here: " +  theTileNames[theRow + backTempVertical + indexCaseCheckHorizontal ,incre + backTempHorizontal + indexCaseCheckHorizontal] + " " + theField[theRow + backTempVertical + indexCaseCheckHorizontal ,incre + backTempHorizontal + indexCaseCheckHorizontal]);
				
		
		
		
		
		
		
		
		
		
		
		
		
//			print("we are looking at: " + theTileNames[theRow + indexCaseCheckVertical ,incre + indexCaseCheckHorizontal] + "  " + theField[theRow + indexCaseCheckVertical ,incre + indexCaseCheckHorizontal] );
			
			//If the spot is empty we place a token
			if(theField[theRow + indexCaseCheckVertical ,incre + indexCaseCheckHorizontal] == 0){
				//We place a token
				//print("<Empty at: " + theTileNames[theRow + indexCaseCheckVertical ,incre + indexCaseCheckHorizontal]);
				
			}else{
				//If it is not empty we raise the scope until it is empty
				//print ("IT WAS NOT EMPTY SO WE LOOK FUTHER");
			/*
				int tempVertical =  indexCaseCheckVertical;
				int tempHorizontal = indexCaseCheckHorizontal;
					
				//We keep on looking until we find an empty space to place the token
				while(theField[theRow + tempVertical ,incre + tempHorizontal] != 0){
			
					tempVertical +=  indexCaseCheckVertical;
					tempHorizontal += indexCaseCheckHorizontal;
				
					print ("the new values for vertical: " + tempVertical);
					print ("the new values for horizontal: " + tempHorizontal);
					print("we are now looking at: " + theTileNames[theRow + tempVertical ,incre + tempHorizontal] + "  " + theField[theRow + tempVertical ,incre + tempHorizontal] );

				}//End While
			*/
			
				if(theField[theRow + indexCaseCheckVertical ,incre + indexCaseCheckHorizontal] == playerSlctColor ){
				
				}else{
					//We  move back a bit to see if its empty is so we can place a token else we do not
					/*if(theField[theRow + indexCaseCheckVertical + indexCaseCheckVertical ,incre + indexCaseCheckHorizontal + indexCaseCheckHorizontal] == 0){
						print("<!> We place a token at: " + theTileNames[theRow + tempVertical ,incre + tempHorizontal]);
			
						//<!> We create a token at that location
						//createToken(theTileNames[theRow + indexCaseCheckVertical ,incre + indexCaseCheckHorizontal]);
						createToken(theTileNames[theRow + tempVertical ,incre + tempHorizontal]);
					}else{
					
					}*/
				
					int someTempVertical =  indexCaseCheckVertical;
					int someTempHorizontal = indexCaseCheckHorizontal;
				
					while(theField[theRow + someTempVertical ,incre + someTempHorizontal] != 0){
						someTempVertical +=  indexCaseCheckVertical;
						someTempHorizontal += indexCaseCheckHorizontal;
					}
					//print ("GO ABCK A CASE " + theTileNames[theRow + someTempVertical - indexCaseCheckVertical - indexCaseCheckVertical ,incre + someTempHorizontal - indexCaseCheckVertical - indexCaseCheckVertical] +"   " +theField[theRow + someTempVertical -indexCaseCheckVertical  ,incre + someTempHorizontal -indexCaseCheckVertical]);
					//We ignore it if its a player token
					if(theField[theRow + someTempVertical - indexCaseCheckVertical  ,incre + someTempHorizontal -indexCaseCheckVertical] == playerSlctColor){
					
					}else{
						
						//print("<!> We place a token at: " + theTileNames[theRow + someTempVertical ,incre + someTempHorizontal]);
						//createToken(theTileNames[theRow + someTempVertical ,incre + someTempHorizontal]);
					}
					
				}//End if same token as player.
				

			}

				
				
		
		
	}
	
	
	//////////////////////////////////////
	//Creates the token at a target location
	void createToken(string targetLocName){
		
		
		//We get the location of where we want to put our token
		GameObject targetToken = GameObject.Find(targetLocName);
					
		//We create a token at the location of the target location.
		GameObject theIndicatorToken = Instantiate(Resources.Load("tokens/indicatorToken") ,  new Vector3(targetToken.transform.position.x,tokenHeight,targetToken.transform.position.z), transform.localRotation) as GameObject;
		//print("We put a indicator here: " + theTileNames[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]);
		
		//before giving it a name we check if there is token with the same name. If so we destroy the duplcation
		string futureTokenName ="indicator" + targetLocName;
		
		if(GameObject.Find(futureTokenName) == null){
			//If it does not exist we assign it to the token
			theIndicatorToken.name = futureTokenName;	
		}else{
		
			//if it exist we destroy the  instance token
			Destroy(GameObject.Find(futureTokenName));
		
			//We rename the new token with the proper name
			theIndicatorToken.name = futureTokenName;	
		}
		
		
		
		//We  register the indicators  we placed on the map
		registerIndicator.Add(theIndicatorToken.name);
		
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	//////////////////////////////////////
	//  [ STEP 1a ] GENERATE INDICATORS : takes care of generating indicators where the user can place his tokens.
	void availablePos(lookDirection theWantedDirection, int[,]  theField, int incre, int theRow,  int opponentSlctColor){
			
			int indexCaseCheckHorizontal = 0;
			int indexCaseCheckVertical = 0;
			
			int sega = 0;
		
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
				
			
				/*
					diagUpLeft,
					diagUpRight,
					diagDownLeft,
					diagDownRight
				*/
				
			}
			
			
		
			//if the token is the opponent's we check to see what follows by raising the scope by one
			if(theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] == opponentSlctColor){
				
				/*print("**************************************************************************************************");
				print(theTileNames[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal ] );
				print("H:    " + (indexCaseCheckHorizontal + indexCaseCheckHorizontal + indexCaseCheckHorizontal));	
				print("V:    " + (indexCaseCheckVertical + indexCaseCheckVertical + indexCaseCheckVertical));	
				print(theTileNames[theRow + (indexCaseCheckVertical + indexCaseCheckVertical + indexCaseCheckVertical), incre + (indexCaseCheckHorizontal + indexCaseCheckHorizontal + indexCaseCheckHorizontal) ] );
				print("**************************************************************************************************");
				
			
				print("++++++++++++++++++++++++++++++");
				print ("AAAA" + indexCaseCheckHorizontal + "  BBBBB" +indexCaseCheckVertical);
				print("++++++++++++++++++++++++++++++");*/
			
				/*indexCaseCheckHorizontal = indexCaseCheckHorizontal + indexCaseCheckHorizontal; 
				indexCaseCheckVertical = indexCaseCheckVertical + indexCaseCheckVertical;*/
			
			
				
				/*while(theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] != 0){
					if(theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] == opponentSlctColor){
						indexCaseCheckHorizontal += indexCaseCheckHorizontal; 
						indexCaseCheckVertical +=indexCaseCheckVertical;
					}	
				}*/
					
				indexCaseCheckHorizontal += indexCaseCheckHorizontal; 
				indexCaseCheckVertical +=indexCaseCheckVertical;
				
				while(theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] != 0){
						sega += 1;
						print("sEGA: " + sega);
						indexCaseCheckHorizontal += indexCaseCheckHorizontal; 
						indexCaseCheckVertical +=indexCaseCheckVertical;
				}
			
			
				//We check to see if it is empty, if so we add a indicator Token
				if(theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] == 0){
		
					//We get the location of where we want to put our token
					//GameObject targetToken = new GameObject();
					GameObject targetToken = GameObject.Find(theTileNames[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]);
					
									
					//We create a token at the location of the target location.
					/*GameObject*/ 
					GameObject theIndicatorToken = Instantiate(Resources.Load("tokens/indicatorToken") ,  new Vector3(targetToken.transform.position.x,tokenHeight,targetToken.transform.position.z), transform.localRotation) as GameObject;
					//print("We put a indicator here: " + theTileNames[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]);
					
					//before giving it a name we check if there is token with the same name. If so we destroy the duplcation
					string futureTokenName ="indicator" + theTileNames[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal];
					
					if(GameObject.Find(futureTokenName) == null){
						//If it does not exist we assign it to the token
						theIndicatorToken.name = futureTokenName;	
					}else{
					
						//if it exist we destroy the  instance token
						Destroy(GameObject.Find(futureTokenName));
					
						//We rename the new token with the proper name
						theIndicatorToken.name = futureTokenName;	
					}
					
					
				
					//We  register the indicators  we placed on the map
					registerIndicator.Add(theIndicatorToken.name);
					
				
				
				
				
				
					//Will be used when recovering the positions.
					//theIndicatorToken.name = theIndicatorToken.name.Replace("indicator", "");
					
				}else{
					
			
				}
				
				
		
			}//End opponentToken
			
	
	}//End  ftc available position
	
	
	
	public void removeIndicatorToken(){
		//Does it make sense?
		foreach(string tokenName in registerIndicator){
			Destroy(GameObject.Find(tokenName));
			//print(tokenName);
		}
		
		//We clean this after use	
		registerIndicator.Clear();
		
	}
	

	// Update is called once per frame
	void Update () {
	
	}
}
