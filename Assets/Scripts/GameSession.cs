using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] string goalWord;
    [SerializeField] string startWord;

    [SerializeField] List<Letter> letters = new();

    private Card selectedCard;
    private int selectedRange;

    void Start()
    {
        SetStartWord(startWord);    
    }

    public void SetStartWord(string newWord)
    {
        startWord = newWord;

        for (int i = 0; i < letters.Count; i++)
        {
            letters[i].UpdateLetter(newWord[i]);
        }
    }

    public void SelectRange(int value)
    {
        if (selectedCard != null && value >= selectedCard.lowerBound && value <= selectedCard.upperBound)
        {
            selectedRange = value;
        }
    }

    public void SelectCard(Card card)
    {
        selectedCard = card;
    }

    void UpdateStartWord()
    {
        string currentWord = "";
        foreach (var letter in letters)
        {
            currentWord += letter.currentLetter;
        }
        startWord = currentWord;
    }

    bool CheckWin()
    {
        if (startWord == goalWord)
        {
            return true;
        }
        return false;
    }

    public void ApplyCard(Card card)
    {
        switch (card.Type)
        {
            case CardType.Addition:
                break;
            case CardType.Subtraction:
                break;
            case CardType.Multiplication:
                break;
            case CardType.Division:
                break;
            case CardType.Swap:
                break;
            case CardType.Ceasar:
                break;
        }
    }

    char ShiftLetter(char letter, int shiftAmount)
    {
        int letterIndex = letter - 'A';
        int newIndex = (letterIndex + shiftAmount) % 26;

        return (char)('A' + newIndex);
    }

    public void Multiply(int letterPosition, int byHowMuch)
    {
        
    }
}
