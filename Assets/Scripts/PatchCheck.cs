using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchCheck : MonoBehaviour {

	public bool isPatched;

	void Start(){
		isPatched = false;
	}

	void OnTriggerEnter2D(Collider2D other){
		isPatched = true;
	}

	void OnTriggerExit2D(Collider2D other){
		isPatched = false;
	}
}
