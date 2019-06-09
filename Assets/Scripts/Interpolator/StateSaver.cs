using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Takes an object to manipulate and then will go from where the object is to
where the object should go.
@author Br0wer
 */
 [RequireComponent(typeof(StateCustom))]
public class StateSaver : MonoBehaviour
{
    public Transform objectToManipulate;
    
    [System.Serializable]
    public class Saved {
        public State[] savedStates;
    }

    public Saved saved;

    public void saveState(State stateToSave) {
        State[] newArray;
        if (saved.savedStates == null) {
            newArray = new State[1];
        } else {
            newArray = new State[saved.savedStates.Length + 1];
        }
        for (int i = 0; i < saved.savedStates.Length; i++) {
            newArray[i] = saved.savedStates[i];
        }
        newArray[saved.savedStates.Length] = stateToSave;
        saved.savedStates = newArray;
    }
}
