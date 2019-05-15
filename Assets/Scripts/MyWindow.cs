using UnityEditor;
using UnityEngine;

public class MyWindow : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
 float myFloat = 1.23f;
    Path path;
    //PathCreator creator;
    
    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/My Window")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(MyWindow));
    }
    // void OnSceneGUI()
    // {
    //     Draw();
    // }
    // void Draw()
    // {

    //     for (int i = 0; i < path.NumSegments; i++)
    //     {
    //         Vector2[] points = path.GetPointsInSegment(i);
    //         Handles.color = Color.black;
    //         Handles.DrawLine(points[1], points[0]);
    //         Handles.DrawLine(points[2], points[3]);
    //         Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green, null, 2);
    //     }

    //     Handles.color = Color.red;
    //     for (int i = 0; i < path.NumPoints; i++)
    //     {
    //         Vector2 newPos = Handles.FreeMoveHandle(path[i], Quaternion.identity, .1f,  Vector2.zero, Handles.CylinderHandleCap);
    //         if (path[i] != newPos)
    //         {
    //             newPos.x = myFloat;
    //             //Undo.RecordObject(creator, "Move point");
    //             path.MovePoint(3, newPos);
    //             Debug.Log(newPos.x);
    //             Debug.Log(myFloat);
    //         }
    //     }
    // }
    void OnGUI()
    {
        GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField ("Text Field", myString);
        
        groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
            myBool = EditorGUILayout.Toggle ("Toggle", myBool);
            myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup ();
        //Debug.Log(myFloat);
    }
}