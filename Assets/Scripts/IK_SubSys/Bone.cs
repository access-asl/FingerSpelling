using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
A new bone class will create a bone, and then use that to be able to
rotate an object around the base instead of the middle.
@author Brower
 */
public class Bone : MonoBehaviour
{
     /**
     This class hold the major constructs of each bone and it's associative pole target
      */
     [System.Serializable]
     public class BoneConstructor {
          // The root position of the bone
          public Vector3 basePos;

          //The distance from this "knuckle" to the next knuckle in the chain
          public float boneLength;

          //the rotation of the knuckle
          public Quaternion baseRotation;
     }
}
