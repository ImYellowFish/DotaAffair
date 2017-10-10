using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardChoiceState {
    None = 0,
    Left = 1,
    Right = 2,
}

public class CardChoiceMaker : MonoBehaviour {
    
    /// <summary>
    /// Min offset x required for input to trigger a choice
    /// </summary>
    public float choiceInputThreshold;

    public CardChoiceState choice;
    public Vector2 touchOffsetPixels;
    public Vector2 touchOffset;


    public Dispatcher<CardEvent> dispatcher { get { return table.dispatcher; } }
    private ICard card { get { return table.card; } }

    private Table table;

    public void Init(Table table) {
        this.table = table;
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
        // only update when table is in idle state
        if (table.state == TableState.Idle) {

            touchOffset = InputManager.Instance.touchOffset;
            touchOffsetPixels = InputManager.Instance.touchOffsetPixels;

            if (touchOffsetPixels.x <= -choiceInputThreshold) {
                choice = CardChoiceState.Left;
            } else if (touchOffsetPixels.x >= choiceInputThreshold) {
                choice = CardChoiceState.Right;
            } else
                choice = CardChoiceState.None;
        }
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
        dispatcher.Dispatch(CardEvent.ExecuteChoice);
        dispatcher.Dispatch(CardEvent.ExecuteLeft);
        card.OnLeftChoice();

    }

    private void ExecuteRight() {
        dispatcher.Dispatch(CardEvent.ExecuteChoice);
        dispatcher.Dispatch(CardEvent.ExecuteRight);
        card.OnRightChoice();

    }


}
