using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private string inputString;

    /*
    takes the inputString and will return the length of the
    string with only letter
    TODO:  add digits
     */
    private int getLength(string str) {
        int length = 0;
        for (int i = 0; i < inputString.Length; i++) {
            if (char.IsLetter(str[i]) || char.IsSeparator(str[i]))
                length++;
        }
        return length;
    }

    /*
    splits inputString into a char[] of only letters and spaces
     */
    public char[] splitString(string str) {
        char[] returnArray = new char[getLength(str)];

        int arrayIdx = 0;
        for (int i = 0; i < str.Length; i++) {
                if (char.IsLetter(str[i]) || char.IsSeparator(str[i])) {
                    returnArray[arrayIdx] = char.ToLower(str[i]);
                    arrayIdx++;
                }
        }

        return returnArray;
    }

    /*
    returns the inputString
     */
    public string getInputString() {
        return inputString;
    }

    /*
    sets the input string to a given string
     */
    public void setInputString(string str) {
        inputString = str;
    }
}
