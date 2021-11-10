using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GalaxyEditor : EditorWindow
{
    [MenuItem("Window/Galaxy Editor")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(GalaxyEditor));
    }

    void OnGUI()
    {
        // The actual window code goes here
    }
}
