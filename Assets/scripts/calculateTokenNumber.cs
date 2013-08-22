using UnityEngine;
using System.Collections;

public class calculateTokenNumber : MonoBehaviour {
	
	
	
	 private static int numWhiteTokens;
	 private static int numBlackTokens;
	 private static int numTotalTokens;
	
	
	// Use this for initialization
	public static void initMe () {
		
		
		//Reset them before use
		numWhiteTokens = 0;
		numBlackTokens = 0;
		numTotalTokens = 0;
		
		//We access createMap
		GameObject gameManagerObj = GameObject.Find("gameManager");
		//createMap theCreatedMap = gameManagerObj.GetComponent<createMap>();
		
		
		
		//Get the number of rows
		int theNumRows = (createMap.theField.Length/8); 
		
		//We set the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We check each entry
			for(int incre = 0; incre < 8; incre++){
				
				//print ("start " + createMap.theField[theRow,incre]);
				switch(createMap.theField[theRow,incre]){
					case 1:
						
						numWhiteTokens += 1;
					break;
					case 2:
						numBlackTokens += 1;
					break;
				}
				
				numTotalTokens =  numWhiteTokens + numBlackTokens;
				
				//We end the game and declare a player
				if(numTotalTokens == 64){
					
					gameManager.theCurrGameState = gameManager.gameState.gameOver;

				}
			}
		}	
		//We loop and check the number of tokens
		//we display the number of white tokens
		//We display the number of black tokens
		//We display the total number
		
	}
	

	void OnGUI () {
		
		GUI.Label (new Rect (25, 25, 300, 30), "White Tokens: " + numWhiteTokens);
		
		GUI.Label (new Rect (25, 45, 300, 30), "Black Tokens: " + numBlackTokens);
		
		GUI.Label (new Rect (25, 65, 300, 30), "Total Tokens: " + numTotalTokens);
		
		
	}
}
