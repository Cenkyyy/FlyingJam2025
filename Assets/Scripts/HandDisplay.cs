using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandDisplay : MonoBehaviour
{
    private int count;

    private GameSession gameSession;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
        count = gameSession.handsCount;
    }

    public void UpdateHandsCounter()
    {
        count--;
        text.text = count.ToString();
    }
}
