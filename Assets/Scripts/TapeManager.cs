using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeManager : MonoBehaviour {

	public GameObject tapePrefab;
	public GameObject tapeInstance;
	private Transform tapeSprite;
	private Transform doorInstance;
	private Transform tapeLogo;

	private string tapeMode;
	private float scaleOverLengthRatio;
	private Vector3 anchorPoint;
	private Vector3 mousePoint;
	private Rigidbody2D rb;
	public float tapeZ;

	void Start(){
		tapeZ = 0;
	}

	void Update () {		
		if (Input.GetMouseButtonDown (0)) {
			//Debug.Log(Camera.main.ScreenToWorldPoint (Input.mousePosition));
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
		anchorPoint.z = tapeZ;
		tapeInstance = Instantiate (tapePrefab, anchorPoint, Quaternion.identity, doorInstance);
		tapeSprite = tapeInstance.transform.GetChild(0);
		tapeLogo = tapeInstance.transform.GetChild(1);
		scaleOverLengthRatio = tapeSprite.localScale.y / tapeSprite.GetComponent<BoxCollider2D> ().bounds.size.y;
		rb = tapeSprite.GetComponent<Rigidbody2D> ();
		rb.simulated = false;
		tapeMode = "dragging";
	}

	//adjusts the tape's scale and rotation to the mouse position relative to its previous anchor point
	void AdjustTape(){
		mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePoint.z = tapeZ;
		float theta = Mathf.Rad2Deg * Mathf.Atan2 (anchorPoint.x - mousePoint.x, mousePoint.y - anchorPoint.y);
		tapeSprite.rotation = Quaternion.Euler (Vector3.forward * theta);
		tapeLogo.rotation = Quaternion.Euler (Vector3.forward * theta);
		tapeSprite.localScale = new Vector3 (tapeSprite.localScale.x, Vector3.Distance (mousePoint, anchorPoint) * scaleOverLengthRatio, 1);
		tapeLogo.localPosition = (mousePoint - anchorPoint) / 2;
	}

	//sets tape mode to stuck and allows physics simulations again; trigger colliders will now be triggered
	void StickTape(){
		rb.simulated = true;
		tapeMode = "stuck";
		tapeZ -= 0.01f;
	}

}
