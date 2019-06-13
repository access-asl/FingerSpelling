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
    float prevNodeSize;
    Node[,] grid;

    float nodeRadius;
    int gridSizeX, gridSizey;
    
    public List<Node> path;

    void Start() {
        calculateGrid();
        createGrid();
    }

    void Update() {
        if (prevNodeSize == nodeSize) {
            checkGrid();
        } else {
            calculateGrid();
            createGrid();
        }
    }

    void calculateGrid() {
        nodeRadius = nodeSize/2;
        gridSizeX = Mathf.RoundToInt(gridSize.x/nodeSize);
        gridSizey = Mathf.RoundToInt(gridSize.y/nodeSize);
    }

    void createGrid() {
        grid = new Node[gridSizeX, gridSizey];
        for(int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizey; y++) {
                Vector3 nodePos = Vector3.right * (x * nodeSize) + Vector3.up * (y * nodeSize);
                bool posOcupied = Physics.CheckSphere(nodePos, nodeRadius, fuckThisObjectMask);
                Node.NodeState nodeState = posOcupied == true ? 
                    nodeState = Node.NodeState.taken : 
                    nodeState = Node.NodeState.available;
                
                grid[x,y] = new Node(nodeState, nodePos, x, y);
            }
        }
    }

    void checkGrid() {
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

    public List<Node> getNeighbors(Node n) {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x < 2; x++) {
            for (int y = -1; y < 2; y++) {
                if (x==0 && y==0) {
                    continue;
                }

                int validX = n.gridX + x;
                int validY = n.gridY + y;

                if (validX >= 0 && validX < gridSizeX && validY >= 0 && validY < gridSizey) {
                    neighbors.Add(grid[validX,validY]);
                }
            }
        }
        return neighbors;
    }

    public Node findNode(Vector3 pos) {
        float xNode = (pos.x + gridSize.x/2)/gridSize.x;
        float yNode = (pos.y + gridSize.y/2)/gridSize.y;

        xNode = Mathf.Clamp01(xNode);
        yNode = Mathf.Clamp01(yNode);

        int x = Mathf.RoundToInt((gridSizeX-1) * xNode);
        int y = Mathf.RoundToInt((gridSizey-1) * yNode);

        return grid[x,y];
    }

    void OnDrawGizmos() {
        Vector3 mid = new Vector3 (gridSize.x/2 - nodeRadius, gridSize.y/2 - nodeRadius, 0);
        Gizmos.DrawWireCube(mid, new Vector3(gridSize.x, gridSize.y, 1));

        if (grid != null) {
            foreach (Node node in grid) {
                if (path != null) {
                    if (path.Contains(node)) {
                        Gizmos.color = new Color(0, 200, 255);
                    Gizmos.DrawCube(node.position, Vector3.one * nodeSize);
                    }
                }
                Node.NodeState nState = node.nodeState;
                switch (nState) {
                    case Node.NodeState.available:
                        Gizmos.color = new Color(255, 255, 255, .5f);
                        Gizmos.DrawCube(node.position, Vector3.one * nodeSize);
                        break;
                    case Node.NodeState.taken:
                        Gizmos.color = new Color(0, 0, 0, .5f);
                        Gizmos.DrawCube(node.position, Vector3.one * nodeSize);
                        break;
                    case Node.NodeState.nonOptimal:
                        Gizmos.color = new Color(150, 150, 150, .5f);
                        Gizmos.DrawCube(node.position, Vector3.one * nodeSize);
                        break;
                }
            }
        }
    }
}
