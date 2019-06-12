using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
This class constructs a grid of nodes that will be representable visually.
The unavailable spaces will be black, available will be white, and prefered unavailable will be grey.
@author Br0wer
 */
public class Grid : MonoBehaviour
{
    public LayerMask fuckThisObjectMask;
    public Vector2 gridSize;
    public float nodeSize;
    Node[,] grid;

    float nodeRadius;
    int gridSizeX, gridSizey;

    void Start() {
        nodeRadius = nodeSize/2;
        gridSizeX = Mathf.RoundToInt(gridSize.x/nodeSize);
        gridSizey = Mathf.RoundToInt(gridSize.y/nodeSize);
        CreateGrid();
    }

    void Update() {
        CheckGrid();
    }

    void CreateGrid() {
        grid = new Node[gridSizeX, gridSizey];
        for(int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizey; y++) {
                Vector3 nodePos = new Vector3(x, y, 0);
                bool posOcupied = Physics.CheckSphere(nodePos, nodeRadius, fuckThisObjectMask);
                Node.NodeState nodeState = posOcupied == true ? 
                    nodeState = Node.NodeState.taken : 
                    nodeState = Node.NodeState.available;
                grid[x,y] = new Node(nodeState, nodePos);
            }
        }
    }

    void CheckGrid() {
        for(int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizey; y++) {
                bool posOcupied = Physics.CheckSphere(grid[x,y].position, nodeRadius, fuckThisObjectMask);
                Node.NodeState nodeState = posOcupied == true ? 
                    nodeState = Node.NodeState.taken : 
                    nodeState = Node.NodeState.available;
                grid[x,y].nodeState = nodeState;
            }
        }
    }

    void OnDrawGizmos() {
        Vector3 mid = new Vector3 (gridSize.x/2 - nodeRadius, gridSize.y/2 - nodeRadius, 0);
        Gizmos.DrawWireCube(mid, new Vector3(gridSize.x, gridSize.y, 1));

        if (grid != null) {
            foreach (Node node in grid) {
                Node.NodeState nState = node.nodeState;
                switch (nState) {
                    case Node.NodeState.available:
                        Gizmos.color = new Color(255, 255, 255, .5f);
                        Gizmos.DrawCube(node.position, Vector3.one);
                        break;
                    case Node.NodeState.taken:
                        Gizmos.color = new Color(0, 0, 0, .5f);
                        Gizmos.DrawCube(node.position, Vector3.one);
                        break;
                    case Node.NodeState.nonOptimal:
                        Gizmos.color = new Color(150, 150, 150, .5f);
                        Gizmos.DrawCube(node.position, Vector3.one);
                        break;
                }
            }
        }
    }
}
