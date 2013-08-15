using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
	int opponentSlctColor;
	
	//Holds the latest token
	public string newTokenLocation = "";
	
	
	//used to assign which direction we want to look at
	enum lookDirection{
		left,
		right,
		up,
		down
	}
	
	List<string> scannedTokensCoord = new List<string>();
	
	//...........................................................................................................
	
	
	
	// Use this for initialization
	public void initMe () {
		
		
		//[IMPORT]		
		//We get the location of where we want to put our token
		
		GameObject getGameMangerObj = GameObject.Find("gameManager");	
		
		//We import the data from the created map 
		//createMap theCreatedMap = getGameMangerObj.GetComponent<createMap>();
		
		//We reassign them to here with the same name to avoid confusion.
		int[,] theField = createMap.theField;
		string[,]  theTileNames = createMap.theTileNames;
	
		//We import the player color from the gameplay script
		playerSlctColor	= getGameMangerObj.GetComponent<gameplay>().playerSlctColor;
		opponentSlctColor  = getGameMangerObj.GetComponent<gameplay>().opponentSlctColor;
		
		
		reverseTokens(lookDirection.right);
		reverseTokens(lookDirection.left);
		reverseTokens(lookDirection.up);
		reverseTokens(lookDirection.down);
	}
	
	void reverseTokens(lookDirection theWantedDirection){
		
		
		
		// FOR  CALCULATE REVERSE
		int indexCaseCheckHorizontal = 0;
		int indexCaseCheckVertical = 0;
		
		//lookDirection theWantedDirection = lookDirection.right;
		
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
		
		
		
		
		//Get the number of rows
		int theNumRows = (theField.Length/8); 
		
		//We set the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We check each entry
			for(int incre = 0; incre < 8; incre++){
				
				//Check for  the location of the newest token
				if( theTileNames[theRow, incre] == newTokenLocation ){
					
					
					print ("We got the location of the new TOKEN " + newTokenLocation);
					
					//We start looking on its right
					if(theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] == 0){
						
						print(" Nothing on the right side!!!!");
						
					}else{
						//If there is something we check what it is
						print(" There is something: " + theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]  + "  " + theTileNames[theRow + indexCaseCheckVertical, incre +indexCaseCheckHorizontal ] );
						
						
						//If int the position we found  and opponenent token we raise the scope and look for the next position
						if(theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] == opponentSlctColor){
						
							//We raise the scope and check what is beyond that.
							//indexCaseCheckHorizontal += indexCaseCheckHorizontal;
							
							//print ("INSVESTIGATE MORE!!!  SCOPE: "  +  indexCaseCheckHorizontal);
							
							//We check until we find an empty space  || Or until we change rows.
							while(theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] != 0){
								
								
								
								print ("INSVESTIGATE MORE!!!  SCOPE: "  +  indexCaseCheckHorizontal);
								print ("Before the end: "  +  theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]  +  "    " + theTileNames[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]);
								
								
								//We check in the row we scanned to see if there is tokens that belongs to the oponent
								if(theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] == opponentSlctColor){
									print ("++++");	
									//If there is a match we add them in this table so that we can process them.
									scannedTokensCoord.Add(theTileNames[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]);
									print("We add: " + theTileNames[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]);
								
								}
								
								
								//We raise the scope and check what is beyond that.
								indexCaseCheckHorizontal += indexCaseCheckHorizontal;
								indexCaseCheckVertical += indexCaseCheckVertical;
									
								
							}//END While Loop
							
							print ("#######" + theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] + "  " + theTileNames[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]);
							
							changeColor(scannedTokensCoord);
							
							
						}
						
						//Make a while we dont reach 0
						
						
						
					}//END ELSE 
					
					
				}	
					
				//We print the names for a test.
				//print ("So I lived: " + theTileNames[theRow,incre ] + "##" + newTokenLocation);
			}
			
		}//End Generating 
		
		
		
		//We get the position of the latest token placed by the user by accessing the replaceTokenScript √
		///We loopthrough each case and check the infront and behind the tokens. √
		//We check for what is on the right if its empty we check on the left until we find an empty spot
		
		//We check for patterns like these:   B W B  and change it for  B B B .
		
		//We also need to check for patterns like that too:   B W W W B    ->  B B B B B
		
		//Actually we need to check for the first Token in the chain  [B]  W W W W B   until we get an empty space [_] (or  until we moved to the next row?) √
		//Then, we go back one case [B]  W W W W (B)  [_] √
		//Everything within this line becomes [B] :   [B]  B B B B (B)  [_] √
		
		//Once complete we use the same algorithm for up and down.√
	}
	
	
	// Update is called once per frame
	void changeColor ( List<string> scannedTokensCoord){ 
		//scannedTokensCoord
			
		
		for(int y = 0; y < scannedTokensCoord.Count; y++){
			
			print("++++***+++*+++* " + scannedTokensCoord[y]);
			
			
			//Get the number of rows
			int theNumRows = (theField.Length/8); 
		
			//We set the rows
			for(int theRow = 0 ; theRow < theNumRows; theRow++){
				
				//We populate the rows
				for(int incre = 0; incre < 8; incre++){	
					
					if(theTileNames[theRow,incre] == scannedTokensCoord[y]){
						
						//We write the data i think into the array. ?
						theField[theRow,incre] = playerSlctColor;
						
						//print ("PASSED DOWN TARGET INFO: " + opponentSlctColor + "   "  + scannedTokensCoord[y]);
						
						//We track dpwn the target.
						GameObject findTargetToReverse = GameObject.Find("token" + opponentSlctColor + scannedTokensCoord[y]);
						
						/*print ("BEFORE TARGet: " + findTargetToReverse); 
						
						print ("$$$ OpponentColor: " + opponentSlctColor + "   " + scannedTokensCoord[y]);
						print ("Target to Reverse: " + findTargetToReverse);
						print ("Location: " + theTileNames[theRow,incre] + "  What does it contain?" + theField[theRow,incre] );*/
						//We need to write the data in the array and change the name as well.
						
						
						//We change the color of the item
						switch(playerSlctColor){
							case 1:
								findTargetToReverse.transform.renderer.material.color = Color.white;
								//	findTargetToReverse.name = "token" + playerSlctColor + theTileNames[theRow,incre] ;
							break;
							case 2:
								findTargetToReverse.transform.renderer.material.color = Color.black;
							  //	findTargetToReverse.name = "token" + playerSlctColor + theTileNames[theRow,incre] ;
							break;	
							
						}
						
						//We rename the token
						findTargetToReverse.name = "token" + playerSlctColor + theTileNames[theRow,incre] ;
						
						
					}
				}
				
			}//For loop  in rows	
	
			
		}//End For loop  in aray
		
		
		
		
		scannedTokensCoord.Clear();
		
	}//End void 
	
}
