using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Holds the references and function calls for StateSaves so,
in the state Saver class the object to manipulate will display
and custom buttons exist for in editor editing
@author Br0wer
 */
public class StateCustom : MonoBehaviour
{
    StateSaver sS;
    /**
    Saves this current state to an array
    TODO: Will be replaced by Dan's RB Binary tree
     */
    public void saveThisState() {
        sS = GameObject.FindGameObjectWithTag("StateSaver").GetComponent<StateSaver>();

        State currState = new State (sS.objectToManipulate.position, sS.objectToManipulate.lossyScale, sS.objectToManipulate.rotation);
        sS.saveState(currState);
    }

    /**
    Prints the states in the array
     */
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

    /**
    Removes all the states from the array
     */
    public void clearAllStates() {
        sS = GameObject.FindGameObjectWithTag("StateSaver").GetComponent<StateSaver>();
        sS.saved.savedStates = null;
        Debug.Log("States Cleared!");
    }

    /**
    Switches to the next state in the array of states cyclicly
     */
    public void switchToNextState() {
        sS.currState++;
        if (sS.currState >= sS.saved.savedStates.Length) {
            sS.currState = 0;
        }

        State currState = sS.saved.savedStates[sS.currState];
        sS.objectToManipulate.position = currState.getPos();
        sS.objectToManipulate.rotation = currState.getRot();
        sS.objectToManipulate.localScale = currState.getScale();
    }
}
