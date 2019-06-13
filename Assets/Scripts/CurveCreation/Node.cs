using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
This stores a singlular node, its possibilty as a navigable spot,
its position and so fourth
@author Br0wer
 */
public class Node
{
    public enum NodeState {
        available,
        taken,
        nonOptimal,
    } ;

    public NodeState nodeState;

    public Vector3 position;

    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;

    public Node prev;

    public Node (NodeState nodeState, Vector3 position, int gridX, int gridY) {
        this.nodeState = nodeState;
        this.position = position;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost  { get {return gCost + hCost;} }
}
