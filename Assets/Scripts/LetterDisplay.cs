using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterDisplay : MonoBehaviour
{
    [SerializeField] int positionID;
    
    private char currentLetter;
    private TextMeshProUGUI text;

    private WordEditor myEditor;

    private void Start()
    {
        myEditor = FindObjectOfType<WordEditor>();
    }

    public void UpdateText()
    {
        currentLetter = myEditor.GetLettersChar(positionID);
        text.text = currentLetter.ToString();
    }

    public int GetLetterIndex()
    {
        return currentLetter - 'a' + 1;
    }
}
