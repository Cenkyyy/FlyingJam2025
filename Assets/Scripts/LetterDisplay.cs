using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterDisplay : MonoBehaviour
{
    [SerializeField] int positionID;

    private WordEditor _myEditor;

    private char _currentLetter;
    private TextMeshProUGUI _text;

    public void UpdateText()
    {
        if (_text == null)
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        if (_myEditor == null)
        {
            _myEditor = FindObjectOfType<WordEditor>();
        }
        
        _currentLetter = _myEditor.GetLettersChar(positionID);
        _text.text = _currentLetter.ToString();
    }
}
