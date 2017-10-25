using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GlobalVariableManager))]
public class GlobalVariableManagerEditor : Editor {
    
    private GlobalVariableManager gvm;
    private List<GlobalVariable> gvList;
    private bool initiated;

    
    public override void OnInspectorGUI()
    {        
        if (!initiated)
        {
            gvm = target as GlobalVariableManager;
            gvm.OnValueChanged += OnValueChanged;
            Refresh();
            initiated = true;
        }

        EditorGUILayout.LabelField("Variables");
        EditorGUILayout.Space();
        DrawList();
    }

    private void DrawList()
    {
        if (gvList == null)
            return;

        for(int i = 0; i < gvList.Count; i++)
        {
            DrawVar(gvList[i]);
        }
    }

    private void DrawVar(GlobalVariable gv)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(gv.key, GUILayout.MaxWidth(200));
        GUI.enabled = false;
        EditorGUILayout.TextField(gv.value.ToString(), GUILayout.MaxWidth(200));
        GUI.enabled = true;
        EditorGUILayout.EndHorizontal(); 
    }

    private void OnValueChanged(string key, int value)
    {
        Refresh();
    }

    private void Refresh()
    {
        if (gvm == null || gvm.Data == null)
            return;
        gvList = new List<GlobalVariable>(gvm.Data.Count);
        foreach(var gv in gvm.Data.Values)
        {
            gvList.Add(gv);
        }
    }
}
