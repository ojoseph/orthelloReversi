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
	
	// A. 2D array of strings.
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
		theField[3,4] = 1;
		theField[3,5] = 2;
		
		
		theField[4,4] = 2;
		theField[4,5] = 1;
		
		// We create an object and place it.
		//theTile = Instantiate(Resources.Load("tile"),  transform.localPosition, transform.localRotation) as GameObject;
		
		//Get the number of rows
		int theNumRows = (theField.Length/8); 
		
		//We set the rows
		for(int theRow = 0 ; theRow < theNumRows; theRow++){
			
			//We populate the rows
			for(int incre = 0; incre < 8; incre++){
				theTile = Instantiate(Resources.Load("tile"),  new Vector3(incre*4.2f, 0, (theRow+3)*4.2f)/*transform.localPosition*incre*/, transform.localRotation) as GameObject;
				//print(incre);
			}
			
		}//End Generating 
		
		
		
		//The token
		whiteToken = Instantiate(Resources.Load("whiteToken"),  new Vector3(0,0.45f,0)/*transform.localPosition*incre*/, transform.localRotation) as GameObject;
		
		whiteToken = Instantiate(Resources.Load("blackToken"),  new Vector3(10,0.45f,0)/*transform.localPosition*incre*/, transform.localRotation) as GameObject;
		
		
		
		
		checkWhatIsThere(theField);
		
	}
	
	
	void checkWhatIsThere(int[,] theCurrArray){
		
		//Check each Entry in the table and we print it out
		/*for(int theRow = 0 ; theRow < 8; theRow++){
			
			for(int incre = 0; incre < theCurrArray.Length; incre++){
				print(theCurrArray[theRow,incre]);
			}
			
		}*/
		foreach(int elm in theCurrArray){
			print(elm);
		}
		
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
