using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandlerer : MonoBehaviour
{
    private GameSession gameSession;
    private List<GameSession.CardType> deck;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    public GameSession.CardType GetCardsType(int id)
    {
        return deck[id];
    }

    public int GetCardsValue(int id)
    {
        return 0;
    }

    public void OnHandCardClicked(Card card)
    {
        // Display overlay with all possible cards in the middle of the Screen
    }
}
