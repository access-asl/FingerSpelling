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
        rotateOnArc(bonesToMove[0].boneRotation, bonesToMove[0], bonesToMove[0].getBottom());
        bonesToMove[0].refresh();
        for (int i = 1; i < bonesToMove.Length; i++) {
            rotateOnArc(bonesToMove[i].boneRotation, bonesToMove[i], bonesToMove[i-1].getTop());
            bonesToMove[i].refresh();
        }
    }

    /**
    Rotates a given bone around the base point
    @rotation the quaternion to rotate to
    @bone the bone to rotate
    @pivot the point to spin about
     */
    public void rotateOnArc(Quaternion rotation, Bone bone, Vector3 pivot) {
        bone.getBone().rotation = rotation;
        bone.refresh();
        Vector3 newBottomPos = bone.getBottom();
        Vector3 finalPos = new Vector3(0, 0, 0);
        finalPos.x = -(newBottomPos.x - pivot.x);
        finalPos.y = -(newBottomPos.y - pivot.y);
        finalPos.z = -(newBottomPos.z - pivot.z);
        bone.getBone().position += finalPos;
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