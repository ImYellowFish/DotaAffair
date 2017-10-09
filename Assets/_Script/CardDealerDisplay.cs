using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealerDisplay : MonoBehaviour {
    public AnimationCurve curveOffsetX_Versus_CardAngle;
    public AnimationCurve curveOffsetX_Versus_CardPosX;
    public AnimationCurve curveOffsetY_Versus_CardPosY;

    public void Init() {
        cardDealer = FindObjectOfType<CardDealer>();
    }


    void Start() {
        Init();
    }


    void Update() {
        UpdateDesiredPositionRotation();
        LerpCardPositionRotation();
    }

    private void UpdateDesiredPositionRotation() {
        Vector2 input = cardDealer.touchOffset;
        
        desiredCardPos.x = curveOffsetX_Versus_CardPosX.Evaluate(input.x);
        desiredCardPos.y = curveOffsetY_Versus_CardPosY.Evaluate(input.y);
        desiredAngle = curveOffsetX_Versus_CardAngle.Evaluate(input.x);
    }

    // Move the card here
    private void LerpCardPositionRotation() {

    }

    private CardDealer cardDealer;
    private Vector2 desiredCardPos;
    private float desiredAngle;
}
