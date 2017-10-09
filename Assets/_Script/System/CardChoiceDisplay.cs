using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChoiceDisplay : MonoBehaviour {
    public Vector2 inputMultiplier;
    public float maxAngle;
    public float maxCardPosX;
    public float maxCardPosY;
    public AnimationCurve curveOffsetX_Versus_CardAngle;   
    public AnimationCurve curveOffsetX_Versus_CardPosX;   
    public AnimationCurve curveOffsetY_Versus_CardPosY;
    
    public RectTransform card;
    public float smooth = 1f;
    
    public void Init() {
        cardChoiceMaker = FindObjectOfType<CardChoiceMaker>();
    }


    void Start() {
        Init();
    }


    void Update() {
        UpdateDesiredPositionRotation();
        LerpCardPositionRotation();
    }

    private void UpdateDesiredPositionRotation() {
        Vector2 input = cardChoiceMaker.touchOffset;
        
        desiredCardPos.x = maxCardPosX * AbsEvaluate(curveOffsetX_Versus_CardPosX, input.x * inputMultiplier.x);
        desiredCardPos.y = maxCardPosY * AbsEvaluate(curveOffsetY_Versus_CardPosY, input.y * inputMultiplier.y);
        desiredAngle = -maxAngle * AbsEvaluate(curveOffsetX_Versus_CardAngle, input.x * inputMultiplier.x);
    }

    // Move the card here
    private void LerpCardPositionRotation() {
        currentAngle = Mathf.Lerp(currentAngle, desiredAngle, smooth * Time.deltaTime);
        card.localRotation = Quaternion.Euler(0, 0, currentAngle);

        currentCardPos = Vector2.Lerp(currentCardPos, desiredCardPos, smooth * Time.deltaTime);
        card.anchoredPosition = currentCardPos;
    }

    private float AbsEvaluate(AnimationCurve curve, float t)
    {
        return Mathf.Sign(t) * curve.Evaluate(Mathf.Abs(t));
    }

    private CardChoiceMaker cardChoiceMaker;
    private Vector2 desiredCardPos;
    private float desiredAngle;

    private float currentAngle;
    private Vector2 currentCardPos;
}
