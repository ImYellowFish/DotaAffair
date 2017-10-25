using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlobalVariable
{
    public string key;
    public int value;

    public GlobalVariable()
    {

    }

    public GlobalVariable(string key, int value)
    {
        this.key = key;
        this.value = value;
    }
}


public class GlobalVariableManager : MonoBehaviour {
    

    public static GlobalVariableManager Instance;
    private Dictionary<string, GlobalVariable> data;

    public Dictionary<string, GlobalVariable> Data { get { return data; } }
    public System.Action<string, int> OnValueChanged;

    public bool strictGetVariable;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Init();
    }

    private void Init()
    {
        data = new Dictionary<string, GlobalVariable>();
        SetVar("test", 14);
        SetVar("test2", 28);
    }

    public GlobalVariable GetVarInternal(string key)
    {
        key = key.Trim();

        if (!data.ContainsKey(key))
        {
            if (strictGetVariable)
                throw new System.ArgumentException("Cannot find variable: " + key);

            data.Add(key, new GlobalVariable(key, 0));
        }
        return data[key];
    }

    public void SetVarInternal(string key, int val)
    {
        key = key.Trim();
        if (data.ContainsKey(key))
            data[key].value = val;
        else
            data.Add(key, new GlobalVariable(key, val));

        if (OnValueChanged != null)
            OnValueChanged(key, val);
    }

    #region Static functions
    public static GlobalVariable GetVar(string key)
    {
        return Instance.GetVarInternal(key);
    }

    public static void SetVar(string key, int val)
    {
        Instance.SetVarInternal(key, val);
    }
    #endregion
}
