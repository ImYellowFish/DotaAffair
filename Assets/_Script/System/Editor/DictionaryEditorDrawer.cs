using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DictionaryEditorDrawer<TKey, TValue>{
    public delegate void ValueDrawer(TValue val);

    protected Editor host;
    private ValueDrawer drawer;
    private Dictionary<TKey, TValue> data;
    private List<TValue> gvList;
    private string dataHeadName;

    public DictionaryEditorDrawer(Editor host, string dataHeadName, Dictionary<TKey, TValue> data, ValueDrawer drawer) {
        this.host = host;
        this.dataHeadName = dataHeadName;
        this.data = data;
        this.drawer = drawer;
        RefreshData();
    }

    public void RefreshData() {
        if (data == null || drawer == null)
            return;
        gvList = new List<TValue>(data.Count);
        foreach (var gv in data.Values) {
            gvList.Add(gv);
        }

        host.Repaint();
    }

    public void OnInspectorGUI() {
        EditorGUILayout.LabelField(dataHeadName);
        EditorGUILayout.Space();
        DrawList();
    }


    private void DrawList() {
        if (gvList == null || data == null)
            return;

        if (gvList.Count != data.Count)
            RefreshData();

        for (int i = 0; i < gvList.Count; i++) {
            drawer.Invoke(gvList[i]);
        }
    }
    
}
