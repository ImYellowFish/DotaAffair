using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDispatcher : MonoBehaviour{
    public float dispatchDuration = 0.5f;
    public float flipDelay = 0.3f;



    private Table table;
    private Dispatcher<CardEvent> dispatcher { get { return table.dispatcher; } }
    private bool start_flip;
    private float timer;

    public void Init(Table table) {
        this.table = table;
    }

    public void DispatchRandom() {
        Debug.Log("Dispatch");
        dispatcher.Dispatch(CardEvent.DispatchStart);
        table.StartDispatch();
        table.card = new EmptyCard();
    }

    public void Dispatch(string cardID) {
        Debug.Log("Dispatch");
        table.StartDispatch();
        table.card = new EmptyCard();
        timer = dispatchDuration;
        start_flip = false;
    }


    void Update() {
        UpdateDispatchTimer();
    }

    private void UpdateDispatchTimer() {
        if (table.state == TableState.Dispatching) {
            timer -= Time.deltaTime;
            if (timer <= dispatchDuration - flipDelay && !start_flip) {
                dispatcher.Dispatch(CardEvent.DispatchFlipNewCard);
                start_flip = true;
            }

            if (timer <= 0)
                table.EndDispatch();
        }
    }
}
