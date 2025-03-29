using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameSession;

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

    [SerializeField] int maxCardSlots = 16;
    [SerializeField] int maxUpgradeSlots = 16;

    List<int> currentCardSlots = new();
    List<int> currentUpgradeSlots = new();

    List<UpgradeType> possibleUpgrades = new();
    int cardsPerMachine;
    int tokensToSpend;

    Dictionary<int, CardType> cardVendinMachine;
    Dictionary<int, UpgradeType> upgradeVendingMachine;

    System.Random rand = new System.Random();

    GameSession myGameSession;

    // Start is called before the first frame update
    void Start()
    {
        myGameSession = FindObjectOfType<GameSession>();

        tokensToSpend = 2;
        cardsPerMachine = myGameSession.shopCardCount;
        currentCardSlots = GetRandomNumbers(maxCardSlots, cardsPerMachine);
        currentUpgradeSlots = GetRandomNumbers(maxUpgradeSlots, cardsPerMachine);
        ChoosePossibleUpgrades();
        SetUpShop();
    }
    
    public void SetUpShop()
    {
        for (int i = 0; i < cardsPerMachine; i++)
        {
            cardVendinMachine[currentCardSlots[i]] = 
                GetRandomElement(Enum.GetValues(typeof(CardType)).Cast<CardType>().ToList());
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
            myGameSession.AddCard(cardVendinMachine[slotIndex]);
            tokensToSpend -= 1;
        }
        else
        {
            // TODO: add effect for telling player he has no tokens.
        }
    }

    public void BuyUpgrade(int slotIndex)
    {
        if (tokensToSpend > 0)
        {
            CarryOutUpdate(upgradeVendingMachine[slotIndex]);
            tokensToSpend -= 1;
        }
        else
        {
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
