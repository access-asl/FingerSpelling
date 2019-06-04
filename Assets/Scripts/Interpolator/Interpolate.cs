using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
This will go linearly from two given positions and rotations over the given time
Yuh.
@author Brower
 */
public class Interpolate : MonoBehaviour
{
    [System.Serializable]
    public class RotationInfo{
        public Vector3 startRotation;
        public Vector3 endRotation;
    }

    [System.Serializable]
    public class PositionInfo{
        public Vector3 startPosition;
        public Vector3 endPosition;
    }

    public float time;

    public RotationInfo rotations;
    public PositionInfo positions;


    protected Quaternion calculatedStartRotation;
    protected Quaternion calculatedEndRotation;

    void Start() {
        calculatedStartRotation = getRotation(rotations.startRotation);
        calculatedEndRotation = getRotation(rotations.endRotation);
    }

    Quaternion getRotation(Vector3 eulerRotation) {
        return Quaternion.Euler(eulerRotation);
    }

}
