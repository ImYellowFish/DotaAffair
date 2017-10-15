using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceTipDisplay : MonoBehaviour {

    public Text _ChoiceTip;
    public GameObject _Card;
    private Animator  _animator;
    public GameObject _ChoiceBG;
    public AnimationCurve _animationCurve;

    public float     _maxXoffset;
    private float     _maxAngle; 
    private bool      _isMaskDown;
    public bool IsTipShowing { get { return _isMaskDown; }}

    public enum MoveDirect
    {
        MoveDirect_Down = 0,
        MoveDirect_Up,
    };

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
        Table.Instance.dispatcher.AddListener(CardEvent.PrepareLeft, HandlerEnterLeftChoice);
        Table.Instance.dispatcher.AddListener(CardEvent.PrepareRight, HandlerEnterRightChoice);
        Table.Instance.dispatcher.AddListener(CardEvent.ExitPrepareLeft, HandlerExitLeftChoice);
        Table.Instance.dispatcher.AddListener(CardEvent.ExitPrepareRight, HandlerExitRightChoice);
        

        _maxAngle = GetComponent<CardChoiceDisplay>().maxAngle;
    }
	
	// Update is called once per frame
	void Update () {
		if(_isMaskDown)
        {
            SetMaskRotation(-_Card.transform.localEulerAngles.z);
        }
	}

    public void SetMaskRotation(float angle)
    {
        if (angle < -180)
            angle += 360;

        _ChoiceBG.transform.localRotation = Quaternion.Euler(0, 0, angle);
        SetLocalPosX(_animationCurve.Evaluate(angle / _maxAngle) * _maxXoffset);
     //   Debug.Log("angle = " + angle);
    }

    private void HandlerEnterLeftChoice(params object[] data)
    {
        ShowChoiceTip(true, Table.Instance.card.cardInfo.leftText);
    }

    private void HandlerEnterRightChoice(params object[] data)
    {
        ShowChoiceTip(true, Table.Instance.card.cardInfo.rightText);
    }

    private void HandlerExitRightChoice(params object[] data)
    {
        ShowChoiceTip(false, Table.Instance.card.cardInfo.rightText);
    }

    private void HandlerExitLeftChoice(params object[] data)
    {
        ShowChoiceTip(false, Table.Instance.card.cardInfo.leftText);
    }

    private void OnDestroy()
    {
        Table.Instance.dispatcher.RemoveListener(CardEvent.PrepareLeft, HandlerEnterLeftChoice);
        Table.Instance.dispatcher.RemoveListener(CardEvent.PrepareRight, HandlerEnterRightChoice);
        Table.Instance.dispatcher.RemoveListener(CardEvent.ExitPrepareLeft, HandlerExitLeftChoice);
        Table.Instance.dispatcher.RemoveListener(CardEvent.ExitPrepareRight, HandlerExitRightChoice);
    }

    public void ShowChoiceTip(bool isShowChoiceTip, string cardInfo)
    {
        BeginMoveAnimation(isShowChoiceTip ? MoveDirect.MoveDirect_Down : MoveDirect.MoveDirect_Up);
        _ChoiceTip.text = cardInfo;
    }

    private void BeginMoveAnimation(MoveDirect moveDirect)
    {
        switch(moveDirect)
        {
            case MoveDirect.MoveDirect_Down:
                if (_isMaskDown)
                    return;
                _animator.Play("ChoiceMask_Down");
                _isMaskDown = true;
                break;
            case MoveDirect.MoveDirect_Up:
                if (!_isMaskDown)
                    return;
                _animator.Play("ChoiceMask_Up");
                _isMaskDown = false;
                break;
            default:
                break;
        }
    }

    private void SetLocalPosX(float posX)
    {
        Vector3 oldPos = _ChoiceTip.transform.localPosition;
        _ChoiceTip.transform.localPosition = new Vector3(posX, oldPos.y, oldPos.z);

      //  Debug.Log("posX = " + posX);
    }
}
