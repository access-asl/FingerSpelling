using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
The bone object, will take two endpoints and then create
a representation of a bone within the two spaces
@author Brower
 */
public class BoneObj
{
    //The first joint
    private Vector3 jointOne;
    //The second joint 
    private Vector3 jointTwo;
    //The Euler rotation of the bone
    private Vector3 boneRotation;

    //the distance between the two joints
    private double length;
    //The different types of rotation we can have default, Bottom
    public enum RotationType {
        Top,
        Center,
        Bottom
    };
    //The specific rotation type of this Bone
    private RotationType rotType;
    //When true it takes draws a line between the two digits for the bone
    public bool debug;

    /*
    The specific constructor for a bone object where everything is defined
     */
    public BoneObj(Vector3 jointOne, Vector3 jointTwo, RotationType rotType, bool debug) {
        this.jointOne = jointOne;
        this.jointTwo = jointTwo;
        this.length = Vector3.Distance(this.jointOne, this.jointTwo);
        this.rotType = rotType;
        this.debug = debug;
    }

    /*
    The default constructor will only take in jointOne jointTwo
     */
    public BoneObj(Vector3 jointOne, Vector3 jointTwo) {
        this.jointOne = jointOne;
        this.jointTwo = jointTwo;
        this.length = Vector3.Distance(this.jointOne, this.jointTwo);
        this.rotType = RotationType.Bottom;
    }

    void OnDrawGizmos() {
        if (debug) {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawLine(jointOne, jointTwo);
        }
    }

}
