//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by CSVDataUtility.ClassWriter.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Collections.Generic;
[System.Serializable]
[CSVFilename("GlobalVariable")]
[CSVDataAsset("GlobalVariableDataTable")]
public class GlobalVariableDataEntry : CSVDataUtility.IDataEntry{
            
	[CSVField("id")]
	public string id;

	[CSVField("initial value")]
	public int initial_value;



    public string internal_dataEntryID { 
        get {
            return id;    
        } 
    }
    
    [CSVInternalIndex]
    public int m_internal_dataEntryIndex;
    public int internal_dataEntryIndex { get { return m_internal_dataEntryIndex; } }
}



[System.Serializable]
public class GlobalVariableDataTable : CSVDataUtility.DataTable<GlobalVariableDataEntry>{
    public static GlobalVariableDataTable Create(){
        GlobalVariableDataTable datatable = new GlobalVariableDataTable();
        datatable.Read();
        return datatable;
    }
}

