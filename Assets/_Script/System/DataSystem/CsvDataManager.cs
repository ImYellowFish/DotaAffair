using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvDataManager : MonoBehaviour {
    public static CsvDataManager Instance { get { return DataManager.Instance.csvData; } }
    
    public void Init()
    {
        card = CardDataTable.Create();
        cardIndexer = new CardIndexer(card);
        condition = ConditionDataTable.Create();
        variable = GlobalVariableDataTable.Create();
    }

    public CardDataTable card;
    public ConditionDataTable condition;
    public GlobalVariableDataTable variable;
    public CardIndexer cardIndexer;    
}
