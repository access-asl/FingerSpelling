/*
This is the initial MOTHER FUCKING fingerspelling code
it will take in a string input and the animate the hand by going through
the mesh at first then it will implement the weight system and then move
shit out of the way.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class FingerSpelling : MonoBehaviour {
    private InputHandler stringInput;

    [System.Serializable]
    public class StringHandler {
        public string inputString;
    }

    public StringHandler inputHandler;

    // Start is called before the first frame update
    void Start() {
        stringInput = this.GetComponent(typeof(InputHandler)) as InputHandler;
        Debug.Log("Starting Scene...");
        stringInput.setInputString(inputHandler.inputString);
        char[] inString = stringInput.splitString(stringInput.getInputString());
        foreach(char c in inString) {
                Debug.Log(c);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
