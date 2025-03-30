using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * When a hand card is clicked, overlay is shown
 * When a overlay card is clicked, overlay is disables,
 * Letter is clicked, word is updated and used hand card is removed
 */

public class Card : MonoBehaviour
{
    [SerializeField] int positionID;

    private GameSession.CardType type;
    private int value;

    private DeckHandlerer myDeckHandlerer;

    void Start()
    {
        myDeckHandlerer = FindObjectOfType<DeckHandlerer>();
    }

    public void UpdateType()
    {
        type = myDeckHandlerer.GetCardsType(positionID);
    }

    public void UpdateValue()
    {
        value = myDeckHandlerer.GetCardsValue(positionID);
    }
}
