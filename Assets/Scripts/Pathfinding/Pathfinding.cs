using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class Pathfinding : MonoBehaviour {
	//public Transform seeker, target;

	private PathRequestManager requestManager;
	private Grid grid;
	private int GetDistance(Node nodeA, Node nodeB) {
		int distanceX = Mathf.Abs (nodeA.gridX - nodeB.gridX);
		int distanceY = Mathf.Abs (nodeA.gridY - nodeB.gridY);
		if (distanceX > distanceY) {
			return 14 * distanceY + 10 * (distanceX - distanceY);
		} else {
			return 14 * distanceX + 10 * (distanceY - distanceX);
		}
	}

	//void Update() {
	//	FindPath (seeker.position, target.position);
	//}

	void Awake() {
		requestManager = GetComponent<PathRequestManager> ();
		// This means that Pathfinding and Grid must be on the same game object!!!
		grid = GetComponent<Grid> ();
	}

	public void StartFindPath(Vector3 startPos, Vector3 targetPos) {
		StartCoroutine (FindPath (startPos, targetPos));
	}

	IEnumerator FindPath(Vector3 startPos, Vector3 targetPos) {
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;
		Node startNode = grid.NodeFromWorldPoint (startPos);
		Node targetNode = grid.NodeFromWorldPoint (targetPos);
		if (startNode.walkable && targetNode.walkable) {
			Heap<Node> openSet = new Heap<Node> (grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node> ();
			openSet.Add (startNode);
			while (openSet.Count > 0) {
				Node currentNode = openSet.Pop ();
				closedSet.Add (currentNode);
				if (currentNode == targetNode) {
					pathSuccess = true;
					break;
				}
				foreach (Node neighbor in grid.GetNeighbors(currentNode)) {
					if (!neighbor.walkable || closedSet.Contains (neighbor)) {
						continue;
					}
					int newMovementCostToNeighbor = currentNode.gCost + GetDistance (currentNode, neighbor);
					if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains (neighbor)) {
						neighbor.gCost = newMovementCostToNeighbor;
						neighbor.hCost = GetDistance (neighbor, targetNode);
						neighbor.parent = currentNode;

						if (!openSet.Contains (neighbor)) {
							openSet.Add (neighbor);
						} else {
							openSet.UpdateItem (neighbor);
						}
					}
				}
			}
		}
		yield return null;
		if (pathSuccess) {
			waypoints = RetracePath (startNode, targetNode);
		}
		requestManager.FinishedProcessingPath (waypoints, pathSuccess);
	}

	private Vector3[] RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node> ();
		Node currentNode = endNode;
		while (currentNode != startNode) {
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}
		Vector3[] waypoints = SimplifyPath (path);
		Array.Reverse (waypoints);
		return waypoints;

	}

	private Vector3[] SimplifyPath(List<Node> path) {
		List<Vector3> waypoints = new List<Vector3> ();
		Vector2 directionOld = Vector2.zero;
		for (int i = 1; i < path.Count; i++) {
			Vector2 directionNew = new Vector2 (path [i - 1].gridX - path [i].gridX, path [i - 1].gridY - path [i].gridY);
			if (directionNew != directionOld) {
				waypoints.Add (path [i].worldPosition);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray ();
	}

}
