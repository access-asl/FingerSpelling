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

    //The previous frames rotation of this bone;
    private Quaternion prevRotation;

    //The pivot to rotate the bone around
    private Vector3 pivot;

    void Start() {
        bottom = calculateBottom();
        top = calculateTop();
        prevRotation = boneRotation;
    }

    void Update() {
        rotateBone();
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
        return ((-bone.up) * halfSize) + getBone().position;
    }

    /**
    Calculates the top position of the bone
     */
    private Vector3 calculateTop() {
        float halfSize = bone.localScale.y/2;
        return ((bone.up) * halfSize) + getBone().position;
    }

    private void rotateBone() {
        Quaternion differenceInRotation = boneRotation * Quaternion.Inverse(prevRotation);
        transform.rotation = differenceInRotation;
        Vector3 prevBottom = getBottom();
        Vector3 prevTop = getTop();
        refresh();
        Vector3 finalPos = new Vector3(0, 0, 0);

        finalPos.x = -(bottom.x - pivot.x);
        finalPos.y = -(bottom.y - pivot.y);
        finalPos.z = -(bottom.z - pivot.z);
        transform.position += finalPos;
        prevRotation = boneRotation;
    }

    public void setPivot(Vector3 piv) { this.pivot = piv; }

    /**
    For debugging, if debug is true then display some helper points
     */
    void OnDrawGizmos() {
        if (debug) {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawSphere(new Vector3(0, -.5f, 0), .1f);
            Gizmos.color = new Color(0, 0, 1, .5f);
            Gizmos.DrawCube(calculateTop(), Vector3.one * .1f);
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawCube(calculateBottom(), Vector3.one * .1f);
        }
    }
}
