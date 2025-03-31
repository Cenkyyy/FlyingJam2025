using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameSession;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public enum UpgradeType
    {
        shopUpgrade,
        handSizeUpgrade,
        handCoundUpgrade,
        smallSignUpgrade,
        bigSignUpgrade,
        multiplicationUpgrade,
        divisionUgrade,
    }

    [SerializeField] Sprite[] cardSprites;
    [SerializeField] Sprite[] upgradeSprites;

    [SerializeField] int maxCardSlots = 16;
    [SerializeField] int maxUpgradeSlots = 16;

    List<int> currentCardSlots = new();
    List<int> currentUpgradeSlots = new();

    List<UpgradeType> possibleUpgrades = new();
    int cardsPerMachine;
    int tokensToSpend;

    Dictionary<int, CardType> cardVendingMachine = new();
    Dictionary<int, UpgradeType> upgradeVendingMachine = new();

    [SerializeField] List<VendingItemPopup> cardItems = new();
    [SerializeField] List<VendingItemPopup> upgradeItems = new();

    [SerializeField] List<GameObject> cardPopups;
    [SerializeField] List<GameObject> upgradePopups;
    

    System.Random rand = new System.Random();

    GameSession myGameSession;

    void Start()
    {
        myGameSession = GameSession.Instance;

        tokensToSpend = 2;
        cardsPerMachine = myGameSession.shopCardCount;
        currentCardSlots = GetRandomNumbers(maxCardSlots, cardsPerMachine);
        currentUpgradeSlots = GetRandomNumbers(maxUpgradeSlots, cardsPerMachine);
        ChoosePossibleUpgrades();
        SetUpShop();
        SetActiveCards();
        SetActiveUpgrades();
        SetCardPopups();
        SetUpgradePopups();
    }

    public void SetCardPopups()
    {
        foreach(int index in currentCardSlots)
        {
            cardPopups[index-1].GetComponent<Image>().sprite = cardSprites[(int)cardVendingMachine[index]];
        }
    }

    public void SetUpgradePopups()
    {
        foreach (int index in currentUpgradeSlots)
        {
            upgradePopups[index - 1].GetComponent<Image>().sprite = upgradeSprites[(int)upgradeVendingMachine[index]];
        }
    }

    public void SetActiveCards()
    {
        foreach(var item in cardItems)
        {
            item.gameObject.SetActive(false);
        }

        foreach(int index in currentCardSlots)
        {
            cardItems[index-1].gameObject.SetActive(true);
        }
    }

    public void SetActiveUpgrades()
    {
        foreach (var item in upgradeItems)
        {
            item.gameObject.SetActive(false);
        }

        foreach (int index in currentUpgradeSlots)
        {
            upgradeItems[index-1].gameObject.SetActive(true);
        }
    }

    public void SetUpShop()
    {
        for (int i = 0; i < cardsPerMachine; i++)
        {
            var cards = Enum.GetValues(typeof(CardType)).Cast<CardType>().ToList();
            cards.Remove(CardType.Invalid);
            cardVendingMachine[currentCardSlots[i]] = GetRandomElement(cards);

            upgradeVendingMachine[currentUpgradeSlots[i]] = GetRandomElement(possibleUpgrades);
        }
    }

    public void ChoosePossibleUpgrades()
    {
        if (myGameSession.smallSignUpperBound < 5)
        {
            possibleUpgrades.Add(UpgradeType.smallSignUpgrade);
        }

        if (myGameSession.bigSignUpperBound < 10)
        {
            possibleUpgrades.Add(UpgradeType.bigSignUpgrade);
        }

        if (myGameSession.multiplicationUpperBound < 5)
        {
            possibleUpgrades.Add(UpgradeType.multiplicationUpgrade);
        }

        if (myGameSession.divisionUpperBound < 5)
        {
            possibleUpgrades.Add(UpgradeType.divisionUgrade);
        }

        if (myGameSession.shopCardCount < 5)
        {
            possibleUpgrades.Add(UpgradeType.shopUpgrade);
        }

        if (myGameSession.handSize < 6)
        {
            possibleUpgrades.Add(UpgradeType.handSizeUpgrade);
        }

        if (myGameSession.handsCount < 10)
        {
            possibleUpgrades.Add(UpgradeType.handCoundUpgrade);
        }

    }

    public void BuyCard(int slotIndex)
    {
        if (tokensToSpend > 0)
        {
            myGameSession.AddCard(cardVendingMachine[slotIndex]);
            currentCardSlots.Remove(slotIndex);
            SetActiveCards();
            tokensToSpend -= 1;
        }
        else
        {
            Debug.Log("no tokens");
            // TODO: add effect for telling player he has no tokens.
        }
    }

    public void BuyUpgrade(int slotIndex)
    {
        if (tokensToSpend > 0)
        {
            CarryOutUpdate(upgradeVendingMachine[slotIndex]);
            currentUpgradeSlots.Remove(slotIndex);
            SetActiveUpgrades();
            tokensToSpend -= 1;
        }
        else
        {
            Debug.Log("no tokens");
            // TODO: add effect for telling player he has no tokens.
        }
    }

    public void CarryOutUpdate(UpgradeType upgrade)
    {
        switch (upgrade)
        {
            case UpgradeType.smallSignUpgrade:
                myGameSession.smallSignUpperBound += 1;
                break;
            case UpgradeType.bigSignUpgrade:
                myGameSession.bigSignUpperBound += 1;
                break;
            case UpgradeType.multiplicationUpgrade:
                myGameSession.multiplicationUpperBound += 1;
                break;
            case UpgradeType.divisionUgrade:
                myGameSession.divisionUpperBound += 1;
                break;
            case UpgradeType.handSizeUpgrade:
                myGameSession.handSize += 1;
                break;
            case UpgradeType.handCoundUpgrade:
                myGameSession.handsCount += 1;
                break;
            case UpgradeType.shopUpgrade:
                myGameSession.shopCardCount += 1;
                break;
        }
    }

    public List<int> GetRandomNumbers(int n, int k)
    {
        List<int> numbers = Enumerable.Range(1, n).ToList();

        for (int i = 0; i < k; i++)
        {
            int j = rand.Next(i, n);
            (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
        }

        return numbers.Take(k).ToList();
    }

    public T GetRandomElement<T>(List<T> list)
    {
        if (list == null || list.Count == 0)
            throw new ArgumentException("List cannot be null or empty");

        return list[rand.Next(list.Count)];
    }
}
