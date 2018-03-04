using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackPatchCheck : MonoBehaviour {
	
	public bool isCrackPatched;

	void Start(){
		isCrackPatched = false;
	}

	void Update(){
		if (!isCrackPatched) {
			CheckAllCrackColliders ();
		} else {
			Debug.Log (name + " has been patched");
			this.enabled = false;
		}
	}

	void CheckAllCrackColliders(){
		isCrackPatched = true;
		foreach(Transform child in transform){
			PatchCheck patchCheck = child.GetComponent<PatchCheck>();
			if (patchCheck != null) {
				//Debug.Log ("Checking " + child.name);
				if (patchCheck.isPatched == false) {
					isCrackPatched = false;
					break;
				}
			}
		}
	}

}
