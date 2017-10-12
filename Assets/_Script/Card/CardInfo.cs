using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardInfo
{    
    public CardDataEntry cardData;
    public string ID { get { return cardData.id; } }
    public string description { get { return cardData.description; } }
    public string leftText { get { return cardData.left_text; } }
    public string rightText { get { return cardData.right_text; } }

    public string leftNextCard { get { return cardData.left_next_card; } }
    public string rightNextCard { get { return cardData.right_next_card; } }

    public CardInfo()
    {

    }

    public CardInfo(CardDataEntry cardData)
    {
        this.cardData = cardData;
    }


}
