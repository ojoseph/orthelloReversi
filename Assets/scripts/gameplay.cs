using UnityEngine;
using System.Collections;

public class gameplay : MonoBehaviour {
	
	
	// A. 2D array of ints.
	public int[,] theField = new int[,]
	{
	    {0}, //A
	};
	
	public string[,] theTileNames = new string[,]{
	    {""}, //A
	};
	
	
	// Use this for initialization
	void Start () {
		
		//We import the data from the created map 
		createMap theCreatedMap = GetComponent<createMap>();
		
		//We reassign them to here with the same name to avoid confusion.
		theField = theCreatedMap.theField;
		theTileNames = theCreatedMap.theTileNames;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
