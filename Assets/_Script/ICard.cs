using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICard{
    /// <summary>
    /// Display info of the card
    /// </summary>
    CardInfo cardInfo { get; }

    /// <summary>
    /// Receives left choice event
    /// </summary>
    void OnLeftChoice();

    /// <summary>
    /// Receives right choice event
    /// </summary>
    void OnRightChoice();
}
