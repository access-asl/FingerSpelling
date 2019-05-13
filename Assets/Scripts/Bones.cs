using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Gives reference as to what a bone is
a class given to the bone object by way of 
inheritance form the initial start.
Used to show rotation around the base of
an object.
Will be placed in the IKTarget object
@author Brower
 */

public class Bones : MonoBehaviour
{
    //A bool that when true will show a gui representation of the current config for each bone
    public bool debugging;

    //Holds the type of bone the current selection is
    public enum BoneType {
        Standard,
        IK,
        FK
    };

    //Fill from root to leaf without having a parented armature
    [Header("Animation Properties")]

    [Tooltip("Root->Leaf only, all bones must be at the same level.")]
    public BoneObj[] bones;

    //Creates a public reference to be edidted in the unity editor
    public BoneType boneType;

    [Tooltip("Gives the bones a thing to look at in reference to the rest of the object")]
    public Transform poleTarget;
    [Tooltip("The object that moves the bone structure.")]
    public Transform IKTarget;
    //Stores the previous position of the target
    private Vector3 previousTargetPos;

    public Transform tipOfTheChain;

    void Start() {
        previousTargetPos = this.transform.position;
        Initialize();
    }

    void Update() {
        if (previousTargetPos != this.transform.position) {
            Solve();
        }
    }

    void Initialize() {
        bones[bones.Length-1].origPos = bones[bones.Length-1].transform.position;
        bones[bones.Length-1].origScale = bones[bones.Length-1].transform.position;
        bones[bones.Length-1].boneRotation = bones[bones.Length-1].transform.rotation;
        GameObject g = new GameObject();
        g.name = bones[bones.Length-1].transform.name;
        g.transform.position = bones[bones.Length-1].transform.position;
        g.transform.up = -(tipOfTheChain.position - bones[bones.Length-1].transform.position);
        g.transform.parent = bones[bones.Length-1].transform.parent;
        bones[bones.Length-1].transform.parent = g.transform;
        for (int i = 0; i < bones.Length-1; i++) {
            bones[i].origPos = bones[i].transform.position;
            bones[i].origScale = bones[i].transform.localScale;
            bones[i].boneRotation = bones[i].transform.rotation;
            bones[i].length = Vector3.Distance(bones[i - 1].transform.position, bones[i].transform.position);
            g = new GameObject();
            g.name = bones[i].transform.name;
            g.transform.position = bones[i].transform.position;
            g.transform.up = -(bones[i - 1].transform.position - bones[i].transform.position);
            g.transform.parent = bones[i].transform.parent;
            bones[i].transform.parent = g.transform;
        }
    }

    void Solve() {
        Vector3 rootPos = bones[0].GetJointOnePos();
        bones[0].transform.up = -(poleTarget.position - bones[0].transform.position);
        for (int i = 1; i < bones.Length-1; i++) {
            bones[i].transform.position = bones[i-1].transform.position + (-bones[i-1].transform.up * bones[i-1].GetLength());
            bones[i].transform.up = -(poleTarget.position - bones[i].transform.position);
        }
        for (int i = 0; i < 5; i++) {
            bones[bones.Length-1].transform.up = -(transform.position - bones[0].transform.position);
            bones[bones.Length-1].transform.position = transform.position - (-bones[0].transform.up * bones[bones.Length-1].GetLength());
            for (int j = bones.Length-2; j > 1; j--) {
                bones[j].transform.up = -(bones[j+1].transform.position - bones[j].transform.position);
                bones[j].transform.position = bones[j+1].transform.position - (-bones[j].transform.up * bones[j].GetLength());
            }
            bones[bones.Length - 1].transform.position = rootPos;
            for (int j = 1; j < bones.Length - 1; j++) {
                bones[j].transform.position = bones[j-1].transform.position + (-bones[j-1].transform.up * bones[j-1].GetLength());
            }
        }
        previousTargetPos = transform.position;
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(IKTarget.position, new Vector3(.1f, .1f, .1f));
        Gizmos.DrawCube(poleTarget.position, new Vector3(.1f, .1f, .1f));
    }
}
