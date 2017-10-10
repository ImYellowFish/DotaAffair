using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCard : ICard {
    public CardInfo cardInfo { get { return m_CardInfo; } }
    
    private CardInfo m_CardInfo = new CardInfo { ID = "Empty" };
    

    public void OnLeftChoice()
    {
        Debug.Log("Left");
        Table.Instance.cardDispatcher.Dispatch("left");
    }

    public void OnRightChoice()
    {
        Debug.Log("Right");
        Table.Instance.cardDispatcher.Dispatch("right");
    }
}
