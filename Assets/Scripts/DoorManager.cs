using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour {

	private Vector3 centerPoint;
	private Vector3 midLeftPoint;
	private Vector3 midRightPoint;

	public GameObject door;
	private GameObject doorInstance;

	public float moveSpeed;
	public string doorState;
	public bool isDoorPatchedUp;

	void Start () {
		float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
		centerPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camDistance));
		midLeftPoint = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.5f, camDistance));
		midRightPoint = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0.5f, camDistance));
		doorState = "non-existent";
	}
	

	void Update () {
		if (doorState == "non-existent") {
			if (Input.GetKeyDown ("space")) {
				newDoor ();
			}
		}else{
			if (doorState == "entering") {			
				enterDoor ();
			}else if (doorState == "exiting") {
				exitDoor ();
			}
		}
	}

	void LateUpdate(){
		if (Input.GetKeyDown ("p")) {
			doorState = "exiting";
		}
	}

	void newDoor(){
		doorInstance = Instantiate (door, midRightPoint, Quaternion.identity);
		isDoorPatchedUp = false;
		doorState = "entering";
	}

	void enterDoor(){
		doorInstance.transform.Translate (Vector3.left * moveSpeed * Time.deltaTime);
		if (doorInstance.transform.position.x <= centerPoint.x) {
			doorState = "hold";
		}
	}

	void exitDoor(){
		doorInstance.transform.Translate (Vector3.left * moveSpeed * Time.deltaTime);
		if (doorInstance.transform.position.x <= midLeftPoint.x) {
			Destroy (doorInstance);
			doorState = "non-existent";
		}
	}

}
