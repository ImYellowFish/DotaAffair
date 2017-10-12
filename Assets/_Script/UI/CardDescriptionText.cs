using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDescriptionText : MonoBehaviour {
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        text.text = "";

        var dispatcher = Table.Instance.dispatcher;
        dispatcher.AddListener(CardEvent.DispatchStart, OnDispatchStart);
        dispatcher.AddListener(CardEvent.DispatchEnd, OnDispatchEnd);

    }

    private void OnDestroy()
    {
        var dispatcher = Table.Instance.dispatcher;
        dispatcher.RemoveListener(CardEvent.DispatchStart, OnDispatchStart);
        dispatcher.RemoveListener(CardEvent.DispatchEnd, OnDispatchEnd);
    }

    private void OnDispatchStart(params object[] data)
    {

    }

    private void OnDispatchEnd(params object[] data)
    {
        UpdateDescription(Table.Instance.card);
    }

    public void UpdateDescription(ICard card)
    {
        text.text = card.cardInfo.description;
    }
}
