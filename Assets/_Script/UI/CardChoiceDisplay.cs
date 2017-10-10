using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChoiceDisplay : MonoBehaviour {
    public RectTransform card;
    public Vector2 inputMultiplier;
    public float smooth = 1f;

    public float maxAngle;
    public float maxCardPosX;
    public float maxCardPosY;
    public AnimationCurve curveOffsetX_Versus_CardAngle;   
    public AnimationCurve curveOffsetX_Versus_CardPosX;   
    public AnimationCurve curveOffsetY_Versus_CardPosY;

    public float dispatchingAngle;
    public float dispatchingPosX;
    public float dispatchingPosY;
    

    void Start() {
        table = Table.Instance;
        cardChoiceMaker = table.cardChoiceMaker;

        table.dispatcher.AddListener(CardEvent.DispatchEnd, OnDispatchEnd);
    }

    void OnDestroy() {
        table.dispatcher.RemoveListener(CardEvent.DispatchEnd, OnDispatchEnd);
    }


    void Update() {        
        UpdateDesiredPositionRotation();
        LerpCardPositionRotation();
    }

    private void UpdateDesiredPositionRotation() {

        // Update based on input in idle state
        if (table.state == TableState.Idle) {
            Vector2 input = cardChoiceMaker.touchOffset;
            desiredCardPos.x = maxCardPosX * AbsEvaluate(curveOffsetX_Versus_CardPosX, input.x * inputMultiplier.x);
            desiredCardPos.y = maxCardPosY * AbsEvaluate(curveOffsetY_Versus_CardPosY, input.y * inputMultiplier.y);
            desiredAngle = -maxAngle * AbsEvaluate(curveOffsetX_Versus_CardAngle, input.x * inputMultiplier.x);

        } else if (table.state == TableState.Dispatching) {
            // update based on left in idle state
            float sgn = 1;
            if (cardChoiceMaker.choice == CardChoiceState.Left) {
                sgn = -1;
            }
            desiredCardPos.x = sgn * maxCardPosX * dispatchingPosX;
            desiredCardPos.y = sgn * maxCardPosY * dispatchingPosY;
            desiredAngle = sgn * -dispatchingAngle;

        }     
    }

    // Move the card here
    private void LerpCardPositionRotation() {
        currentAngle = Mathf.Lerp(currentAngle, desiredAngle, smooth * Time.deltaTime);
        card.localRotation = Quaternion.Euler(0, 0, currentAngle);

        currentCardPos = Vector2.Lerp(currentCardPos, desiredCardPos, smooth * Time.deltaTime);
        card.anchoredPosition = currentCardPos;
    }


    /// <summary>
    /// Reset the card rectTransform when a new card is dispatched.
    /// </summary>
    private void OnDispatchEnd(params object[] data) {
        currentAngle = 0;
        card.localRotation = Quaternion.identity;
        currentCardPos = Vector2.zero;
        card.anchoredPosition = currentCardPos;
    }

    private float AbsEvaluate(AnimationCurve curve, float t)
    {
        return Mathf.Sign(t) * curve.Evaluate(Mathf.Abs(t));
    }



    private Table table;
    private CardChoiceMaker cardChoiceMaker;
    private Vector2 desiredCardPos;
    private float desiredAngle;

    private float currentAngle;
    private Vector2 currentCardPos;
}
