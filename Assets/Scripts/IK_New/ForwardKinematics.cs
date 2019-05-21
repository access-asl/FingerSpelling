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
        Vector3 root = bonesToMove[0].getBottom();
        rotateOnArc(bonesToMove[0].boneRotation, bonesToMove[0], root);
        bonesToMove[0].refresh();
        for (int i = 1; i < bonesToMove.Length; i++) {
            setBoneBottomPos(bonesToMove[i], bonesToMove[i-1].getTop());
            bonesToMove[i].refresh();
        }
        for (int i = 1; i < bonesToMove.Length; i++) {
            rotateOnArc(bonesToMove[i].boneRotation, bonesToMove[i], bonesToMove[i-1].getTop());
            setBoneBottomPos(bonesToMove[i], bonesToMove[i-1].getTop());
            bonesToMove[i].refresh();
        }



        // Vector3 root = bonesToMove[0].getBottom();
        // rotateOnArc(bonesToMove[0].boneRotation, bonesToMove[0], root);
        // setBoneBottomPos(bonesToMove[1], bonesToMove[0].getTop());
        // foreach(Bone b in bonesToMove) {
        //     b.refresh();
        // }
        // rotateOnArc(Quaternion.Inverse(bonesToMove[0].boneRotation) * bonesToMove[1].boneRotation, bonesToMove[1], bonesToMove[0].getTop());
        
        
        // rotateOnArc(bonesToMove[1].boneRotation, bonesToMove[1], bonesToMove[0].getTop());
        //set the bottom position of the bone[1] to the top pos of the bone[0]
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
        Vector3 newBottomPos = bone.getBottom();//getLocationRelative(bone.getBottom(), pivot);
        Vector3 finalPos = new Vector3(0, 0, 0);
        finalPos.x = -(newBottomPos.x - pivot.x);
        finalPos.y = -(newBottomPos.y - pivot.y);
        finalPos.z = -(newBottomPos.z - pivot.z);
        bone.getBone().position += finalPos;
    }

    /**
    Sets the bottom pos of a bone to a given position
    @bone the bone to set to the bottom
    @pos the position to set the bone's bottom to.
     */
    void setBoneBottomPos(Bone bone, Vector3 pos) {
        Vector3 newPos = getLocationRelative(bone.getBone().position, pos);
        bone.getBone().position = newPos;
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