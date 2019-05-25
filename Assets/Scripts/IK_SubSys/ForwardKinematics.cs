using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
This class will make it so when one bone is rotated all of it's children are also rotated.
@author Brower
 */
public class ForwardKinematics : MonoBehaviour
{
    /**
    This will take an array of bones to move with bones[0] being the base
    and everything else follows this one bone in an arc.
    @bonesToMove the bones to move with [0] being the base
    @rotation The rotation to put the bones at
     */
    public void ForwardKinematic(Bone[] bonesToMove) {
        fkRotation(bonesToMove);
    }

    void fkRotation(Bone[] bonesToMove) {
        for (int i = 0; i < bonesToMove.Length; i++) {
            if (i == 0) {
                for(int j = i+1; j < bonesToMove.Length; j++) {
                    bonesToMove[j].transform.parent = bonesToMove[j-1].transform;
                }
                rotateOnArc(bonesToMove[i].boneRotation, bonesToMove[i], bonesToMove[i].getBottom());
                bonesToMove[i].refresh();
            } else {
                for(int j = i+1; j < bonesToMove.Length; j++) {
                    bonesToMove[j].transform.parent = bonesToMove[j-1].transform;
                }
                rotateOnArc(bonesToMove[i].boneRotation, bonesToMove[i], bonesToMove[i-1].getTop());
                bonesToMove[i].refresh();
            }
        }


        // for (int i = 0; i < bonesToMove.Length; i++) {
        //     if (i == 0) {
        //         rotateOnArc(bonesToMove[i].boneRotation, bonesToMove[i], bonesToMove[i].getBottom());
        //         bonesToMove[i].refresh();
        //     } else {
        //         rotateOnArc(bonesToMove[i].boneRotation, bonesToMove[i], bonesToMove[i-1].getTop());
        //         bonesToMove[i].refresh();
        //         for (int j = i; j < bonesToMove.Length; j++) {
        //             rotateOnArc(bonesToMove[i].boneRotation, bonesToMove[j], bonesToMove[j-1].getTop());
        //             bonesToMove[j].refresh();
        //         }
        //     }
        // }
    }

    /**
    Rotates a given bone around the base point
    @rotation the quaternion to rotate to
    @bone the bone to rotate
    @pivot the point to spin about
     */
    public void rotateOnArc(Quaternion rotation, Bone bone, Vector3 pivot) {
        bone.setPivot(pivot);
        bone.boneRotation = rotation;
    }

    /**
    Gets the local position of the pos given a pivot point
    @pos the unaltered position of the bone
    @pivot the base point(the bottom of the root bone)
     */
    Vector3 getLocationRelative(Vector3 pos, Vector3 pivot) {
        Vector3 final = new Vector3(0, 0, 0);
        final.x = -(pos.x - pivot.x);
        final.y = -(pos.y - pivot.y);
        final.z = -(pos.z - pivot.z);
        return final;
    }
}