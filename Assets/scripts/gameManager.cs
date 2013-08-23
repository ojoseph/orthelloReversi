using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameManager : MonoBehaviour {
	
	
	public enum gameState{
		none,
		initialize,
		playGame,
		gameOver
	}
	
	public static gameState theCurrGameState;
	
	
	public enum whoTurns{
		waitingPlayer,
		waitingOpponent,
		player,
		opponent
		
	}
	
	public static whoTurns theCurrTurn;
	
	//####   AI    ####
	string aiPosSlct;
	//holds the name of the final position.
	string aiPosSlctFinal = "";
	GameObject tempGameObj;
	
	
	public List<string> accumulateName = new List<string>();
	public List<int> accumulateVal = new List<int>();
	public List<string> refineName = new List<string>();
	public List<int> refineVal = new List<int>();
	
	
	// Use this for initialization
	void Start () {
		
		//We Init the game
		theCurrGameState = gameState.initialize;
		
		theCurrTurn = whoTurns.player;
	}
	
	
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
	
	
	
	
	
	
	
	// Update is called once per frame
	void Update () {
		switch(theCurrGameState){
			
			case gameState.initialize:
				initialize();
			break;
			
			case gameState.playGame:
			
				switch(theCurrTurn){
					
					case  whoTurns.player:
						print ("<!> PlayerTurn <!>");
						theCurrTurn = whoTurns.waitingPlayer;
						playGame();
						
				
						//We Count the number of tokens
						calculateTokenNumber.initMe();
					break;
				
					case  whoTurns.opponent:
						print ("<!> OpponentTurn <!>");
				
						//We change the status before
						theCurrTurn = whoTurns.waitingOpponent;
				
						opponentTurn();
						
				
						//We Count the number of tokens
						calculateTokenNumber.initMe();
				
					break;
				
				}
			
				
			break;
			
			case gameState.gameOver:
				//loadGameOverGameManager();
				StartCoroutine("loadGameOver");
			break;
		}
	}
	
	
	
	//Here we create the Map
	void initialize(){
		//We access the create Map class
		//We call the ftc for creating a map and start the mapSetUp process.
		createMap theCreatedMap = GetComponent<createMap>();
		
		
		//We check if the process is done or not if so we move to the next phase, Else we do not.
		if(theCreatedMap.theCurrentStatus == createMap.currentStatus.notDoneYet){
			
			//If we havent created a map yet we make one.
			theCreatedMap.initMe();
			
		}else{
			
        	//If the process is already done we move on to the next phase
			theCurrGameState = gameState.playGame;
		}
		
	}
	
	
	//We Start Playing
	public void playGame(){
		//We have to split this into phases to
		//print("We are playing the game");
		//We call the ftc for creating a map and start the mapSetUp process.
		
		
		gameplay thegameplay = GetComponent<gameplay>();
		
			thegameplay.theCurrentStatus = gameplay.currentStatus.notDoneYet;
		
		//We check if the process is done or not if so we move to the next phase, Else we do not.
		if(thegameplay.theCurrentStatus == gameplay.currentStatus.notDoneYet){
			
			//playerSlctColor
			thegameplay.playerSlctColor = 2;
			thegameplay.opponentSlctColor = 1;
			
			
			//If we havent created a map yet we make one.
			thegameplay.initMe();
			
		}else{
			
        	//If the process is already done we move on to the next phase
			theCurrGameState = gameState.none;
		}
	}
	
	
	
	//We Start Playing
	public void opponentTurn(){
		//We have to split this into phases to
		print("Its the opponent's TURN!!");
		//We call the ftc for creating a map and start the mapSetUp process.
		
		
		gameplay thegameplay = GetComponent<gameplay>();
		
		thegameplay.theCurrentStatus = gameplay.currentStatus.notDoneYet;
		
		//We check if the process is done or not if so we move to the next phase, Else we do not.
		if(thegameplay.theCurrentStatus == gameplay.currentStatus.notDoneYet){
			
			//print ("###TEST");
			
			//playerSlctColor
			thegameplay.playerSlctColor = 1;
			thegameplay.opponentSlctColor = 2;
			
			
			//If we havent created a map yet we make one.
			thegameplay.initMe();
			
		
				
			//#################
			///#####    AI     #####
			
				//Clean before use
				accumulateName.Clear();
				accumulateVal.Clear();
			
				//We look for available positions
			
				print ("Getting the available positions");
				
				//Check what is beign held
				foreach(string items in gameplay.registerIndicator){
					print("got: " + items);
					accumulateName.Add(items.Replace("indicator", ""));
				
					//we have ready on standby
					accumulateVal.Add (0);
				}
				
				//AI selects a position
				//print("randomRange" + Random.Range(0,gameplay.registerIndicator.Count));
				aiPosSlct = gameplay.registerIndicator[Random.Range(0,gameplay.registerIndicator.Count-1)];
				
				
			
				// We calculate and look for the best position available.
				calculateBestPosition(gameplay.registerIndicator);
			
			
			
			
				print(aiPosSlct);
				//It finds that position
				tempGameObj = GameObject.Find(aiPosSlct);
				if(tempGameObj != null){
				
				
					print("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
					
					//StartCoroutine(delay(aiPosSlct));
					
					StartCoroutine(delay(aiPosSlctFinal));
					
					print("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");				
				
				
				
				}else{
				
					print ("DID not Found LOCATIon");
					 
				}
				//It activates the script. It  place  a token at that location
				//tempGameObj.GetComponent<replaceToken>().Start();
				//tempGameObj.GetComponent<replaceToken>().OnMouseDown();
				
				
				//We calculate for the best position
				//We add a token at that location.
			
			//#################
			///#####    AI     #####
			
			
			
			
			//thegameplay.startCheckingForPosition();
			
		}else{
			print ("###TEST ELSE");
        	//If the process is already done we move on to the next phase
			theCurrGameState = gameState.none;
		}
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	IEnumerator delay(string someName){
		
 		print("GO WITH: " + someName);
		
			yield return new WaitForSeconds(0.5f);
		if(someName != ""){
			
			print("I COLOR: " + someName);
			GameObject.Find(someName).renderer.material.color = Color.red;
		 
		}else{
			
			print ("CANT COLOR IT");
			
		}
		
		yield return new WaitForSeconds(2.0f);
		print("OK WE GO WITH: " + someName);
		
		if(someName != ""){
			print("I chose: " + someName);
			//theTempGameObj.GetComponent<replaceToken>().OnMouseDown();
			GameObject.Find(someName).GetComponent<replaceToken>().OnMouseDown();
		}else{
			print("Did not FIND THE TARGET");
			//print("I chose: " + theTempGameObj.name);
		}
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	IEnumerator loadGameOver(){
//		print("FROM COROUTINE");
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel("gameOver");
		theCurrGameState = gameState.none;
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	void calculateBestPosition(List<string> theRegisterIndicator){
		
		//We check each potential position available. √
		//From that position, they loop in each direction
		//They add up a point for each token they can take.
		//Finally they take the one with the highest value; if 2 of the same value occurs we select randomly
		
		foreach(string potentialPos in theRegisterIndicator){
			
			print("# POTENTIAL" + potentialPos);
			
			checkForBestPos(lookDirection.up, potentialPos);
			checkForBestPos(lookDirection.left, potentialPos);
			checkForBestPos(lookDirection.down, potentialPos);
			checkForBestPos(lookDirection.right, potentialPos);
			
			
			
			checkForBestPos(lookDirection.diagDownLeft, potentialPos);
			checkForBestPos(lookDirection.diagDownRight, potentialPos);
			checkForBestPos(lookDirection.diagUpLeft, potentialPos);
			checkForBestPos(lookDirection.diagUpRight, potentialPos);
			
		}
		
	}
	
	
	
	
	
	void checkForBestPos(lookDirection theWantedDirection, string posToRecover){
		
		string thePos;
		
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
		
		
		
		
		//First We clean the content 
		thePos = posToRecover.Replace("indicator", "");
		
		
		
		
		//Get the number of rows
		int theNumRows = (createMap.theField.Length/8); 
		
		
		gameplay thegameplay = GetComponent<gameplay>();
		
		int addTempVertical =  indexCaseCheckVertical;
		int addTempHorizontal = indexCaseCheckHorizontal;	
		int numPotentialToken = 0;
		
		
		
		//We set the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We check each entry
			for(int incre = 0; incre < 8; incre++){
				
				
				if(createMap.theTileNames[theRow,incre] == thePos){
					
					
					 
					print("");
					print("-------------------------------   " + theWantedDirection + "   -------------------------------------");
					 
					print("WE RECOVERED THE Potential POSITION: " + createMap.theTileNames[theRow,incre] + "  " + createMap.theField[theRow,incre]);
					
					//We start looping until we reach nothing || until we reach a token with the same color as us
					
					print("BEFORE ENTERING: " + addTempVertical + "   " + addTempHorizontal + "PlayerColor: " + thegameplay.playerSlctColor);
					
					while(createMap.theField[theRow + addTempVertical ,incre + addTempHorizontal] != 0 /*&& createMap.theField[theRow + addTempVertical,incre + indexCaseCheckHorizontal ] == thegameplay.opponentSlctColor*/){
						
						
						print ("<#> We check a token " +  createMap.theTileNames[theRow + addTempVertical ,incre + addTempHorizontal] + "   " + createMap.theField[theRow + addTempVertical ,incre + addTempHorizontal]);
						
						
						
						
						/*if( accumulateName.Count <= 0){
							
							accumulateName.Add(createMap.theTileNames[theRow,incre]);
							
						}else{
 
							print("##########"  + accumulateName.Count);
							//int indexWhere = 0;
							
							//foreach(string something in accumulateName){
								for(int item = 0; item < accumulateName.Count -1; item++){
									
									if(accumulateName[item] == createMap.theTileNames[theRow,incre]){
										
										print("it already exist so we accumulate " + item);
										accumulateVal[item] += numPotentialToken;
										
									}else{
										
										print("it already exist so we accumulate " + item);
									
										//It does not exist
										accumulateName.Add(createMap.theTileNames[theRow, incre]);
										
									}
									
									//indexWhere += 1;
								} 
							//}
							
							
							 
						} */
					
						
						
						
						
						
						addTempVertical +=  indexCaseCheckVertical;
						addTempHorizontal += indexCaseCheckHorizontal;	
						
						print("<+++> Increment: " + addTempVertical + "   " + addTempHorizontal);
						print ("<*> Accumulation: " + numPotentialToken + "  " + createMap.theTileNames[theRow + addTempVertical ,incre + addTempHorizontal] );
						numPotentialToken += 1;
						
						if(createMap.theField[theRow + addTempVertical ,incre + addTempHorizontal] == thegameplay.playerSlctColor){
							
							
							print ("<=#=> We stop the chain incrementation: " + numPotentialToken);
							
							//RECOVER AND UPDATE
							for(int item = 0; item < accumulateName.Count; item++){
								
								if(accumulateName[item] == createMap.theTileNames[theRow,incre]){
									
									//We make a visual confirmation
									print("<R> Will Register " + createMap.theTileNames[theRow,incre] + "   " + numPotentialToken);
									accumulateVal[item] += numPotentialToken;
									
								}
								
							}//End for loop
							
							
							//===============================================
							//We try to figure out the highest value. in the collected Data
							//RECOVER HIGHEST VALUE
							//===============================================
							
							int potenTokenPow = 0;
							int highestPoten = 0;
							string highestPotenName = "";
							
							
							
							for(int item = 0; item < accumulateName.Count; item++){
								
								//if(accumulateName[item] == createMap.theTileNames[theRow,incre]){
									
									potenTokenPow = accumulateVal[item];
									
								
									//if  its the same we add it in the things we may select
									if(potenTokenPow == highestPoten){
										
									
										refineName.Add(accumulateName[item]);
										refineVal.Add(accumulateVal[item]);
									
										highestPotenName = accumulateName[item];
										highestPoten = potenTokenPow;
										print("Same as Highest number is" + highestPoten + "   " + accumulateName[item]);
										
									}
								
								
									//If its bigger we clean the table and add the new value.
									if(potenTokenPow > highestPoten){
										
										//We clear  them as a new highest value will be added.
										refineName.Clear();
										refineVal.Clear();
									
										//We add the new highest values
										refineName.Add(accumulateName[item]);
										refineVal.Add(accumulateVal[item]);
									
									
										highestPotenName = accumulateName[item];
										highestPoten = potenTokenPow;
										print("Highest number is" + highestPoten + "   " + accumulateName[item]);
										//break;
									}
								
									
								//}
								
							}//End for loop
							
							/*
							public List<string> refineName = new List<string>();
							public List<int> refineVal = new List<int>();
							*/
							int getRandRefine = Random.Range(0, refineName.Count);
							
							print( " <?Random?> " + refineName[getRandRefine]  +  "    "  +  refineVal[getRandRefine]);
							
							//We update the last pos
							aiPosSlctFinal = "indicator" + highestPotenName;
							
							//StartCoroutine(delay("indicator" + highestPotenName));
							
							break;
						}
						
						
						
						 
						
						
						
						
						
						
						
						
						//////////////////////////
						//<!> Safety
						//////////////////////////
						//print ();
						if((incre + addTempHorizontal) > 7 || (incre + addTempHorizontal) < 0){
							addTempVertical = 0;
							break;
						}
						
						if((theRow + addTempVertical) > 7 || (theRow + addTempVertical) < 0){
							addTempVertical = 0;
							break;
						}	
						
						//////////////////////////
						//Its a good patch
						//////////////////////////
					}
					
					print("--------------------------------------------------------------------");
					print("");
					 
				}
				
				
			}//end for loop
			
			
		}//End for loop	
		
		
		
		
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
