using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class createMap : MonoBehaviour {
	
	//We create the main plane
	//private List<int> theMap = new List<int>();
	GameObject theTile;
	GameObject whiteToken;
	GameObject blackToken;
	
	//public GameObject theTile = new GameObject();
	
	// A. 2D array of ints.
	int[,] theField = new int[,]
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
	string[,] theTileNames = new string[,]
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
	void Start () {
	 //	theMap[0].Add(0,1,3);
		print(theField.Length);
		print(72/8);
		
		//We set the game 
		theField[3,3] = 1;
		theField[3,5] = 2;
		
		
		theField[4,4] = 2;
		theField[4,4] = 1;
		
		// We create an object and place it.
		//theTile = Instantiate(Resources.Load("tile"),  transform.localPosition, transform.localRotation) as GameObject;
		
		//Get the number of rows
		int theNumRows = (theField.Length/8); 
		
		//We display a MAP
		//We set the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We populate the rows
			for(int incre = 0; incre < 8; incre++){
				theTile = Instantiate(Resources.Load("tile"),  new Vector3(incre*4.2f, 0, (theRow+3)*4.2f)/*transform.localPosition*incre*/, transform.localRotation) as GameObject;
				theTile.name =theTileNames[theRow,incre ];
				
				//We print the names for a test.
				//print (theTileNames[theRow,incre ]);
			}
			
		}//End Generating 
		
		
		
		//The token
		whiteToken = Instantiate(Resources.Load("whiteToken"),  new Vector3(0,0.45f,0)/*transform.localPosition*incre*/, transform.localRotation) as GameObject;
		
		whiteToken = Instantiate(Resources.Load("blackToken"),  new Vector3(10,0.45f,0)/*transform.localPosition*incre*/, transform.localRotation) as GameObject;
		
		
		
		
		checkWhatIsThere(theField);
		
	}
	
	
	void checkWhatIsThere(int[,] theCurrArray){
		
		int theNumRows = (theField.Length/8); 
		
		//Check each Entry in the table and we print it out
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			for(int incre = 0; incre < 8; incre++){
				//print(theCurrArray[theRow,incre]);
				switch(theCurrArray[theRow,incre]){
				case 1:
					//We place a token 
					print("WE FOUND ONE " + theTileNames[theRow,incre]);
					
					//We get the location of where we want to put our token
					GameObject targetToken = new GameObject();
					targetToken = GameObject.Find(theTileNames[theRow,incre]);
					
					//We create a token at this location
	
					whiteToken = Instantiate(Resources.Load("whiteToken"),  new Vector3(targetToken.transform.position.x,0.45f,targetToken.transform.position.z)/*transform.localPosition*incre*/, transform.localRotation) as GameObject;
					
				break;
				
				case 2:
					//We place a token 
					print("WE FOUND TWO " + theTileNames[theRow,incre]);
					
				break;
			}
			}
			
		}
		/*foreach(int elm in theCurrArray){
			print(elm);
			
			switch(elm){
				case 1:
					//We place a token 
				break;
				
				case 2:
					//We place a token 
				break;
			}
		}*/
		
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
