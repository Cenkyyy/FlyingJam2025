using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockPile : MonoBehaviour
{
    List<Card> stockPile = new();

    public void AddCard(Card card)
    {
        stockPile.Add(card);
    }

    public List<Card> Recycle()
    {
        List<Card> tmp = new();
        foreach(var card in stockPile)
        {
            tmp.Add(card);
        }
        stockPile.Clear();
        return tmp;
    }
}
