using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandDisplay : MonoBehaviour
{
    private GameSession _gameSession;
    private TextMeshProUGUI _text;
    private int _count;

    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _gameSession = GameSession.Instance;
        _count = _gameSession.handsCount;
    }

    public void UpdateHandsCounter()
    {
        _count--;
        _text.text = _count.ToString();
    }
}
