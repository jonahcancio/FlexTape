using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFader : MonoBehaviour {

	public DoorManager doorManager;
	private Text text;

	void Start () {
		if (doorManager == null) {
			Debug.Log ("You have no door manager :(");
			enabled = false;
		}
		text = GetComponent<Text> ();
		Debug.Log (text.color.a);
	}
	

	void Update () {
		Color newColor = text.color;
		if (doorManager.doorState == "exiting") {			
			newColor.a = 1;
			text.color = newColor;
		} else {
			newColor.a = 0;
			text.color = newColor;
		}
	}
}
