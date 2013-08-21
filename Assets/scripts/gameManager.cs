using UnityEngine;
using System.Collections;

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
	GameObject tempGameObj;
	
	
	
	// Use this for initialization
	void Start () {
		
		//We Init the game
		theCurrGameState = gameState.initialize;
		
		theCurrTurn = whoTurns.player;
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
						playGame();
						theCurrTurn = whoTurns.waitingPlayer;
				
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
			//We look for available positions
			print ("Getting the available positions");
			foreach(string items in gameplay.registerIndicator){
				//gameplay.registerIndicator
				print("got: " + items);
			}
			
			//AI selects a position
			//print("randomRange" + Random.Range(0,gameplay.registerIndicator.Count));
			aiPosSlct = gameplay.registerIndicator[Random.Range(0,gameplay.registerIndicator.Count)];
			
			print(aiPosSlct);
			//It finds that position
			tempGameObj = GameObject.Find(aiPosSlct);
			tempGameObj.renderer.material.color = Color.red;
			
			
			//It activates the script. It  place  a token at that location
			//tempGameObj.GetComponent<replaceToken>().Start();
			tempGameObj.GetComponent<replaceToken>().OnMouseDown();
			
			
			//We calculate for the best position
			//We add a token at that location.
			
			
			
			
			
			
			//thegameplay.startCheckingForPosition();
			
		}else{
			print ("###TEST ELSE");
        	//If the process is already done we move on to the next phase
			theCurrGameState = gameState.none;
		}
	}
	
	
	IEnumerator loadGameOver(){
//		print("FROM COROUTINE");
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel("gameOver");
		theCurrGameState = gameState.none;
	}
	
	
}
