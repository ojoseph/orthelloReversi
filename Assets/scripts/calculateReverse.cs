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
	 int[,] theField = new int[,]
	{
	    {0}, //A
	};
	
	// A. 2D array of strings that will be replaced by what Created Map has done
	 string[,] theTileNames = new string[,]{
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
		down,
		diagUpLeft,
		diagUpRight,
		diagDownLeft,
		diagDownRight
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
		theField = createMap.theField;
		theTileNames = createMap.theTileNames;
	
		//We import the player color from the gameplay script
		playerSlctColor	= getGameMangerObj.GetComponent<gameplay>().playerSlctColor;
		opponentSlctColor  = getGameMangerObj.GetComponent<gameplay>().opponentSlctColor;
		
		
		reverseTokens(lookDirection.right);
		reverseTokens(lookDirection.left);
		reverseTokens(lookDirection.up);
		reverseTokens(lookDirection.down);
		
		
		reverseTokens(lookDirection.diagUpRight);
		reverseTokens(lookDirection.diagUpLeft);
		
		reverseTokens(lookDirection.diagDownRight);
		reverseTokens(lookDirection.diagDownLeft);
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
		
		
		
		
		//Get the number of rows
		int theNumRows = (theField.Length/8); 
		
		//We set the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We check each entry
			for(int incre = 0; incre < 8; incre++){
				
				//Check for  the location of the newest token
				if(theTileNames[theRow, incre] == newTokenLocation){
					
					//print ("We got the location of the new TOKEN " + newTokenLocation);
					
					//We start looking on its right
					if(theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] == 0){
						
						print(" Nothing on the " + theWantedDirection+"  side!!!! " + theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]  + "  " + theTileNames[theRow + indexCaseCheckVertical, incre +indexCaseCheckHorizontal ] );
						
					}else{
						//If there is something we check what it is
						print(" There is something: " + theWantedDirection+"  side!!!!  " + theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]  + "  " + theTileNames[theRow + indexCaseCheckVertical, incre +indexCaseCheckHorizontal ] );
						
						
						//If int the position we found  and opponenent token we raise the scope and look for the next position
						if(theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] == opponentSlctColor){
							
							int pullTempVertical =  indexCaseCheckVertical + indexCaseCheckVertical;
							int pullTempHorizontal = indexCaseCheckHorizontal + indexCaseCheckHorizontal;
							
							int secPullTempVertical =  indexCaseCheckVertical + indexCaseCheckVertical;
							int secPullTempHorizontal = indexCaseCheckHorizontal + indexCaseCheckHorizontal;
							
							
							while(theField[theRow + pullTempVertical, incre + pullTempHorizontal] != 0){
								//We check if tis the same color as the current player
								if(theField[theRow + pullTempVertical, incre + pullTempHorizontal]  == playerSlctColor){
									
									//We pull out the last Token info until none is left
									print ("<X> pull me out: " + theField[theRow + pullTempVertical, incre + pullTempHorizontal]  + "  " + theTileNames[theRow + pullTempVertical, incre +pullTempHorizontal ]);
									
									/*while(theField[theRow + secPullTempVertical, incre + secPullTempHorizontal] != 0){
									
										//We pull out the last Token info until none is left
										print ("<Y> Loop: " + theField[theRow + secPullTempVertical, incre + secPullTempHorizontal]  + "  " + theTileNames[theRow + secPullTempVertical, incre +secPullTempHorizontal ]);
										
										//We raise the scope and check what is beyond that.
										secPullTempVertical += indexCaseCheckHorizontal;
										secPullTempHorizontal += indexCaseCheckVertical;
									}*/
									
								}
								
								//We raise the scope and check what is beyond that.
								pullTempHorizontal += indexCaseCheckHorizontal;
								pullTempVertical += indexCaseCheckVertical;
							}
							
							
							
							
							
							
							//We raise the scope and check what is beyond that.
							//indexCaseCheckHorizontal += indexCaseCheckHorizontal;
							
							print ("INSVESTIGATE MORE!!!  SCOPE: "  +  indexCaseCheckHorizontal);
							
							//We check until we find an empty space  || Or until we change rows.
							while(theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] != 0){
								
									while(theField[theRow + secPullTempVertical, incre + secPullTempHorizontal] == opponentSlctColor){
									
										//We pull out the last Token info until none is left
										print ("<R> Loop: " + theField[theRow + secPullTempVertical, incre + secPullTempHorizontal]  + "  " + theTileNames[theRow + secPullTempVertical, incre +secPullTempHorizontal ]);
										
									
										//We raise the scope and check what is beyond that.
										secPullTempVertical += indexCaseCheckHorizontal;
										secPullTempHorizontal += indexCaseCheckVertical;
									}
							
								
								
								
								
								
								
								
								
								
								//We check in the row we scanned to see if there is tokens that belongs to the oponent
								if(theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] == opponentSlctColor){
								
									
									//We check if the token that follows the opponent's token is empty, if so we do nothing, else we take it.
									int tempVertical =  indexCaseCheckVertical + indexCaseCheckVertical;
									int tempHorizontal = indexCaseCheckHorizontal + indexCaseCheckHorizontal;
									
									//print ("SCOPE SIGHT: H" + tempHorizontal + "   V" + tempVertical);
									//print ("FUTURE SCOPE: " + theField[theRow + tempVertical, incre + tempHorizontal]  +  "    " + theTileNames[theRow + tempVertical, incre + tempHorizontal]);
									
									if(theField[theRow + tempVertical, incre + tempHorizontal] == 0 || theField[theRow + tempVertical, incre + tempHorizontal] != playerSlctColor ){
										//	print("<!> We found that the Future position is empty");
										//	print ("FUTURE POS: " + theField[theRow + tempVertical, incre + tempHorizontal]  +  "    " + theTileNames[theRow + tempVertical, incre + tempHorizontal]);
									}else{
										
										
										
										
										//print("We add: " + theTileNames[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]);
										scannedTokensCoord.Add(theTileNames[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]);
										
									}
									
								
								
								}
								
								
								//We raise the scope and check what is beyond that.
								indexCaseCheckHorizontal += indexCaseCheckHorizontal;
								indexCaseCheckVertical += indexCaseCheckVertical;
									
									
								
							}//END While Loop
							
							//print ("####### LAST COORD: " + theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] + "  " + theTileNames[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]);
							
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
			
			//print("++++***+++*+++* " + scannedTokensCoord[y]);
			
			
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
							break;
							case 2:
								findTargetToReverse.transform.renderer.material.color = Color.black;
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
