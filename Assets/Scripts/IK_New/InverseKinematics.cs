using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
The class that controlls Inverse Kinematics or linear progression of
animation.
@author Brower
 */
 [RequireComponent(typeof(ForwardKinematics))]
public class InverseKinematics : MonoBehaviour
{
    /**
    This class organizes the code overall putting this IK system's bones pole target and
    aim bone in one place given the name Bone Information
     */
    [System.Serializable]
    public class BoneInformation {
        [Tooltip("The array of bones Base->Tip base being the bone that hardly moves, tip being the bone that touches the aimTarget is at all possible.")]
        public Bone[] bones;
        public Transform poleTarget;
        public Transform aimTarget;
    }

    [Tooltip("This is where all bone properties are set.")]
    public BoneInformation boneInformation;

    private ForwardKinematics fk;

    void Start() {
        fk = GetComponent<ForwardKinematics>();
    }

    void Update() {
        fk.ForwardKinematic(boneInformation.bones);
        // for(int i = 0; i < boneInformation.bones.Length-1; i++) {
            // fk.rotateOnArc(boneInformation.bones[i].boneRotation, boneInformation.bones[i], )
        // }
    }

    /**
    Splices an array at an index +1
    @idx the index to split the array
    @arrayToSplice the array to splice at a given index
     */
    Bone[] spliceArray(int idx, Bone[] arrayToSplice) {
        Bone[] res = new Bone[arrayToSplice.Length - idx];
        for (int i = idx; i < arrayToSplice.Length; i++) {
            res[i-idx] = arrayToSplice[i];
        }
        return res;
    }

    /**
    Just shows where the aim and pole target are
     */
    void OnDrawGizmos() {
        Gizmos.color = new Color(0, 1, 1, .5f);
        Gizmos.DrawCube(boneInformation.aimTarget.position, Vector3.one * .1f);
        Gizmos.color = new Color(1, 1, 0, .5f);
        Gizmos.DrawCube(boneInformation.poleTarget.position, Vector3.one * .1f);
    }
}