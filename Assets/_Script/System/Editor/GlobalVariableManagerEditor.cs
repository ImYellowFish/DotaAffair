using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GlobalVariableManager))]
public class GlobalVariableManagerEditor : Editor {

    private GlobalVariableManager gvm;
    private DictionaryEditorDrawer<string, GlobalVariable> dictDrawer;
    
    public override void OnInspectorGUI()
    {        
        if (gvm == null || dictDrawer == null)
        {
            gvm = target as GlobalVariableManager;
            dictDrawer = new DictionaryEditorDrawer<string, GlobalVariable>(this, "Variables", gvm.Data, DrawVar);
            gvm.OnValueChanged += OnValueChanged;
        }

        DrawDefaultInspector();
        dictDrawer.OnInspectorGUI();
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
        dictDrawer.RefreshData();
    }
    
}
