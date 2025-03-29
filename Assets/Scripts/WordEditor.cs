using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;

public class WordEditor : MonoBehaviour
{
    private GameSession myGameSession;
    private List<LetterDisplay> letters;
    private string currentWord;
    private string goalWord;

    private GameSession.CardType cardType;
    private int cardValue;

    void Start()
    {
        myGameSession = FindObjectOfType<GameSession>();
        letters = FindObjectsOfType<LetterDisplay>().ToList();
        (currentWord, goalWord) = myGameSession.GetNextWordsPair();
    }

    public char GetLettersChar(int letterID)
    {
        return currentWord[letterID];
    }

    public void ApplyCardOperation(int letterPosToChange)
    {
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
            case GameSession.CardType.Swap:
                break;
            case GameSession.CardType.Ceasar:
                Ceasar(cardValue);
                break;
            default:
                break;
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
