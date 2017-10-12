using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DotaAffair.Extensions;

/// <summary>
/// Parses cardInfo
/// </summary>
public static class CardUtility {
    private static readonly string SUCCESSION_IDENTIFIER = ">";

    public static void ParseNextCard(ICard card, string nextCardID)
    {
        if (nextCardID == "")
            return;
        if (nextCardID == SUCCESSION_IDENTIFIER)
            Table.Instance.cardDispatcher.Dispatch(card.cardInfo.cardData.Succession());
        else
            Table.Instance.cardDispatcher.Dispatch(nextCardID);
    }
}
