using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] List<Letter> letters = new();
    [SerializeField] string goalWord;

    private string GetCurrWord()
    {
        string currWord = "";
        for (int i = 0; i < letters.Count; i++)
        {
            currWord += letters[i].GetLetter();
        }
        return currWord;
    }

    bool Compare()
    {
        string currWord = GetCurrWord();
        
        if (currWord == goalWord)
        {
            return true;
        }
        return false;
    }
}
