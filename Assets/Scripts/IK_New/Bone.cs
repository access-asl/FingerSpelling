using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
A new bone class will create a bone, and then use that to be able to
rotate an object around the base instead of the middle.
@author Brower
 */
public class Bone : MonoBehaviour
{
    public bool debug;

    //The Top and bottom on the bone
    private Vector3 bottom, top;

    //The bone object
    public Transform bone;

    //The rotation of the bone
    public Quaternion boneRotation;

    void Start() {
        bottom = calculateBottom();
        top = calculateTop();
    }

    public Vector3 getTop() { return this.top; }

    public Vector3 getBottom() { return this.bottom; }

    public Transform getBone() { return this.bone; }

    /**
    recalculates the top and bottom position of the bone
     */
    public void refresh() {
        bottom = calculateBottom();
        top = calculateTop();
    }

    /**
    Calculates the bottom position of the bone
     */
    private Vector3 calculateBottom() {
        //The distance in a downward direction to go to hit the exact bottom of the object
        float halfSize = bone.lossyScale.y/2;
        return (-bone.up) * halfSize;
    }

    /**
    Calculates the top position of the bone
     */
    private Vector3 calculateTop() {
        float halfSize = bone.localScale.y/2;
        return (bone.up) * halfSize;
    }

    /**
    For debugging, if debug is true then display some helper points
     */
    void OnDrawGizmos() {
        if (debug) {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawSphere(new Vector3(0, -.5f, 0), .1f);
            Gizmos.DrawCube(transform.position, new Vector3(.1f, .1f, .1f));
        }
    }
}
