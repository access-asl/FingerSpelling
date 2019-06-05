using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSaver : MonoBehaviour
{
    public Transform objectToManipulate;
    
    [System.Serializable]
    public class Saved {
        public State[] savedStates;
    }

    public Saved saved;

    public void saveState(State stateToSave) {
        State[] newArray = new State[saved.savedStates.Length];
        for (int i = 0; i < saved.savedStates.Length; i++) {
            newArray[i] = saved.savedStates[i];
        }
        newArray[saved.savedStates.Length] = stateToSave;
        saved.savedStates = newArray;
    }
}
