using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardDealerChoiceState {
    None = 0,
    Left = 1,
    Right = 2,
}

public enum CardDealerChoiceEvent {
    None = 0,
    ExecuteLeft = 1,
    ExecuteRight = 2,
}

public class CardDealer : MonoBehaviour {
    public CardDealerChoiceState choice;

    /// <summary>
    /// Min offset x required for input to trigger a choice
    /// </summary>
    public float choiceInputThreshold;

    /// <summary>
    /// Current input touchOffset
    /// </summary>
    public Vector2 touchOffset;

    public Dispatcher<CardDealerChoiceEvent> Dispatcher { get { return dispatcher; } }
    private Dispatcher<CardDealerChoiceEvent> dispatcher = new Dispatcher<CardDealerChoiceEvent>();
    private ICard card;


    /// <summary>
    /// Called when a new card is dispatched
    /// </summary>
    public void SetCard(ICard card) {
        this.card = card;
    }

    /// <summary>
    /// Called to update the touch position
    /// </summary>
    /// <param name="touchOffset"></param>
    public void SetInputPosition(Vector2 touchOffset) {
        this.touchOffset = touchOffset;

        if (touchOffset.x <= -choiceInputThreshold) {
            choice = CardDealerChoiceState.Left;
        } else if (touchOffset.x >= choiceInputThreshold) {
            choice = CardDealerChoiceState.Right;
        } else
            choice = CardDealerChoiceState.None;
    }

    /// <summary>
    /// Called when a touch is released or ended.
    /// </summary>
    public void OnTouchEnd() {
        switch (choice) {
            case CardDealerChoiceState.Left:
                ExecuteLeft();
                break;
            case CardDealerChoiceState.Right:
                ExecuteRight();
                break;
            default:
                break;
        }
    }

    private void ExecuteLeft() {
        card.OnLeftChoice();
        dispatcher.Dispatch(CardDealerChoiceEvent.ExecuteLeft);
    }

    private void ExecuteRight() {
        card.OnRightChoice();
        dispatcher.Dispatch(CardDealerChoiceEvent.ExecuteRight);
    }


}
