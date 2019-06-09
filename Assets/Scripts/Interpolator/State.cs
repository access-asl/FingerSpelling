using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
An object to store position, scale, rotation
@author Br0wer
 */
public class State
{
    private Vector3 pos;
    private Vector3 scale;
    private Quaternion rot;

    public State(Vector3 pos, Vector3 scale, Quaternion rot) {
        this.pos = pos;
        this.scale = scale;
        this.rot = rot;
    }

    public Vector3 getPos() { return this.pos; }

    public Vector3 getScale() { return this.scale; }

    public Quaternion getRot() { return this.rot; }

    public void setPos(Vector3 pos) { this.pos = pos; }

    public void setScale(Vector3 scale) { this.scale = scale; }

    public void setRot(Quaternion rot) { this.rot = rot; }

    public string toString() {
        return "Position: " + getPos() + "\nScale: " + getScale() + "\nRotation: " + getRot().eulerAngles;
    }
}
