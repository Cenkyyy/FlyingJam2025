using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> deck = new();

    void Start()
    {
        
    }

    private void InitializeCard()
    {

    }

    private void ShuffleCard()
    {
        
    }

    public void AddCard(Card card)
    {
        deck.Add(card);
    }
}
