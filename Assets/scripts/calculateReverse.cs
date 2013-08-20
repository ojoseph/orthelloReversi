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
	    {0} , //A
	} ;
	
	// A. 2D array of strings that will be replaced by what Created Map has done
	 string[,] theTileNames = new string[,]{
	    {""} , //A
	} ;
	
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
	
	public List<int> scannedTokenValue = new List<int>();
		
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
		
		//Clear it before use
		scannedTokensCoord.Clear();
		
		
		//Get the number of rows
		int theNumRows = (theField.Length/8); 
		
		//We set the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We check each entry
			for(int incre = 0; incre < 8; incre++){
				
				
				
				
				//print ("&&: " + theTileNames[theRow, incre]);
				
				
				
				//Check for  the location of the newest token
				if(theTileNames[theRow, incre] == newTokenLocation){
					
					//print ("We got the location of the new TOKEN " + newTokenLocation);

					//////////////////////////
					//<!> Safety
					//////////////////////////
					//Its a good patch
					if((theRow + indexCaseCheckVertical) < 0 || (theRow + indexCaseCheckVertical) > 7){
						indexCaseCheckVertical = 0;
						//print ("ROW UNDER 0 ");
					}
					
					if((incre + indexCaseCheckHorizontal) < 0 || (incre + indexCaseCheckHorizontal) >7){
						indexCaseCheckHorizontal = 0;
						//print ("INCRE UNDER 0 ");
					}
					
					
//					print ("AFTERPATCH:  "  + theWantedDirection +"  ### V: "+ (theRow + indexCaseCheckVertical) +" ### H: "+ (incre + indexCaseCheckHorizontal));
					//////////////////////////
					//Its a good patch
					//////////////////////////
					
					
					
					
					//We start looking on its right
					if(theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] == 0){
						
						//print(" Nothing on the " + theWantedDirection+"  side!!!! " + theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]  + "  " + theTileNames[theRow + indexCaseCheckVertical, incre +indexCaseCheckHorizontal ] );
						
					}else{
						
						//If there is something we check what it is
						//print(" There is something: " + theWantedDirection+"  side!!!!  " + theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]  + "  " + theTileNames[theRow + indexCaseCheckVertical, incre +indexCaseCheckHorizontal ] );
						
						
						//If int the position we found  and opponenent token we raise the scope and look for the next position
						if(theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] == opponentSlctColor){
							
							
							
						
							
							//////////////////////////
							//<!> Safety
							//////////////////////////
							//print ();
							/*if((incre + pullTempHorizontal) > 7 || (incre + pullTempHorizontal) < 0){
								
								break;
								print ("H BIGGER THAN 8");
								pullTempHorizontal = 0;
							}
							
							if((theRow + pullTempVertical) > 7 || (theRow + pullTempVertical) < 0){
								
								break;
								print ("V BIGGER THAN 8");
								pullTempVertical = 0;
							}	*/
							
							//////////////////////////
							//Its a good patch
							//////////////////////////
							
							
							
							int pullTempVertical =  indexCaseCheckVertical;
							int pullTempHorizontal = indexCaseCheckHorizontal;
							
							while(theField[theRow + pullTempVertical, incre + pullTempHorizontal] != 0){
								
								//We check if tis the same color as the current player
								if(theField[theRow + pullTempVertical, incre + pullTempHorizontal]  == playerSlctColor){
									
									//We pull out the last Token info until none is left
									//print ("<X> pull me out: " + theField[theRow + pullTempVertical, incre + pullTempHorizontal]  + "  " + theTileNames[theRow + pullTempVertical, incre +pullTempHorizontal ]);
			
								}
								
								//We raise the scope and check what is beyond that.
								pullTempHorizontal += indexCaseCheckHorizontal;
								pullTempVertical += indexCaseCheckVertical;
								
								
								
								//////////////////////////
								//<!> Safety
								//////////////////////////
								//print ();
								if((incre + pullTempHorizontal) > 7 || (incre + pullTempHorizontal) < 0){
									pullTempHorizontal = 0;
									//print ("H BIGGER THAN 8");
									break;
									
								}
								
								if((theRow + pullTempVertical) > 7 || (theRow + pullTempVertical) < 0){
									pullTempVertical = 0;
									//print ("V BIGGER THAN 8");
									break;
							
								}	
								
								//////////////////////////
								//Its a good patch
								//////////////////////////
																
							}
							
							
							
							
							
							
							//We raise the scope and check what is beyond that.
							//indexCaseCheckHorizontal += indexCaseCheckHorizontal;
							
							//print ("INSVESTIGATE MORE!!!  SCOPE: "  +  (theRow + indexCaseCheckVertical)   + "  " + (incre + indexCaseCheckHorizontal)  + "  "  + theTileNames[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] );
							//print ("<%>  V: "  +  (theRow + indexCaseCheckVertical)   + "   H: " + (incre + indexCaseCheckHorizontal));
							
							//print ("==  V: " + (theRow + indexCaseCheckVertical) +  "       H: " + (incre + indexCaseCheckHorizontal));
							//print ("==  WWW: " + theTileNames[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]   + "     "  + theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] );
							
							
							
							int anotherVertical =  indexCaseCheckVertical;
							int anotherHorizontal = indexCaseCheckHorizontal;
							/*scannedTokenValue.Add(theField[theRow + anotherVertical, incre + anotherHorizontal]);
							  scannedTokensCoord.Add(theTileNames[theRow + anotherVertical, incre + anotherHorizontal]);
							*/
							//We check until we find an empty space  || Or until we change rows.
							while(theField[theRow + anotherVertical, incre + anotherHorizontal] != 0 ){
							
								
								//print ("entry: " + theTileNames[theRow + anotherVertical, incre + anotherHorizontal] + "    " + theField[theRow + anotherVertical, incre + anotherHorizontal]);
								
								scannedTokensCoord.Add(theTileNames[theRow + anotherVertical, incre + anotherHorizontal]);
								scannedTokenValue.Add(theField[theRow + anotherVertical, incre + anotherHorizontal]);
								
								
								if(theField[theRow + anotherVertical, incre + anotherHorizontal] == playerSlctColor){
									//print("WE stop at " + theTileNames[theRow + anotherVertical, incre + anotherHorizontal]);
									//We remove the last entry
									scannedTokensCoord.Remove(theTileNames[theRow + anotherVertical, incre + anotherHorizontal]);
									
									//If the last entry is the opponent we clear this up
									if(theField[theRow + anotherVertical, incre + anotherHorizontal] == opponentSlctColor){
										scannedTokensCoord.Clear();
									}
									
									
									//DIsplay to confirm
									foreach( string itemsHeld in scannedTokensCoord){
										//print ("WE HELD: " + itemsHeld );	
									}
									changeColor(scannedTokensCoord);
									break;
								}
								
								
								
								
								
								
								
								
								
								
								
								
								//We raise the scope and check what is beyond that.
								anotherHorizontal += indexCaseCheckHorizontal;
								anotherVertical += indexCaseCheckVertical;
								
								//////////////////////////
								//<!> Safety
								//////////////////////////
								//print ();
								if((incre + anotherHorizontal) > 7 || (incre + anotherHorizontal) < 0){
									anotherHorizontal = 0;
									break;
								}
								
								if((theRow + anotherVertical) > 7 || (theRow + anotherVertical) < 0){
									anotherVertical = 0;
									break;
								}	
								
								//////////////////////////
								//Its a good patch
								//////////////////////////
								
								
							}//END While Loop
							
							
							/*print("#ITEMS in " + scannedTokenValue.Count);
							print ("WE BREAK OUT: " + theTileNames[theRow + anotherVertical, incre + anotherHorizontal] + "    " + theField[theRow + anotherVertical, incre + anotherHorizontal] );
							print ("La case FIN: " + theTileNames[theRow + anotherVertical - indexCaseCheckVertical, incre + anotherHorizontal - indexCaseCheckHorizontal] + "    " + theField[theRow + anotherVertical - indexCaseCheckVertical, incre + anotherHorizontal - indexCaseCheckHorizontal] );
							
							
							print("LAST MOTHER FUCKER " + scannedTokensCoord[(scannedTokensCoord.Count-1)] +"   "+ scannedTokenValue[(scannedTokenValue.Count-1)] );
							
							*/
							
							
						 
							
							
							/*
							
							foreach( string scanItemz in scannedTokensCoord){
								
								
								//Get the number of rows
								int theNumRowsEnd = (theField.Length/8); 
								
									//We set the rows
									for(int theRowEnd = 0 ; theRowEnd < theNumRowsEnd; theRowEnd++){
									
									//We check each entry
									for(int increEnd = 0; increEnd < 8; increEnd++){
										if(scanItemz == theTileNames[theRowEnd,increEnd]){
											print ("00000000000000000000000: " +  scanItemz);
											if(theField[theRowEnd,increEnd] == playerSlctColor){
												//changeColor(scannedTokensCoord);
												print ("SEGA");
												//break;
											}
										}
									
									}
								}
								
							 
							}*/
							
							
							
							//print ("####### LAST COORD: " + theField[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal] + "  " + theTileNames[theRow + indexCaseCheckVertical, incre + indexCaseCheckHorizontal]);
							//changeColor(scannedTokensCoord);
							
							
						}//End if raise scope pos
						
						//Make a while we dont reach 0
						
						
						
					}//END ELSE 
					
					
				}	
					
				//We print the names for a test.
				//print ("So I lived: " + theTileNames[theRow,incre ] + "##" + newTokenLocation);
			}
			
		}//End Generating 
		
		
		
		//We get the position of the latest token placed by the user by accessing the replaceTokenScript 竏
		///We loopthrough each case and check the infront and behind the tokens. 竏
		//We check for what is on the right if its empty we check on the left until we find an empty spot
		
		//We check for patterns like these:   B W B  and change it for  B B B .
		
		//We also need to check for patterns like that too:   B W W W B    ->  B B B B B
		
		//Actually we need to check for the first Token in the chain  [B]  W W W W B   until we get an empty space [_] (or  until we moved to the next row?) 竏
		//Then, we go back one case [B]  W W W W (B)  [_] 竏
		//Everything within this line becomes [B] :   [B]  B B B B (B)  [_] 竏
		
		//Once complete we use the same algorithm for up and down.竏
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
								//print("<!>1 trying to find: " + findTargetToReverse.name);
								findTargetToReverse.transform.renderer.material.color = Color.white;
							break;
							case 2:
								//print("<!>2 trying to find: " + findTargetToReverse.name);
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

