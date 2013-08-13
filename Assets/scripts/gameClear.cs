using UnityEngine;
using System.Collections;

public class gameClear : MonoBehaviour {

	void OnTriggerEnter(Collider theTrigger){
		print("THE TRIGGER" + theTrigger );
		
		//If we touch the player we tell the gameManager to change state!
		if(theTrigger.transform.tag == "Player"){
			Application.LoadLevel("gameClear");
		}
	}
}
