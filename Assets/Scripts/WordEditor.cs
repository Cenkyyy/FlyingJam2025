using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordEditor : MonoBehaviour
{
    [SerializeField] string goalWord;
    [SerializeField] string startWord;
    [SerializeField] List<LetterDisplay> letters;

    private string currentWord;

    void Start()
    {
        letters = FindObjectsOfType<LetterDisplay>().ToList();

        currentWord = startWord;
    }

    public char GetLettersChar(int letterID)
    {
        return currentWord[letterID];
    }
}
