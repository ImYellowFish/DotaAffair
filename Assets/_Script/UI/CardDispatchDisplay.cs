using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDispatchDisplay : MonoBehaviour {
    public Image card;
    public Image newCard;
    public Sprite cardBackground;

    public Sprite cardForeground {
        get {
            return Table.Instance.card.cardInfo.sprite;
        }
    }

    public string flipAnimName;
    
    /// <summary>
    /// Time required to flip the card by 90 degrees
    /// </summary>
    public float flipHalfDuration;
    public float maxAngleX;

    private Table table;
    private Animator anim;
    private enum CardFlipState { NotActive = 0, FlipBack = 1, FlipFront = 2}
    private CardFlipState state;

    private float timer;

    void Start() {
        table = Table.Instance;
        table.dispatcher.AddListener(CardEvent.DispatchStart, OnDispatchStart);
        table.dispatcher.AddListener(CardEvent.DispatchFlipNewCard, OnDispatchFlipNewCard);
        table.dispatcher.AddListener(CardEvent.DispatchEnd, OnDispatchEnd);

        OnDispatchEnd();

        anim = GetComponent<Animator>();
    }

    void OnDestroy() {
        table.dispatcher.RemoveListener(CardEvent.DispatchStart, OnDispatchStart);
        table.dispatcher.RemoveListener(CardEvent.DispatchFlipNewCard, OnDispatchFlipNewCard);
        table.dispatcher.RemoveListener(CardEvent.DispatchEnd, OnDispatchEnd);
    }

    private void OnDispatchStart(params object[] data) {
        newCard.sprite = cardBackground;
    }

    private void OnDispatchFlipNewCard(params object[] data) {
        EnterFlipBackState();
    }

    private void OnDispatchEnd(params object[] data) {
        if (state != CardFlipState.NotActive)
            ExitFlipState();

        // hide new card and show main card
        newCard.sprite = cardBackground;
        card.enabled = true;
    }

    void Update() {
        if (table.state != TableState.Dispatching || state == CardFlipState.NotActive)
            return;

        timer += Time.deltaTime;

        //if(state == CardFlipState.FlipBack) {
        //    UpdateFlipBackState();
        //}else if(state == CardFlipState.FlipFront) {
        //    UpdateFlipFrontState();
        //}        

    }

    private void EnterFlipBackState() {
        state = CardFlipState.FlipBack;
        card.enabled = false;
        timer = 0;
        anim.Play(flipAnimName);
    }

    private void EnterFlipFrontState() {
        state = CardFlipState.FlipFront;
        newCard.sprite = cardForeground;
        card.sprite = cardForeground;
    }

    private void ExitFlipState() {
        state = CardFlipState.NotActive;
    }

    //private void UpdateFlipBackState() {
    //    if(timer >= flipHalfDuration) {
    //        EnterFlipFrontState();
    //        return;
    //    }
    //}

    //private void UpdateFlipFrontState() {
    //    float frontPhaseTimer = timer - flipHalfDuration;
    //    if(frontPhaseTimer >= flipHalfDuration) {
    //        ExitFlipState();
    //        return;
    //    }
    //}

    #region Animation Event
    public void AnimHalfFlip() {
        EnterFlipFrontState();
    }

    public void AnimFlipOver() {
        ExitFlipState();
    }

    #endregion

    //private void UpdateNewCardRotation(float t) {
    //    float angleX = maxAngleX;
    //    float angleY = t * 90f;
    //    newCard.rectTransform.localRotation = Quaternion.Euler(angleX, angleY, 0);
    //}
}
