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
        CardUtility.ParseNextCard(this, m_CardInfo.leftNextCard);
    }

    public void OnRightChoice()
    {
        Debug.Log("Right");
        CardUtility.ParseNextCard(this, m_CardInfo.rightNextCard);
    }
}
