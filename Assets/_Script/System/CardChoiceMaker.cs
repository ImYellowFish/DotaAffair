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
        CardChoiceState newChoice;
        
        // only update when table is in idle state
        if (table.state == TableState.Idle) {

            touchOffset = InputManager.Instance.touchOffset;
            touchOffsetPixels = InputManager.Instance.touchOffsetPixels;

            
            if (touchOffsetPixels.x <= -choiceInputThreshold)
            {
                newChoice = CardChoiceState.Left;
                
            }
            else if (touchOffsetPixels.x >= choiceInputThreshold)
            {
                newChoice = CardChoiceState.Right;
            }
            else
            {
                newChoice = CardChoiceState.None;
            }

        } else {
            newChoice = CardChoiceState.None;
            touchOffset = Vector2.zero;
            touchOffsetPixels = Vector2.zero;
        }

        CheckChoiceChange(newChoice);
    }


    private void CheckChoiceChange(CardChoiceState newChoice)
    {
        if (newChoice == choice)
            return;

        if(newChoice == CardChoiceState.Left)
        {
            dispatcher.Dispatch(CardEvent.Prepare, newChoice, choice);
            dispatcher.Dispatch(CardEvent.PrepareLeft, newChoice, choice);
        }
        else if(newChoice == CardChoiceState.Right)
        {
            dispatcher.Dispatch(CardEvent.Prepare, newChoice, choice);
            dispatcher.Dispatch(CardEvent.PrepareRight, newChoice, choice);
        }

        if(choice == CardChoiceState.Left)
        {
            dispatcher.Dispatch(CardEvent.ExitPrepare, newChoice, choice);
            dispatcher.Dispatch(CardEvent.ExitPrepareLeft, newChoice, choice);
        }
        else if(choice == CardChoiceState.Right)
        {
            dispatcher.Dispatch(CardEvent.ExitPrepare, newChoice, choice);
            dispatcher.Dispatch(CardEvent.ExitPrepareRight, newChoice, choice);
        }

        choice = newChoice;
    }

    /// <summary>
    /// Called when a touch is released or ended.
    /// </summary>
    private void OnTouchEnd(params object[] data) {
        if (table.state != TableState.Idle)
            return;

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
        dispatcher.Dispatch(CardEvent.ExecuteChoice, choice);
        dispatcher.Dispatch(CardEvent.ExecuteLeft);
        card.OnLeftChoice();

    }

    private void ExecuteRight() {
        dispatcher.Dispatch(CardEvent.ExecuteChoice, choice);
        dispatcher.Dispatch(CardEvent.ExecuteRight);
        card.OnRightChoice();

    }


}
