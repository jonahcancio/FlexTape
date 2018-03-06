using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCrackManager : MonoBehaviour {

	public bool isDoorCompletelyPatched;

	public int cracksPerDoor;
	public float maxCrackRotation;
	public float minCrackSize;
	public float maxCrackSize;
	public GameObject crackPrefab;

	public DoorManager doorManager;

	void Start () {
		doorManager = GameObject.FindWithTag ("GameController").GetComponent<DoorManager> ();
		isDoorCompletelyPatched = false;
		distributeCracksRandomly ();
	}

	void Update () {
		if (!isDoorCompletelyPatched) {
			CheckAllCracks ();
		} else {
			doorManager.doorState = "exiting";
		}
	}

	void CheckAllCracks(){
		isDoorCompletelyPatched = true;
		foreach(Transform child in transform){
			CrackPatchCheck cpc = child.GetComponent<CrackPatchCheck> ();
			if (cpc != null) {
				if (cpc.isCrackPatched == false) {
					isDoorCompletelyPatched = false;
					break;
				}
			}
		}
	}

	void distributeCracksRandomly(){
		Bounds doorBounds = GetComponent<SpriteRenderer> ().bounds;
		Vector3 doorMin = doorBounds.min;
		Vector3 doorMax = doorBounds.max;
		GameObject crackInstance;
		SpriteRenderer crackSprite;
		Bounds crackBounds;
		for (int i = 0; i < cracksPerDoor; i++) {
			Vector3 crackPos = new Vector3 (Random.Range (doorMin.x, doorMax.x), Random.Range (doorMin.y, doorMax.y), 0);
			Quaternion crackRot = Quaternion.Euler (Vector3.forward * Random.Range (-maxCrackRotation, maxCrackRotation));
			crackInstance = Instantiate (crackPrefab, crackPos, crackRot, transform);
			crackSprite = crackInstance.GetComponent<SpriteRenderer> ();
			crackBounds = crackSprite.bounds;
			if (!doorBounds.Contains (crackBounds.max)) {
				Vector3 movement = doorBounds.ClosestPoint (crackBounds.max) - crackBounds.max;
				crackInstance.transform.Translate (movement, Space.World);

			}
			if (!doorBounds.Contains (crackBounds.min)) {
				Vector3 movement = doorBounds.ClosestPoint (crackBounds.min) - crackBounds.min;
				crackInstance.transform.Translate (movement, Space.World);
			}
		}
	}

}
