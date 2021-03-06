﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/**
The class that controlls Inverse Kinematics or linear progression of
animation.
@author Brower
 */
public class InverseKinematics : MonoBehaviour
{
    [System.Serializable]
    public class BoneInfo {
        public bool debuggingBone;
        public int chainLength;
    }

    [System.Serializable]
    public class InverseKinematicsVariables {
        public Transform target;
        public Transform pole;
        public int iterations;
        public float delta = .1f;
    }

    public BoneInfo boneInformation;
    public InverseKinematicsVariables ikModifiers;
    protected Quaternion TargetInitialRotation;
    protected Quaternion EndInitialRotation;
    protected Transform[] bones;
    protected float completedLength;

    void Awake() {
        bones = new Transform[boneInformation.chainLength + 1];
        TargetInitialRotation = ikModifiers.target.rotation;
        EndInitialRotation = transform.rotation;

        Transform cur = this.transform;
        completedLength = 0;
        for (int i = boneInformation.chainLength - 1; i >= 0; i--) {
            completedLength += (cur.position - cur.parent.position).magnitude;
            bones[i+1] = cur;
            bones[i] = cur.parent;
            cur = cur.parent;
        }
    }

    void LateUpdate() {
        Transform lastBone = bones[bones.Length-1];
        for (int iterations = 0; iterations < ikModifiers.iterations; iterations++) {
            for (int i = bones.Length - 1; i >= 0; i--) {
                if (i == bones.Length - 1) {
                    bones[i].rotation = ikModifiers.target.rotation * Quaternion.Inverse(TargetInitialRotation) * EndInitialRotation;
                } else {
                    bones[i].rotation = Quaternion.FromToRotation(lastBone.position - bones[i].position, ikModifiers.target.position - bones[i].position) * bones[i].rotation;

                    if (ikModifiers.pole != null && i+2 <= bones.Length - 1) {
                        Plane plane = new Plane(bones[i+2].position - bones[i].position, bones[i].position);
                        Vector3 projectedPole = plane.ClosestPointOnPlane(ikModifiers.pole.position);
                        Vector3 projectedBone = plane.ClosestPointOnPlane(bones[i+1].position);

                        if ((projectedBone - bones[i].position).sqrMagnitude > .01f) {
                            float angle = Vector3.SignedAngle(projectedBone - bones[i].position, projectedPole - bones[i].position, plane.normal);
                            bones[i].rotation = Quaternion.AngleAxis(angle, plane.normal) * bones[i].rotation;
                        }
                    }
                }

                if ((lastBone.position - ikModifiers.target.position).sqrMagnitude < ikModifiers.delta * ikModifiers.delta) {
                    break;
                }
            }
        }
    }

    void OnDrawGizmos() {
        if (boneInformation.debuggingBone) {
            Gizmos.color = new Color(0, 200, 255, .5f);
            Gizmos.DrawCube(ikModifiers.pole.position, Vector3.one * .2f);
            Gizmos.color = new Color(255, 0, 0, .5f);
            Gizmos.DrawCube(ikModifiers.target.position, Vector3.one * .2f);
            var cur = this.transform;
            for (int i = 0; i < boneInformation.chainLength && cur != null && cur.parent != null; i++) {
                var scale = Vector3.Distance(cur.position, cur.parent.position) * .1f;
                Handles.matrix = Matrix4x4.TRS(cur.position, Quaternion.FromToRotation(Vector3.up, cur.parent.position - cur.position), new Vector3(scale, Vector3.Distance(cur.parent.position, cur.position), scale));
                Handles.color = new Color(0, 200, 255, .5f);
                Handles.DrawWireCube(Vector3.up * .5f, Vector3.one);
                cur = cur.parent;
            }
        }
    }
}