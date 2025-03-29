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

    private Deck deck;
    private StockPile stockPile;
    private Hand hand;

    void Start()
    {
        deck = FindObjectOfType<Deck>();
        stockPile = FindObjectOfType<StockPile>();
        hand = FindObjectOfType<Hand>();

        deck.InitializeCard();
        DealInitialCards();
        SetStartWord(startWord);
    }

    public void DealInitialCards()
    {
        for (int i = 0; i < hand.handSize; i++)
        {
            hand.AddCard(deck.DrawCard());
        }
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

    public void CardClicked(Card card)
    {
        selectedCard = card;
        Debug.Log("Card clicked!");
    }

    void UpdateStartWord()
    {
        startWord = "";
        foreach (var letter in letters)
        {
            startWord += letter.currentLetter;
        }
    }

    bool CheckWin()
    {
        return startWord == goalWord;
    }

    public void ApplyCard(int letterIndex)
    {
        if (selectedCard == null || letterIndex < 0 || letterIndex >= letters.Count)
            return;

        switch (selectedCard.Type)
        {
            case CardType.Addition:
                letters[letterIndex].UpdateLetter(ShiftLetter(letters[letterIndex].currentLetter, selectedRange));
                break;
            case CardType.Subtraction:
                letters[letterIndex].UpdateLetter(ShiftLetter(letters[letterIndex].currentLetter, -selectedRange));
                break;
            case CardType.Multiplication:
                letters[letterIndex].UpdateLetter(Multiply(letterIndex, selectedRange));
                break;
            case CardType.Swap:
                Swap();
                break;
            case CardType.Ceasar:
                
                break;
        }

        UpdateStartWord();

        if (CheckWin())
        {
            Debug.Log("You Win!");
        }

        hand.RemoveCard(selectedCard);
        stockPile.AddCard(selectedCard);
        selectedCard = null;
    }

    private char ShiftLetter(char letterPosition, int shiftAmount)
    {
        int letterIndex = letterPosition - 'A';
        int newIndex = (letterIndex + shiftAmount) % 26;

        return (char)('A' + newIndex);
    }

    private char Multiply(int letterPosition, int shiftAmount)
    {
        int letterIndex = letters[letterPosition].currentLetter - 'A';
        int newIndex = (letterIndex * shiftAmount) % 26;

        return (char)('A' + newIndex);

    }

    public void Swap()
    {

    }
}
