﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StateCustom))]
public class StateEditor : Editor
{
    public override void OnInspectorGUI() {
        GameObject root = GameObject.FindGameObjectWithTag("StateSaver");
        if (GUILayout.Button("Save Current State")) {
            root.GetComponent<StateCustom>().saveThisState();
        }
        if (GUILayout.Button("Print All States")) {
            root.GetComponent<StateCustom>().printAllStates();
        }
        if (GUILayout.Button("Clear All States")) {
            root.GetComponent<StateCustom>().clearAllStates();
        }
    }
}
