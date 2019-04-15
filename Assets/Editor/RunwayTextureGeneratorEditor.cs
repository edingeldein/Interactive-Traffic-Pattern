using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RunwayTextureGenerator))]
public class RunwayTextureGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //RunwayTextureGenerator script = (RunwayTextureGenerator)target;

        //GUILayout.Label("Center Line Settings");
        //script.dashWidth = EditorGUILayout.IntField("Dash Width", script.dashWidth);
        //EditorGUILayout.IntField("Number of Dashes", script.dashWidth);

    }
}
