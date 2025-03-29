using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> deck = new();

    public void InitializeCard()
    {
        deck.Add(new Card { Type = CardType.Addition });
        deck.Add(new Card { Type = CardType.Addition });
        deck.Add(new Card { Type = CardType.Multiplication });
        deck.Add(new Card { Type = CardType.Multiplication });

        Shuffle();
    }

    private void Shuffle()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = Random.Range(0, deck.Count);

            Card tmp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = tmp;
        }
    }

    public void AddCard(Card card)
    {
        deck.Add(card);
    }

    public Card DrawCard()
    {
        Card drawnCard = deck[0];
        deck.RemoveAt(0);
        return drawnCard;
    }

    public bool IsEmpty()
    {
        return deck.Count == 0;
    }

    public void AddCards(List<Card> recycledCards)
    {
        foreach (var recycledCard in recycledCards)
        {
            deck.Add(recycledCard);
        }

        Shuffle();
    }
}
