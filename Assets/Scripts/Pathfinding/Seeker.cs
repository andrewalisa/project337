using UnityEngine;
using System.Collections;

public class Seeker : MonoBehaviour {

	public Transform target;
	public float speed;

	private Vector3[] path;
	private int targetIndex;
	private float distanceToTarget;

	void Start() {
		Debug.Log ("Request Path");
		PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);

	}
	void Update() {
		//distanceToTarget = Vector3.Distance (transform.position, target.position);
		//if (distanceToTarget > 5.0F) {
		//	PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
		//}
	}
	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			Debug.Log ("Found Path");
			path = newPath;
			StopCoroutine ("FollowPath");
			StartCoroutine ("FollowPath");
		}
	}

	private IEnumerator FollowPath() {
		if (path [0] != null) {
			Vector3 currentWaypoint = path [0];
		
			while (true) {
				if (transform.position == currentWaypoint) {
					targetIndex++;
					if (targetIndex >= path.Length) {
						yield break;
					}
					currentWaypoint = path [targetIndex];
				}
				transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, speed * Time.deltaTime);
				yield return null;
			}
		} else {
			yield return null;
		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube (path [i], Vector3.one);

				if (i == targetIndex) {
					Gizmos.DrawLine (transform.position, path [i]);
				} else {
					Gizmos.DrawLine(path[i-1], path[i]);
				}
			}
		}
	}
}
