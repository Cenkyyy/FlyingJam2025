using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Addition,
    Subtraction,
    Multiplication,
    Division,
    Swap,
    Ceasar
}

public class Card : MonoBehaviour
{
    public CardType Type;
    public int lowerBound = 1;
    public int upperBound = 1;

    public void OnClick()
    {

    }
}
