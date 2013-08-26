using UnityEngine;
using System.Collections;

public class showSkip : MonoBehaviour {

	// Use this for initialization
	void Start () {
		hideSkip();
	}
	
	// Update is called once per frame
	 public void displaySkip() {
		
		this.renderer.enabled = true;
		StartCoroutine("countDownHide");
		
	}
	
	
	public void hideSkip() {
		this.renderer.enabled = false;
	}
	
	IEnumerator countDownHide(){
		
		yield return new WaitForSeconds(2);	//Wait for 2 seconds
		hideSkip ();
		
	}
	
}
