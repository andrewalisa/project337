using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {
	public Transform target;
	public float distance;
	private Vector3 targetLocale;
	// Use this for initialization
	void Start () {
		targetLocale = new Vector3(target.position.x, target.position.y + distance, target.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
		targetLocale.z = target.position.z;
		targetLocale.x = target.position.x;
		transform.position = targetLocale;
	}
}
