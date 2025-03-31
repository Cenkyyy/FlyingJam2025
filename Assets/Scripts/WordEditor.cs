using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WordEditor : MonoBehaviour
{
    private DeckHandlerer _myDeckHandlerer;
    private HandDisplay _myHandDisplay;
    private GoalWordDisplay _myGoalWordDisplay;
    private GameSession _myGameSession;
    private SceneLoader _mySceneLoader;

    // Words data
    private List<LetterDisplay> _letters;
    private string _currentWord;
    private string _goalWord;

    // Last card data
    private int _cardValue;
    private GameSession.CardType _cardType = GameSession.CardType.Invalid;

    void Start()
    {
        _myGameSession = GameSession.Instance;

        _mySceneLoader = FindObjectOfType<SceneLoader>();
        _myHandDisplay = FindObjectOfType<HandDisplay>();
        _myGoalWordDisplay = FindObjectOfType<GoalWordDisplay>();
        _myDeckHandlerer = FindObjectOfType<DeckHandlerer>();
        _letters = FindObjectsOfType<LetterDisplay>().ToList();

        if (_myGameSession.currentLevel == 1)
        {
            _currentWord = "hero";
            _goalWord = "zero";
        }
        else
        {
            (_currentWord, _goalWord) = _myGameSession.GetNextWordsPair();
        }

        UpdateTextOfAllLetters();
        _myGoalWordDisplay.UpdateText(_goalWord);
    }

    // Returns char from current word at given position
    public char GetLettersChar(int letterPosID)
    {
        return _currentWord[letterPosID];
    }

    // Updates each letter's text in game
    private void UpdateTextOfAllLetters()
    {
        foreach (var letter in _letters)
        {
            letter.UpdateText();
        }
    }

    // Sets last clicked card
    public void SetClickedCardType(GameSession.CardType type)
    {
        _cardType = type;
    }

    // Sets last clicked card's value
    public void SetClickedCardValue(int value)
    {
        _cardValue = value;
    }

    // Removes last clicked card when a card operation is applied on letter
    private void RemoveLastClickedHandCard()
    {
        GameObject handCardToRemove = _myDeckHandlerer.GetLastClickedCard();
        handCardToRemove.SetActive(false);
    }

    // Applies operation from last clicked card and last clicked card value
    // on clicked letter based on its position
    public void OnClickedLetter(int letterPosToChange)
    {
        if (_cardType == GameSession.CardType.Invalid)
        {
            // TODO tell player invalid card
            return;
        }

        switch (_cardType)
        {
            case GameSession.CardType.SmallPlus:
            case GameSession.CardType.BigPlus:
            case GameSession.CardType.SmallMinus:
            case GameSession.CardType.BigMinus:
                Add(letterPosToChange, _cardValue);
                break;
            case GameSession.CardType.Multiplication:
                Multiplication(letterPosToChange, _cardValue);
                break;
            case GameSession.CardType.Division:
                Division(letterPosToChange, _cardValue);
                break;
            case GameSession.CardType.Ceasar:
                Ceasar(_cardValue);
                break;
            default:
                break;
        }

        // update game state
        UpdateTextOfAllLetters();
        RemoveLastClickedHandCard();
        CheckForWin();
        CheckForEmptyHand();

        // set last clicked hand card to invalid after applying the operation
        _cardType = GameSession.CardType.Invalid;
    }

    // Updates current word after applying given operation
    private void ApplyOperation(int letterPosToChange, Func<int, int, int> operation)
    {
        StringBuilder wordBuilder = new StringBuilder(_currentWord);

        int currLetterIndex = wordBuilder[letterPosToChange] - 'a' + 1;

        int newLetterIndex = operation(currLetterIndex, _cardValue);

        newLetterIndex = (((newLetterIndex - 1) % 26) + 26) % 26 + 1;

        wordBuilder[letterPosToChange] = (char)(newLetterIndex - 1 + 'a');
        _currentWord = wordBuilder.ToString();
    }

    private void Add(int letterPosToChange, int byHowMuch)
    {
        ApplyOperation(letterPosToChange, (currLetterIndex, cardValue) => currLetterIndex + cardValue);
    }

    private void Multiplication(int letterPosToChange, int byHowMuch)
    {
        ApplyOperation(letterPosToChange, (currLetterIndex, cardValue) => currLetterIndex * cardValue);
    }

    private void Division(int letterPosToChange, int byHowMuch)
    {
        if (byHowMuch == 0) 
            return;

        ApplyOperation(letterPosToChange, (currLetterIndex, cardValue) => currLetterIndex / cardValue);
    }

    private void Ceasar(int byHowMuch)
    {
        for (int i = 0; i < _currentWord.Length; i++)
        {
            Add(i, byHowMuch);
        }
    }

    private void CheckForWin()
    {
        if (HasWon())
        {
            if (_myGameSession.GetWordsCount() == 0) // All of the words have been solved
            {
                _mySceneLoader.LoadWinScreen();
            }
            else // Round was won, load the shop scene
            {
                _myGameSession.currentLevel += 1;
                _mySceneLoader.LoadNextScene();
            }
        }
    }

    private bool HasWon()
    {
        return _currentWord == _goalWord;
    }

    private void CheckForEmptyHand()
    {
        if (_myDeckHandlerer.IsHandEmpty())
        {
            if (_myGameSession.handsCount <= 0) // No more hands available, meaning game lost
            {
                _mySceneLoader.LoadLoseScreen();
            }
            else // Load new hand
            {
                _myDeckHandlerer.GetNewHand();
                _myHandDisplay.UpdateHandsCounter();
            }
        }
    }
}
