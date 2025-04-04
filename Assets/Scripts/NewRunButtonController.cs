using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRunButtonController : MonoBehaviour
{
    private GameSession _myGameSession;

    void Start()
    {
        _myGameSession = GameSession.Instance;    
    }

    public void ResetRun()
    {
        Debug.Log("im being resetted");

        // reset game data
        _myGameSession.currentLevel = 1;
        _myGameSession.SetPlayersDeck();
        _myGameSession.SetWordLists();

        // reset upgrades
        _myGameSession.handSize = 3;
        _myGameSession.handsCount = 5;
        _myGameSession.shopCardCount = 3;
        _myGameSession.smallSignUpperBound = 3;
        _myGameSession.bigSignUpperBound = 8;
        _myGameSession.multiplicationUpperBound = 3;
        _myGameSession.divisionUpperBound = 3;
    }
}
