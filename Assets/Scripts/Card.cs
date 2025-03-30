using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        UpdateSprite();
    }

    public void UpdateValue()
    {
        value = myDeckHandlerer.GetCardsValue(positionID);
    }

    private void UpdateSprite()
    {
        // alternaltive
        //myDeckHandlerer.GetSprite()
        switch (type)
        {
            // display different sprites
        }
    }
}
