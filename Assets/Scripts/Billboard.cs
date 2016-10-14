using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {


	public Camera mainCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(mainCamera.transform.position, Vector3.up);
	}
}
