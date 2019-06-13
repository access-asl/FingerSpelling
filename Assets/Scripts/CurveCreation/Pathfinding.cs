using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
This is a rudementary implementation of the a* pathfinding
algorith, as time progresses I will use this to make the 
scalling interpreter.
@author Br0wer
 */
public class Pathfinding : MonoBehaviour
{
    public Transform target, goal;
    Grid grid;

    void Awake() {
        grid = GetComponent<Grid>();
    }

    void Update() {
        findPath(target.position, goal.position);
    }

    void findPath(Vector3 a, Vector3 b) {
        Node start = grid.findNode(a);
        Node end = grid.findNode(b);

        List<Node> exploringSet = new List<Node>();
        exploringSet.Add(start);
        HashSet<Node> closedSet = new HashSet<Node>();

        while (exploringSet.Count > 0) {
            Node n = exploringSet[0];

            for (int i = 1; i < exploringSet.Count; i++) {
                if (exploringSet[i].fCost <= n.fCost) {
                    if (exploringSet[i].hCost < n.hCost) {
                        n = exploringSet[i];
                    }
                }
            }
            exploringSet.Remove(n);
            closedSet.Add(n);

            if (n == end) {
                tracePath(start, end);
                return;
            }

            foreach (Node neighbor in grid.getNeighbors(n)) {
                if (neighbor.nodeState != Node.NodeState.available || closedSet.Contains(neighbor)) {
                    continue;
                }

                int newCostToNeighbor = n.gCost + getDistance(n, neighbor);
                if (newCostToNeighbor < neighbor.gCost || !exploringSet.Contains(neighbor)) {
                    neighbor.gCost = newCostToNeighbor;
                    neighbor.hCost = getDistance(neighbor, end);
                    neighbor.prev = n;

                    if (!exploringSet.Contains(neighbor)) {
                        exploringSet.Add(neighbor);
                    }
                }
            }
        }
    }

    void tracePath(Node start, Node end) {
        List<Node> path = new List<Node>();
		Node currentNode = end;

		while (currentNode != start) {
			path.Add(currentNode);
			currentNode = currentNode.prev;
		}
		path.Reverse();

		grid.path = path;
    }

    int getDistance(Node nodeStart, Node nodeEnd) {
        int dstX = Mathf.Abs(nodeStart.gridX - nodeEnd.gridX);
		int dstY = Mathf.Abs(nodeStart.gridY - nodeEnd.gridY);

		if (dstX > dstY) {
			return 14*dstY + 10* (dstX-dstY);
        }
		return 14*dstX + 10 * (dstY-dstX);
    }
}