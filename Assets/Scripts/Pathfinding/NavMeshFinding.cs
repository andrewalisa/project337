using UnityEngine;
using System.Collections;

public class NavMeshFinding : MonoBehaviour {

	public Transform target;
	private NavMeshAgent agent;
	private float distanceToTarget;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		distanceToTarget = Vector3.Distance (transform.position, target.position);
		if (distanceToTarget > 2.5F) {
			agent.destination = target.position;
		}
	}
}