using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : MonoBehaviour
{
    public char currentLetter;
    private TextMeshProUGUI text;

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (text != null)
        {
            text.text = currentLetter.ToString();
        }
    }

    public void UpdateLetter(char newLetter)
    {
        currentLetter = newLetter;
        UpdateText();
    }
}
