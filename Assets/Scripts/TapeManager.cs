using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeManager : MonoBehaviour {

	public GameObject tapePrefab;
	private Transform tapeInstance;
	private Transform doorInstance;


	private string tapeMode;
	private float scaleOverLengthRatio;
	private Vector3 anchorPoint;
	private Vector3 mousePoint;
	private Rigidbody2D rb;

	void Update () {		
		if (Input.GetMouseButtonDown (0)) {
			CreateTape ();
		}
		if (tapeMode == "dragging") {
			AdjustTape ();
			if (Input.GetMouseButtonUp (0)) {
				StickTape ();
			}
		}
	}

	//create's a new tape set to drag mode where physics simulations have been disabled in its rigidbody component; no trigger colliders will be triggered
	void CreateTape(){
		doorInstance = GameObject.FindWithTag ("Door").transform;
		anchorPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		anchorPoint.z = 0;
		//if ( doorInstance.GetComponent<SpriteRenderer> ().bounds.Contains (anchorPoint)) {
		tapeInstance = Instantiate (tapePrefab, anchorPoint, Quaternion.identity, doorInstance).transform.GetChild(0);
		scaleOverLengthRatio = tapeInstance.localScale.y / tapeInstance.GetComponent<BoxCollider2D> ().bounds.size.y;
		rb = tapeInstance.GetComponent<Rigidbody2D> ();
		rb.simulated = false;
		tapeMode = "dragging";
		//}
	}

	//adjusts the tape's scale and rotation to the mouse position relative to its previous anchor point
	void AdjustTape(){
		mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePoint.z = 0;
		float theta = Mathf.Rad2Deg * Mathf.Atan2 (anchorPoint.x - mousePoint.x, mousePoint.y - anchorPoint.y);
		tapeInstance.rotation = Quaternion.Euler (Vector3.forward * theta);
		tapeInstance.localScale = new Vector3 (tapeInstance.localScale.x, Vector3.Distance (mousePoint, anchorPoint) * scaleOverLengthRatio, 1);
	}

	//sets tape mode to stuck and allows physics simulations again; trigger colliders will now be triggered
	void StickTape(){
		rb.simulated = true;
		tapeMode = "stuck";
	}

}
