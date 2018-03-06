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
			//if (Input.GetKeyDown ("space")) {
				NewDoor ();
			//}
		}else{
			if (doorState == "entering") {			
				EnterDoor ();
			}else if (doorState == "exiting") {
				ExitDoor ();
			}
		}
	}

	void LateUpdate(){
		if (Input.GetKeyDown ("p")) {
			doorState = "exiting";
		}
	}

	void NewDoor(){		
		doorInstance = Instantiate (door, midRightPoint, Quaternion.identity);
		float doorWidth = doorInstance.GetComponent<SpriteRenderer>().bounds.size.x;
		doorInstance.transform.Translate (new Vector3 (doorWidth, 0, 0));
		isDoorPatchedUp = false;
		doorState = "entering";
		if (GetComponent<TapeManager> () != null) {
			GetComponent<TapeManager> ().tapeZ = 0f;
			Debug.Log ("TAPEZ RESET");
		}
	}

	void EnterDoor(){
		doorInstance.transform.Translate (Vector3.left * moveSpeed * Time.deltaTime);
		if (doorInstance.transform.position.x <= centerPoint.x) {
			doorState = "hold";
		}
	}

	void ExitDoor(){
		float doorWidth = doorInstance.GetComponent<SpriteRenderer>().bounds.size.x;
		doorInstance.transform.Translate (Vector3.left * moveSpeed * Time.deltaTime);

		if (doorInstance.transform.position.x <= midLeftPoint.x - doorWidth) {
			Destroy (doorInstance);
			doorState = "non-existent";
		}
	}

}
