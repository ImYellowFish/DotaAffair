using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardIndexer {
    private CardDataEntry[] array;
    
    public CardIndexer(CardDataTable cardDataTable)
    {
        array = cardDataTable.ValueArray;
    }

    public bool IsLast(CardDataEntry card)
    {
        return card.internal_dataEntryIndex + 1 == array.Length;
    }

    public CardDataEntry Next(CardDataEntry card)
    {
        return array[card.internal_dataEntryIndex + 1];
    }
}
