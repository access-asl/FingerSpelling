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
    private Vector3 bottom, top;
    public Transform bone;

    public Quaternion boneRotation;

    void Start() {
        bottom = calculateBottom();
        top = calculateTop();
    }

    public Vector3 getTop() { return this.top; }

    public Vector3 getBottom() { return this.bottom; }

    public Transform getBone() { return this.bone; }

    public void refresh() {
        bottom = calculateBottom();
        top = calculateTop();
    }

    private Vector3 calculateBottom() {
        //The distance in a downward direction to go to hit the exact bottom of the object
        float halfSize = bone.lossyScale.y/2;
        return (-bone.up) * halfSize;
    }

    private Vector3 calculateTop() {
        float halfSize = bone.localScale.y/2;
        return (bone.up) * halfSize;
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(new Vector3(0, -.5f, 0), new Vector3(.1f, .1f, .1f));
        Gizmos.DrawCube(transform.position, new Vector3(.1f, .1f, .1f));
    }
}
