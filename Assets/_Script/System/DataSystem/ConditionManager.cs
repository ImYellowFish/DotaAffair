using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DotaAffair.Condition;

public class ConditionManager : MonoBehaviour {
    public static ConditionManager Instance { get { return DataManager.Instance.condition; } }

    private Dictionary<string, CardCondition> conditions;
    public Dictionary<string, CardCondition> Data { get { return conditions; } }

    public void Init() {
        conditions = new Dictionary<string, CardCondition>();
        var data = CsvDataManager.Instance.condition;
        foreach(var c in data.Values) {
            conditions.Add(c.id, new CardCondition(c));
        }
    }
    
    public static bool Check(string conditionName) {
        return Instance.CheckCondition(conditionName);
    }

    public bool CheckCondition(string conditionName) {
        return GetCondition(conditionName).IsTrue();
    }

    public CardCondition GetCondition(string conditionName) {
        try {
            return conditions[conditionName];
        } catch {
            throw new System.ArgumentException("Cannot find condition: " + conditionName);
        }
    }
}
