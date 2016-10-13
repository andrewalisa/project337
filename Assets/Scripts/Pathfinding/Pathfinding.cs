using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Pathfinding : MonoBehaviour {
	public Transform seeker, target;


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

	void Update() {
		FindPath (seeker.position, target.position);
	}

	void Awake() {
		// This means that Pathfinding and Grid must be on the same game object!!!
		grid = GetComponent<Grid> ();
	}
	void FindPath(Vector3 startPos, Vector3 targetPos) {
		Stopwatch sw = new Stopwatch ();
		Node startNode = grid.NodeFromWorldPoint (startPos);
		Node targetNode = grid.NodeFromWorldPoint (targetPos);

		Heap<Node> openSet = new Heap<Node> (grid.MaxSize);
		HashSet<Node> closedSet = new HashSet<Node> ();
		openSet.Add (startNode);
		while (openSet.Count > 0) {
			Node currentNode = openSet.Pop ();
			closedSet.Add (currentNode);
			if (currentNode == targetNode) {
				RetracePath (startNode, targetNode);
				return;
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

	private void RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node> ();
		Node currentNode = endNode;
		while (currentNode != startNode) {
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse ();
		grid.path = path;
	}

}
