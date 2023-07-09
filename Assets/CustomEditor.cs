using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[UnityEditor.CustomEditor(typeof(ProGen))]
public class CustomEditor : UnityEditor.Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ProGen progen = (ProGen)target;


        if (GUILayout.Button("Generate"))
        {
            progen.Generate();
        }
        
        if(GUILayout.Button("Clear")) progen.Clear();

    }
}
