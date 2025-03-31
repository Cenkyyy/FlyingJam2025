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

    private void Start()
    {
        _myEditor = FindObjectOfType<WordEditor>();
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText()
    {
        _currentLetter = _myEditor.GetLettersChar(positionID);
        _text.text = _currentLetter.ToString();
    }
}
