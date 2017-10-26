using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DotaAffair.Condition;

[CustomEditor(typeof(ConditionManager))]
public class ConditionManagerEditor : Editor {
    private ConditionManager gvm;
    private DictionaryEditorDrawer<string, CardCondition> dictDrawer;


    public override void OnInspectorGUI() {
        if (gvm == null || dictDrawer == null) {
            gvm = target as ConditionManager;
            dictDrawer = new DictionaryEditorDrawer<string, CardCondition>(this, "Conditions", gvm.Data, DrawVar);
        }
        
        DrawDefaultInspector();
        dictDrawer.OnInspectorGUI();
    }

    public void DrawVar(CardCondition gv) {
        EditorGUILayout.LabelField(gv.data.id, GUILayout.MaxWidth(200));

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("IsTrue", GUILayout.MaxWidth(50));
        EditorGUILayout.LabelField("Variable", GUILayout.MaxWidth(150));
        EditorGUILayout.LabelField("L_Value", GUILayout.MaxWidth(50));
        EditorGUILayout.LabelField("Op", GUILayout.MaxWidth(50));
        EditorGUILayout.LabelField("R_Value", GUILayout.MaxWidth(50));
        EditorGUILayout.EndHorizontal();

        foreach(var e in gv.elements) {
            DrawElement(e);
        }

        EditorGUILayout.Space();
    }

    private void DrawElement(CardConditionElement e) {
        EditorGUILayout.BeginHorizontal();
        GUI.enabled = false;
        EditorGUILayout.LabelField(e.IsTrue().ToString(), GUILayout.MaxWidth(50));
        EditorGUILayout.TextField(e.data.variable, GUILayout.MaxWidth(150));
        EditorGUILayout.TextField(e.leftVariable.value.ToString(), GUILayout.MaxWidth(50));
        EditorGUILayout.LabelField(e.data._operator, GUILayout.MaxWidth(50));
        EditorGUILayout.TextField(e.data.value.ToString(), GUILayout.MaxWidth(50));
        GUI.enabled = true;
        EditorGUILayout.EndHorizontal();
    }
}
