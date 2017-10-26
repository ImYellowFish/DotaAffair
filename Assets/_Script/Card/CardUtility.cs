using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DotaAffair.Extensions;

/// <summary>
/// Parses cardInfo
/// </summary>
public static class CardUtility {
    private static readonly string SUCCESSION_IDENTIFIER = ">";

    public static bool ParseNextCard(ICard card, string nextCardID)
    {
        if (nextCardID == "")
            return false;
        if (nextCardID == SUCCESSION_IDENTIFIER)
            Table.Instance.cardDispatcher.Dispatch(card.cardInfo.cardData.Succession());
        else
            Table.Instance.cardDispatcher.Dispatch(nextCardID);
        return true;
    }

    public static void ParseCardEvent(ICard card, bool isLeft) {
        if (isLeft)
            card.cardInfo.cardData.Invoke_left_event();
        else
            card.cardInfo.cardData.Invoke_right_event();
    }
    
    public static void ParseCardValues(ICard card, List<int> valueChange) {
        var count = valueChange.Count;
        if (count >= 1)
            GlobalVariableManager.boyHp += valueChange[0];
        if (count >= 2)
            GlobalVariableManager.girlHp += valueChange[1];
        if (count >= 3)
            GlobalVariableManager.love += valueChange[2];
    }
}
