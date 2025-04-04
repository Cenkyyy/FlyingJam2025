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
        SetWordLists();
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

    // Game stats
    public List<string> startingWordsCopy;
    public List<string> goalWordsCopy;

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

    public void SetPlayersDeck()
    {
        playerDeck = new List<GameSession.CardType>(startingDeck);
    }

    public void SetWordLists()
    {
        startingWordsCopy = new List<string>(startingWords);
        goalWordsCopy = new List<string>(goalWords);
    }

    public int GetWordsCount()
    {
        return startingWords.Count;
    }

    public (string, string) GetNextWordsPair()
    {
        int randomStartingWord = random.Next(startingWordsCopy.Count);
        int randomGoalWord = random.Next(goalWordsCopy.Count);

        string startingWord = startingWordsCopy[randomStartingWord];
        string goalWord = goalWordsCopy[randomGoalWord];

        startingWordsCopy.RemoveAt(randomStartingWord);
        goalWordsCopy.RemoveAt(randomGoalWord);

        return (startingWord, goalWord);
    }

    public void AddCard(CardType newType)
    {
        playerDeck.Add(newType);
    }
}
