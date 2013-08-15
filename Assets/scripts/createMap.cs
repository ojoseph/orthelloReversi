using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class createMap : MonoBehaviour {
	
	
	public enum currentStatus{
		notDoneYet,
		taskComplete
	}
	
	//This process progression
	public currentStatus theCurrentStatus = currentStatus.notDoneYet;
	
	//GameObbject for the tiles
	GameObject theTile;

	//Sets the spacing between each tiles
	public float gridSpacing = 4.2f;
	
	//Decides how high does the token stands [FIX]
	private float tokenHeight = 0.45f; 
	
	//-----------------------------------------------------------------------------------------------------------------------
	
	//public GameObject theTile = new GameObject();
	
	// A. 2D array of ints.
	public static int[,] theField = new int[,]
	{
	    {10, 11, 12, 13, 14, 15, 16, 17}, //A
		{20, 21, 22, 23, 24, 25, 26, 27}, //B
		{30, 31, 32, 33, 34, 35, 36, 37}, //C
		{40, 41, 42, 43, 44, 45, 46, 47}, //D
		{50, 51, 52, 53, 54, 55, 56, 57}, //E
		{60, 61, 62, 63, 64, 65, 66, 67}, //F
		{70, 71, 72, 73, 74, 75, 76, 77}, //G
		{80, 81, 82, 83, 84, 85, 86, 87}, //H
	};
	
	
	
	// A. 2D array of strings.Where we store the names
	public static string[,] theTileNames = new string[,]
	{
	    {"A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8"}, //A
		{"B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8"}, //B
		{"C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8"}, //C
		{"D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8"}, //D
		{"E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8"}, //E
		{"F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8"}, //F
		{"G1", "G2", "G3", "G4", "G5", "G6", "G7", "G8"}, //G
		{"H1", "H2", "H3", "H4", "H5", "H6", "H7", "H8"}, //H
	};
	
	
	
	
	//We need 3 values
	// 0 : Empty spots
	// 1 : White 
	// 2 : Black
	
	
	// Use this for initialization
	public void initMe () {
		
		//We do a soft reset
		softReset();
		
		
	 	//theMap[0].Add(0,1,3);
		//print(theField.Length);
		//print(72/8);
		
		//We set the tokens initial position 
		theField[3,3] = 2;
		theField[3,4] = 1;

		
		theField[4,4] = 2;
		theField[4,3] = 1;
		
		//We display the map for a quick check
		//displayArray();
		
		
		//We put the tiles int place
		makeField();
	}
	

	
	//////////////////////////////////////
	// [ STEP1 ]  MAKE FIELD : We make a simple field
	void makeField(){
		
		//Get the number of rows
		int theNumRows = (theField.Length/8); 
		
		//We set the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We populate the rows
			for(int incre = 0; incre < 8; incre++){
				theTile = Instantiate(Resources.Load("tile"),  new Vector3(incre*gridSpacing, 0, (theRow+3)*gridSpacing)/*transform.localPosition*incre*/, transform.localRotation) as GameObject;
				theTile.name = theTileNames[theRow,incre ];
				
				//We print the names for a test.
				//print (theTileNames[theRow,incre ]);
			}
			
		}//End Generating 
		
		
		//[ NEXT ] We place the tokens
		placeTokens(theField);
	}
	
	
	
	
	
	
	//////////////////////////////////////
	//  [ STEP 2 ]  PLACE TOKENS: We look for the spots where we need to add the token than than we generate the tokens at those positions
	void placeTokens(int[,] theCurrArray){
		
		int theNumRows = (theCurrArray.Length/8); 
		
		//Check each Entry in the table and we print it out
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			for(int incre = 0; incre < 8; incre++){
				
				//print(theCurrArray[theRow,incre]);
				switch(theCurrArray[theRow,incre]){
					
					case 1:
						//We place a white token 
						generateTokens(theRow,incre,theCurrArray[theRow,incre]);
					break;
					
					case 2:
						//We place a black token  
						generateTokens(theRow,incre,theCurrArray[theRow,incre]);
					break;
				}//end Switch
				
			}//End for loop
			
		}//End Row For loop
		
		//We are done creating the map so we give the greenlight
		theCurrentStatus = currentStatus.taskComplete;
		
	}//end FCT
	
	
	
	
	
	
	
	//////////////////////////////////////
	//  [ STEP 2a ] GENERATE TOKENS : takes care of generating the token
	void generateTokens(/*tokenType theType, */int theCurRow, int theCurIncre, int theTokenType /*GameObject theTargeToken, string theTargetNames*/){
		
		//We get the location of where we want to put our token
		GameObject targetToken = new GameObject();
		targetToken = GameObject.Find(theTileNames[theCurRow,theCurIncre]);
		
		//Make a variable that holds the location of the token we want.
		string loadedToken ="";
		
		switch(theTokenType){
			case 1:
				loadedToken = "tokens/whiteToken";
			break;
			case 2:
				loadedToken = "tokens/blackToken";
			break;
		}
		
		//Will serve for Token Creation
		GameObject theCreatedToken;
		
		//We create a token at the location of the target location.
		theCreatedToken = Instantiate(Resources.Load(loadedToken),  new Vector3(targetToken.transform.position.x,tokenHeight,targetToken.transform.position.z), transform.localRotation) as GameObject;
		theCreatedToken.name = "token" + theTokenType + theTileNames[theCurRow,theCurIncre];
	}
	
	
	
	
	
	
	
	
	
	//........................................................................................................................................................................................................................................................................................
	//TOOLS
	
	
	
	
	
	
	//////////////////////////////////////
	// [ TOOL ]  SOFT RESET: Takes care of cleaning the arrays so that we ahve a clean table
	void softReset(){
		
		//Get the number of rows
		int theNumRows = (theField.Length/8); 
		
		//We set the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We populate the rows
			for(int incre = 0; incre < 8; incre++){
			
				theField[theRow,incre] = 0;	
				//We print the names for a test.
				//print (theTileNames[theRow,incre ]);
			}
			
		}//End Generating 
	}
	
	
	 //////////////////////////////////////
	// [ TOOL ]  DiSPLAY ARRAY: shows the values in the FIeld Array
	public static void displayArray(){
		/*foreach(int theValues in theField){
			print("val: " + theValues);
		}*/
		
		//Get the number of rows
		int theNumRows = (theField.Length/8); 
		
		//We set the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We populate the rows
			for(int incre = 0; incre < 8; incre++){
			
				print ( theTileNames[theRow,incre]  + " " +  theField[theRow,incre ]);
				//We print the names for a test.
				//print (theTileNames[theRow,incre ]);
			}
			
		}//End Generating 
	}
	
	
}
