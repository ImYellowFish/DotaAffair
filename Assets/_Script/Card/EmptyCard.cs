using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCard : ICard {
    public CardInfo cardInfo { get { return m_CardInfo; } }
    
    private CardInfo m_CardInfo = new CardInfo();
    

    public void OnLeftChoice()
    {
        Debug.Log("Left");
    }

    public void OnRightChoice()
    {
        Debug.Log("Right");
    }
}
