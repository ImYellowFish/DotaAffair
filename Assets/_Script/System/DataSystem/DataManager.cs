using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
    public static DataManager Instance;

    public CsvDataManager csvData;
    public GlobalVariableManager variable;
    public ConditionManager condition;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Init();
    }
    
    private void Init() {
        csvData = GetComponent<CsvDataManager>();
        csvData.Init();

        variable = GetComponent<GlobalVariableManager>();
        variable.Init();

        condition = GetComponent<ConditionManager>();
        condition.Init();
    }
}
