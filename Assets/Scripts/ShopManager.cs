using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] List<GameObject> cardSlots = new List<GameObject>();
    [SerializeField] List<GameObject> upgradeSlots = new List<GameObject>();

    bool canBuyCard = true;
    bool canBuyUbgrade = true;


    GameSession myGameSession;

    // Start is called before the first frame update
    void Start()
    {
        myGameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyCard(int slotIndex)
    {

    }
}
