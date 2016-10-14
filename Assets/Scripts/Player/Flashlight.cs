using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {
	public AudioClip soundOn;
	public AudioClip soundOff;
	private Light light;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
		light.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			if (light.enabled == false) {
				light.enabled = true;
				AudioSource.PlayClipAtPoint (soundOn, transform.position,0.1f);
			} else {
				light.enabled = false;
				AudioSource.PlayClipAtPoint (soundOff, transform.position,0.1f);
			}
		}
	}
}
