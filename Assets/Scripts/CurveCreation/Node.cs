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

    public Node (NodeState nodeState, Vector3 position) {
        this.nodeState = nodeState;
        this.position = position;
    }
}
