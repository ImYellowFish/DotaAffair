using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : ICard {
    public CardInfo cardInfo { get { return m_CardInfo; } }
    private CardInfo m_CardInfo;
    
    public Card(CardDataEntry cardData)
    {
        m_CardInfo = new CardInfo(cardData);
    }

    public void OnLeftChoice()
    {
        Debug.Log("Left");
        CardUtility.ParseCardValues(this, m_CardInfo.cardData.left_value);
        CardUtility.ParseCardEvent(this, true);
        CardUtility.ParseNextCard(this, m_CardInfo.leftNextCard);
    }

    public void OnRightChoice()
    {
        Debug.Log("Right");
        CardUtility.ParseCardValues(this, m_CardInfo.cardData.right_value);
        CardUtility.ParseCardEvent(this, false);
        CardUtility.ParseNextCard(this, m_CardInfo.rightNextCard);
    }
}
