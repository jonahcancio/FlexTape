using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFader : MonoBehaviour {

	public DoorManager doorManager;
	private Text text;
	private string message;
	private bool isFadingOut;

	void Start () {
		if (doorManager == null) {
			Debug.Log ("You have no door manager :(");
			enabled = false;
		}
		text = GetComponent<Text> ();
		isFadingOut = false;
	}
	

	void Update () {
		Color newColor = text.color;
		if (doorManager.doorState == "exiting") {
			if (!isFadingOut) {
				newColor.a = 1;
				text.color = newColor;
				StartCoroutine (FadeOut());
				isFadingOut = true;
			}
		} else {
			StopCoroutine (FadeOut());
			isFadingOut = false;
			newColor.a = 0;
			text.color = newColor;

		}
	}

	IEnumerator FadeOut(){
		for(float k = 1.0f; k >= 0f; k-=0.08f){
			Color newColor = text.color;
			newColor.a = k;
			text.color = newColor;
			yield return new WaitForSeconds(0.1f);
		}
		Debug.Log("DONE FADING OUT");
	}


}
