using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCustom : MonoBehaviour
{
    StateSaver sS;
    public void saveThisState() {
        sS = GameObject.FindGameObjectWithTag("StateSaver").GetComponent<StateSaver>();

        if (sS == null) {
            Debug.Log("te fucl");
            return;
        }
        State currState = new State (sS.objectToManipulate.position, sS.objectToManipulate.lossyScale, sS.objectToManipulate.rotation);
        sS.saveState(currState);
        Debug.Log(currState.toString());
    }
}
