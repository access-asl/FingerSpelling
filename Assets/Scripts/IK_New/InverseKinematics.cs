using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseKinematics : MonoBehaviour
{
    [System.Serializable]
    public class BoneInformation {
        [Tooltip("The array of bones Base->Tip base being the bone that hardly moves, tip being the bone that touches the aimTarget is at all possible.")]
        public Bone[] bones;
        public Transform poleTarget;
        public Transform aimTarget;
    }

    public Quaternion test;

    [Tooltip("This is where all bone properties are set.")]
    public BoneInformation boneInformation;

    void Update() {
        rotateOnArc(test, boneInformation.bones[0]);
    }

    void rotateOnArc(Quaternion rotation, Bone bone) {
        Vector3 startBottomPos = bone.getBottom();
        bone.getBone().rotation = rotation;
        bone.refresh();
        // bone.rotation = rotation;
        Vector3 newBottomPos = bone.getBottom();
        Vector3 finalPos = new Vector3(0, 0, 0);
        finalPos.x = -(newBottomPos.x - startBottomPos.x);
        finalPos.y = -(newBottomPos.y - startBottomPos.y);
        finalPos.z = -(newBottomPos.z - startBottomPos.z);
        if (startBottomPos != newBottomPos) {
            if (finalPos.x != 0 || finalPos.y != 0) {
                Debug.Log(startBottomPos + " start");
                Debug.Log(newBottomPos + " new");
                Debug.Log(finalPos.x + " x");
                Debug.Log(finalPos.y + " y");
                Debug.Log(finalPos + " final");
            }
        }
        bone.getBone().position += finalPos;
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(0, 1, 1, .5f);
        Gizmos.DrawCube(boneInformation.aimTarget.position, Vector3.one * .1f);
        Gizmos.color = new Color(1, 1, 0, .5f);
        Gizmos.DrawCube(boneInformation.poleTarget.position, Vector3.one * .1f);
    }
}
