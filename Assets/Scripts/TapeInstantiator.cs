using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeInstantiator : MonoBehaviour {

	public GameObject tape;
	private Transform tapeInstance;
	public Transform door;


	private string tapeMode;
	private float scaleOverLengthRatio;
	private Vector3 anchorPoint;
	private Vector3 mousePoint;
	private Rigidbody2D rb;

	void Update () {
		//create's a new tape set to drag mode where physics simulations have been disabled in its rigidbody component; no trigger colliders will be triggered
		if (Input.GetMouseButtonDown (0)) {
			door = GameObject.FindWithTag ("Door").transform;
			anchorPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Debug.Log (door.GetComponent<SpriteRenderer> ().bounds);
			Debug.Log (anchorPoint);
			if ( door.GetComponent<SpriteRenderer> ().bounds.Contains (new Vector3(anchorPoint.x, anchorPoint.y)) ) {
				tapeInstance = Instantiate (tape, new Vector3 (anchorPoint.x, anchorPoint.y), Quaternion.identity, door).transform.Find ("Tape Sprite");
				scaleOverLengthRatio = tapeInstance.localScale.y / tapeInstance.GetComponent<BoxCollider2D> ().bounds.size.y;
				rb = tapeInstance.GetComponent<Rigidbody2D> ();
				rb.simulated = false;
				tapeMode = "dragging";
			}
		}

		//adjusts the tape's scale and rotation to the mouse position relative to its previous anchor point
		if (tapeMode == "dragging") {			
			mousePoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			float theta = Mathf.Rad2Deg * Mathf.Atan2 (anchorPoint.x - mousePoint.x, mousePoint.y - anchorPoint.y);
			tapeInstance.rotation = Quaternion.Euler (Vector3.forward * theta);
			tapeInstance.localScale = new Vector3 (tapeInstance.localScale.x, Vector3.Distance (mousePoint, anchorPoint) * scaleOverLengthRatio, 1);
		
			//sets tape mode to stuck and allows physics simulations again; trigger colliders will now be triggered
			if (Input.GetMouseButtonUp (0)) {
				rb.simulated = true;
				tapeMode = "stuck";
			}
		}


	}


}
