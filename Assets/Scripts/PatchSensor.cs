using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchSensor : MonoBehaviour {
	public bool hasBeenPatched;

	void Start(){
		hasBeenPatched = false;
	}

	void OnTriggerEnter2D(Collider2D other){
		hasBeenPatched = true;
		Debug.Log (gameObject.name + " has been patched");
	}	

	void OnTriggerExit2D(Collider2D other){
		hasBeenPatched = false;
		Debug.Log (gameObject.name + " has been unpatched");
	}	

}
