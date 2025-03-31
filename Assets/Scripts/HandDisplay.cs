using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandDisplay : MonoBehaviour
{
    private GameSession _myGameSession;
    private TextMeshProUGUI _text;
    private int _count;

    void Start()
    {
        _myGameSession = GameSession.Instance;
        _count = _myGameSession.handsCount;
    }

    public void UpdateHandsCounter()
    {
        if (_text == null)
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        _count--;
        _text.text = _count.ToString();
    }

    public void SetHandsCounter()
    {
        if (_text == null)
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        if (_myGameSession == null)
        {
            _myGameSession = GameSession.Instance;
        }

        _count = _myGameSession.handsCount;
    }

    public bool IsEmpty()
    {
        return _count <= 0;
    }
}
