using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WordEditor : MonoBehaviour
{
    private GameSession myGameSession;
    private string currentWord;
    private string goalWord;

    private GameSession.CardType cardType = GameSession.CardType.Invalid;
    private int cardValue;

    private List<LetterDisplay> letters;

    private DeckHandlerer myDeckHandlerer;

    void Start()
    {
        myGameSession = FindObjectOfType<GameSession>();
        myDeckHandlerer = FindObjectOfType<DeckHandlerer>();
        letters = FindObjectsOfType<LetterDisplay>().ToList();
        (currentWord, goalWord) = myGameSession.GetNextWordsPair();
    }

    public char GetLettersChar(int letterPosID)
    {
        return currentWord[letterPosID];
    }

    public void SetClickedCardType(GameSession.CardType type)
    {
        cardType = type;
    }

    public void SetClickedCardValue(int value)
    {
        cardValue = value;
    }

    public void ApplyCardOperation(int letterPosToChange)
    {
        if (cardType == GameSession.CardType.Invalid)
        {
            // TODO tell player invalid card
            return;
        }

        switch (cardType)
        {
            case GameSession.CardType.SmallPlus:
            case GameSession.CardType.BigPlus:
            case GameSession.CardType.SmallMinus:
            case GameSession.CardType.BigMinus:
                Add(letterPosToChange, cardValue);
                break;
            case GameSession.CardType.Multiplication:
                Multiplication(letterPosToChange, cardValue);
                break;
            case GameSession.CardType.Division:
                Division(letterPosToChange, cardValue);
                break;
            case GameSession.CardType.Ceasar:
                Ceasar(cardValue);
                break;
            default:
                break;
        }

        UpdateTextOfAllLetters();

        // remove last clicked card
        List<GameObject> handCards = myDeckHandlerer.GetHandCards();
        handCards[myDeckHandlerer.GetLastCardID()].SetActive(false);

        cardType = GameSession.CardType.Invalid;
    }

    private void UpdateTextOfAllLetters()
    {
        foreach (var letter in letters)
        {
            letter.UpdateText();
        }
    }

    private void ApplyOperation(int letterPosToChange, Func<int, int, int> operation)
    {
        StringBuilder wordBuilder = new StringBuilder(currentWord);

        int currLetterIndex = wordBuilder[letterPosToChange] - 'a';

        int newLetterIndex = operation(currLetterIndex, cardValue);

        wordBuilder[letterPosToChange] = (char)(newLetterIndex + 'a');
        currentWord = wordBuilder.ToString();
    }

    private void Add(int letterPosToChange, int byHowMuch)
    {
        ApplyOperation(letterPosToChange, (currLetterIndex, cardValue) => (currLetterIndex + cardValue + 26) % 26);
    }

    private void Multiplication(int letterPosToChange, int byHowMuch)
    {
        ApplyOperation(letterPosToChange, (currLetterIndex, cardValue) => (currLetterIndex * cardValue + 26) % 26);
    }

    private void Division(int letterPosToChange, int byHowMuch)
    {
        if (byHowMuch == 0) 
            return;

        ApplyOperation(letterPosToChange, (currLetterIndex, cardValue) => (currLetterIndex / cardValue + 26) % 26);
    }

    private void Ceasar(int byHowMuch)
    {
        for (int i = 0; i < currentWord.Length; i++)
        {
            Add(i, byHowMuch);
        }
    }

    public bool IsSame()
    {
        return currentWord == goalWord;
    }
}
