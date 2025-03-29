using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameSession : MonoBehaviour
{   
    public static GameSession Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        SetPlayersDeck();
        DontDestroyOnLoad(gameObject);
    }

    public enum CardType
    {
        SmallPlus,
        BigPlus,
        SmallMinus,
        BigMinus,
        Multiplication,
        Division,
        Swap,
        Ceasar
    }

    [SerializeField] List<CardType> startingDeck;

    // Player's stats
    public int currentLevel;
    public List<CardType> playerDeck;

    // Upgrades
    public int handSize = 3;
    public int handsCount = 5;
    public int shopCardCount = 3;
    public int smallSignUpperBound = 3;
    public int bigSignUpperBound = 8;
    public int multiplicationUpperBound = 3;
    public int divisionUpperBound = 3;

    void SetPlayersDeck()
    {
        playerDeck = startingDeck;
    }
}
