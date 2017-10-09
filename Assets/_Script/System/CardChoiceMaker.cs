using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardChoiceState {
    None = 0,
    Left = 1,
    Right = 2,
}

public enum CardChoiceMakerEvent {
    None = 0,
    ExecuteLeft = 1,
    ExecuteRight = 2,
}

public class CardChoiceMaker : MonoBehaviour {
    public CardChoiceState choice;

    /// <summary>
    /// Min offset x required for input to trigger a choice
    /// </summary>
    public float choiceInputThreshold;

    public Dispatcher<CardChoiceMakerEvent> Dispatcher { get { return dispatcher; } }
    private Dispatcher<CardChoiceMakerEvent> dispatcher = new Dispatcher<CardChoiceMakerEvent>();
    private ICard card = new EmptyCard();

    public Vector2 touchOffsetPixels;
    public Vector2 touchOffset;

    /// <summary>
    /// Called when a new card is dispatched
    /// </summary>
    public void SetCard(ICard card) {
        this.card = card;
    }



    private void Start()
    {
        InputManager.Instance.dispatcher.AddListener(InputEvent.TouchRelease, OnTouchEnd);
    }

    private void OnDestroy()
    {
        InputManager.Instance.dispatcher.RemoveListener(InputEvent.TouchRelease, OnTouchEnd);
    }


    private void Update()
    {
        UpdateTouchOffset();
    }



    /// <summary>
    /// Called to update the touch position
    /// </summary>
    /// <param name="touchOffsetPixels"></param>
    private void UpdateTouchOffset() {
        touchOffset = InputManager.Instance.touchOffset;
        touchOffsetPixels = InputManager.Instance.touchOffsetPixels;
          
        if (touchOffsetPixels.x <= -choiceInputThreshold) {
            choice = CardChoiceState.Left;
        } else if (touchOffsetPixels.x >= choiceInputThreshold) {
            choice = CardChoiceState.Right;
        } else
            choice = CardChoiceState.None;
    }

    /// <summary>
    /// Called when a touch is released or ended.
    /// </summary>
    private void OnTouchEnd(params object[] data) {
        switch (choice) {
            case CardChoiceState.Left:
                ExecuteLeft();
                break;
            case CardChoiceState.Right:
                ExecuteRight();
                break;
            default:
                break;
        }
    }

    private void ExecuteLeft() {
        card.OnLeftChoice();
        dispatcher.Dispatch(CardChoiceMakerEvent.ExecuteLeft);
    }

    private void ExecuteRight() {
        card.OnRightChoice();
        dispatcher.Dispatch(CardChoiceMakerEvent.ExecuteRight);
    }


}
