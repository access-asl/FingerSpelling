using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Gives reference as to what a bone is
a class given to the bone object by way of 
inheritance form the initial start.
Used to show rotation around the base of
an object.
@author Brower
 */

public class Bones : MonoBehaviour
{
    //A bool that when true will show a gui representation of the current config for each bone
    public bool debugging;

    //Holds refence to the type of base this bone has (currently the only option is bottom)
    public enum BaseLocation {
        Bottom
    };

    //The public refence of the base of the bone's location
    public BaseLocation boneBase;


    //Holds the type of bone the current selection is
    public enum BoneType {
        Standard,
        IK,
        FK
    };

    //Creates a public reference to be edidted in the unity editor
    public BoneType boneType;

    /**
    Will calculate the bottom position of this bone
    for  the initial creation will only work when the
    bone's rotation as a Euler is (0, 0, 0)
    */
    public Vector3 getBottomCenter() {
        Vector3 returnPos;

        Vector3 objectPos = transform.position;
        returnPos.y = objectPos.y - (1.5f);
        returnPos.z = objectPos.z;
        returnPos.x = objectPos.x;

        return returnPos;
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(getBottomCenter(), new Vector3(.1f, .1f, .1f));
    }

}
