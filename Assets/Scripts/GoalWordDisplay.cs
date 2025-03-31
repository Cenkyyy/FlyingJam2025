using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalWordDisplay : MonoBehaviour
{
    private TextMeshProUGUI _text;

    public void UpdateText(string newText)
    {
        if (_text == null)
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
        _text.text = newText;
    }
}
