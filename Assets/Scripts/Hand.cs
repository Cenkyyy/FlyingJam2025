using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<Card> hand = new();
    public int handSize = 4;

    public void AddCard(Card card)
    {
        hand.Add(card);
    }

    public void RemoveCard(Card card)
    {
        hand.Remove(card);
    }
}
