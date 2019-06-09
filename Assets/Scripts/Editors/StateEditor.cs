using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StateCustom))]
public class StateEditor : Editor
{
    public override void OnInspectorGUI() {
        if (GUILayout.Button("Save Current State")) {
            GameObject root = GameObject.FindGameObjectWithTag("StateSaver");
            root.GetComponent<StateCustom>().saveThisState();
        }
    }
}
