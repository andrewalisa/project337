using UnityEngine;
using System.Collections;

public class SkyController : MonoBehaviour {
	Material sky;
	//public Renderer water;

	public Transform stars;
	public Transform worldProbe;

	// Use this for initialization
	void Start () {
		sky = RenderSettings.skybox;
	}
	
	// Update is called once per frame
	void Update () {
		stars.transform.rotation = transform.rotation;
		Vector3 tvec = Camera.main.transform.position;
		worldProbe.transform.position = tvec;


	}
}
