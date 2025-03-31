using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private static GameSession _instance;

    public static GameSession Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameSession>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameSession");
                    _instance = singletonObject.AddComponent<GameSession>();
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        SetPlayersDeck();
    }

    public enum CardType
    {
        SmallPlus,
        BigPlus,
        SmallMinus,
        BigMinus,
        Multiplication,
        Division,
        Ceasar,
        Invalid
    }

    [SerializeField] List<string> startingWords;
    [SerializeField] List<string> goalWords;
    [SerializeField] List<CardType> startingDeck;

    // Player's stats
    public int currentLevel = 1;
    public List<CardType> playerDeck;

    // Upgrades
    public int handSize = 3;
    public int handsCount = 5;
    public int shopCardCount = 3;
    public int smallSignUpperBound = 3;
    public int bigSignUpperBound = 8;
    public int multiplicationUpperBound = 3;
    public int divisionUpperBound = 3;

    System.Random random = new System.Random();

    void SetPlayersDeck()
    {
        playerDeck = new List<GameSession.CardType>(startingDeck);
    }

    public int GetWordsCount()
    {
        return startingWords.Count;
    }

    public (string, string) GetNextWordsPair()
    {
        int randomIndex = random.Next(startingWords.Count);

        string startingWord = startingWords[randomIndex];
        string goalWord = goalWords[randomIndex];

        startingWords.RemoveAt(randomIndex);
        goalWords.RemoveAt(randomIndex);

        return (startingWord, goalWord);
    }

    public void AddCard(CardType newType)
    {
        playerDeck.Add(newType);
    }
}
