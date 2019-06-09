using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCustom : MonoBehaviour
{
    StateSaver sS;
    public void saveThisState() {
        sS = GameObject.FindGameObjectWithTag("StateSaver").GetComponent<StateSaver>();

        State currState = new State (sS.objectToManipulate.position, sS.objectToManipulate.lossyScale, sS.objectToManipulate.rotation);
        sS.saveState(currState);
    }

    public void printAllStates() {
        sS = GameObject.FindGameObjectWithTag("StateSaver").GetComponent<StateSaver>();

        if (sS.saved.savedStates == null) {
            Debug.Log("No States to print.");
            return;
        }
        foreach (State s in sS.saved.savedStates) {
            Debug.Log(s.toString());
        }
    }

    public void clearAllStates() {
        sS = GameObject.FindGameObjectWithTag("StateSaver").GetComponent<StateSaver>();
        sS.saved.savedStates = null;
        Debug.Log("States Cleared!");
    }
}
