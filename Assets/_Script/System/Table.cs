using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum CardEvent {
    None = 0,

    ExecuteChoice = 100,
    ExecuteLeft = 110,
    ExecuteRight = 120,

    DispatchStart = 200,
    DispatchFlipNewCard = 210,
    DispatchEnd = 220,
}


public enum TableState {
    Idle = 0,
    Dispatching = 10,
}


/// <summary>
/// Contains current card.
/// Controls the game flow
/// </summary>
public class Table : MonoBehaviour {
    public static Table Instance { get; private set; }
    public CardChoiceMaker cardChoiceMaker { get; private set; }
    public CardDispatcher cardDispatcher { get; private set; }
    public Dispatcher<CardEvent> dispatcher;

    public string currentCardID;
    public ICard card {
        get { return m_card; }
        set {
            m_card = value;
            currentCardID = card.cardInfo.ID;
        }
    }

    public TableState state;

    public void StartDispatch() {
        dispatcher.Dispatch(CardEvent.DispatchStart);
        state = TableState.Dispatching;
    }

    public void EndDispatch() {
        if (state == TableState.Dispatching) {
            dispatcher.Dispatch(CardEvent.DispatchEnd);
            state = TableState.Idle;

        }
    }

    private ICard m_card;
    
    void Awake() {
        if(Instance != null) {
            Destroy(gameObject);
            return;
        }

        // TODO: let gamemanager keeps the reference
        Instance = this;
        DontDestroyOnLoad(gameObject);

        dispatcher = new Dispatcher<CardEvent>();
    }

    void Start() {
        card = new EmptyCard();

        cardChoiceMaker = GetComponent<CardChoiceMaker>();
        cardChoiceMaker.Init(this);

        cardDispatcher = GetComponent<CardDispatcher>();
        cardDispatcher.Init(this);
        
    }


}
