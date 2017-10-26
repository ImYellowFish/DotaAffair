using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DotaAffair.Condition;

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

    public bool strictGetVariable;


    #region Static functions
    public static GlobalVariableManager Instance { get { return DataManager.Instance.variable; } }

    public Dictionary<string, GlobalVariable> Data { get { return variables; } }

    public static GlobalVariable GetVar(string key) {
        return Instance.GetVarInternal(key);
    }

    public static void SetVar(string key, int val) {
        Instance.SetVarInternal(key, val, true);
    }

    public static readonly string KEY_BOY_HP = "main_boy_hp";
    public static readonly string KEY_GIRL_HP = "main_girl_hp";
    public static readonly string KEY_LOVE = "main_love";

    public static int boyHp {
        get { return GetVar(KEY_BOY_HP).value; }
        set { SetVar(KEY_BOY_HP, value); }
    }
    public static int girlHp {
        get { return GetVar(KEY_GIRL_HP).value; }
        set { SetVar(KEY_GIRL_HP, value); }
    }
    public static int love {
        get { return GetVar(KEY_LOVE).value; }
        set { SetVar(KEY_LOVE, value); }
    }
    #endregion
    

    private Dictionary<string, GlobalVariable> variables;
    public System.Action<string, int> OnValueChanged;
    
    public void Init()
    {
        variables = new Dictionary<string, GlobalVariable>();

        // set main values
        SetVarInternal(KEY_BOY_HP, 0, false);
        SetVarInternal(KEY_GIRL_HP, 0, false);
        SetVarInternal(KEY_LOVE, 0, false);

        // set init values according to csv
        var gvTable = CsvDataManager.Instance.variable;
        foreach (var variable in gvTable.Values) {
            SetVarInternal(variable.id, variable.initial_value, false);
        }

        // invoke value changed callback
        if (OnValueChanged != null)
            OnValueChanged("", 0);
    }
    
    public GlobalVariable GetVarInternal(string key)
    {
        key = key.Trim();

        if (!variables.ContainsKey(key))
        {
            if (strictGetVariable)
                throw new System.ArgumentException("Cannot find variable: " + key);

            variables.Add(key, new GlobalVariable(key, 0));
        }
        return variables[key];
    }

    public void SetVarInternal(string key, int val, bool invokeCallback)
    {
        key = key.Trim();
        if (variables.ContainsKey(key))
            variables[key].value = val;
        else
            variables.Add(key, new GlobalVariable(key, val));

        if (invokeCallback && OnValueChanged != null)
            OnValueChanged(key, val);
    }

}
