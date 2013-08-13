using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {
	
	
	public enum gameState{
		none,
		initialize,
		playGame,
		gameOver
	}
	
	public gameState theCurrGameState;
	
	
	// Use this for initialization
	void Start () {
		
		//We Init the game
		theCurrGameState = gameState.initialize;
	}
	
	
	
	// Update is called once per frame
	void Update () {
		switch(theCurrGameState){
			
			case gameState.initialize:
				initialize();
			break;
			
			case gameState.playGame:
				playGame();
			break;
			
			case gameState.gameOver:
				//loadGameOverGameManager();
				StartCoroutine("loadGameOver");
			break;
		}
	}
	
	
	
	//Here we create the Map
	void initialize(){
		//We call the ftc for creating a map.
	}
	
	
	//We Start Playing
	void playGame(){
		//We have to split this into phases to
	}
	
	
	
	IEnumerator loadGameOver(){
//		print("FROM COROUTINE");
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel("gameOver");
		theCurrGameState = gameState.none;
	}
	
	
}
