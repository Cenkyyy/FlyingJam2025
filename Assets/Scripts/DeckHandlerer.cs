using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DeckHandlerer : MonoBehaviour
{
    [SerializeField] GameObject backgroundButtonCollider;

    [SerializeField] List<Sprite> operationSprites;

    [SerializeField] List<GameObject> smallPlusCards;
    [SerializeField] List<GameObject> bigPlusCards;
    [SerializeField] List<GameObject> smallMinusCards;
    [SerializeField] List<GameObject> bigMinusCards;
    [SerializeField] List<GameObject> multiplicationCards;
    [SerializeField] List<GameObject> divisionCards;
    [SerializeField] List<GameObject> ceasarCards;

    [SerializeField] List<GameObject> handCards;

    private GameSession gameSession;
    private WordEditor wordEditor;
    private List<GameSession.CardType> deck = new();
    private int lastClickedHandCardID;


    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        wordEditor = FindObjectOfType<WordEditor>();
        backgroundButtonCollider.SetActive(false);

        StartCoroutine(DealCardsWithDelay());
    }

    // Coroutine that waits 1 second before dealing the cards
    private IEnumerator DealCardsWithDelay()
    {
        yield return new WaitForSeconds(1f);  // Wait for 1 second
        DealAndDisplayHandCards();
    }

    public GameSession.CardType GetCardsType(int positionID)
    {
        return deck[positionID];
    }

    public int GetCardsValue(int positionID)
    {
        // TODO fix this
        return 0;
    }

    public int GetLastCardID()
    {
        return lastClickedHandCardID;
    }

    public bool IsHandEmpty()
    {
        foreach (var card in handCards)
        {
            if (card.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    public List<GameObject> GetHandCards()
    {
        return handCards;
    }

    private void CardSetUp()
    {
        for (int i = 0; i < handCards.Count; i++)
        {
            if (i < gameSession.handSize)
            {
                handCards[i].SetActive(true);
            }
            else handCards[i].SetActive(false);
        }
    }

    private void SetHandCardsVisible()
    {
        foreach (var handCard in handCards)
        {
            Image cardImage = handCard.GetComponent<Image>();
            if (cardImage != null)
            {
                Color newColor = cardImage.color;
                newColor.a = 1f;  // Set alpha to fully visible
                cardImage.color = newColor;
            }
        }
    }

    public void DealAndDisplayHandCards()
    {
        CardSetUp();
        System.Random random = new System.Random();
        deck.Clear();

        GameSession.CardType[] allCardTypes = Enum.GetValues(typeof(GameSession.CardType))
            .Cast<GameSession.CardType>()
            .Where(t => t != GameSession.CardType.Invalid)  // Exclude Invalid
            .ToArray();

        for (int i = 0; i < gameSession.handSize; i++)
        {
            int randomIndex = random.Next(allCardTypes.Length - 1);
            Image cardImage = handCards[i].GetComponent<Image>();
            deck.Add(allCardTypes[randomIndex]);
            cardImage.sprite = operationSprites[randomIndex];
        }

        SetHandCardsVisible();
    }

    public void GetNewHand()
    {
        StartCoroutine(DealCardsWithDelay());
        gameSession.handsCount--;
    }

    public void OnHandCardClicked(int positionID)
    {
        lastClickedHandCardID = positionID;
        GameSession.CardType type = deck[positionID];

        wordEditor.SetClickedCardType(type);
        
        List<GameObject> overlayToDisplay;
        int cardsSize;
        switch (type)
        {
            case GameSession.CardType.SmallPlus:
                overlayToDisplay = smallPlusCards;
                cardsSize = gameSession.smallSignUpperBound;
                break;
            case GameSession.CardType.BigPlus:
                overlayToDisplay = bigPlusCards;
                cardsSize = gameSession.bigSignUpperBound;
                break;
            case GameSession.CardType.SmallMinus:
                overlayToDisplay = smallMinusCards;
                cardsSize = gameSession.smallSignUpperBound;
                break;
            case GameSession.CardType.BigMinus:
                overlayToDisplay = bigMinusCards;
                cardsSize = gameSession.bigSignUpperBound;
                break;
            case GameSession.CardType.Multiplication:
                overlayToDisplay = multiplicationCards;
                cardsSize = gameSession.multiplicationUpperBound;
                break;
            case GameSession.CardType.Division:
                overlayToDisplay = divisionCards;
                cardsSize = gameSession.divisionUpperBound;
                break;
            case GameSession.CardType.Ceasar:
                overlayToDisplay = ceasarCards;
                cardsSize = 6;
                break;
            default:
                return;
        }
        backgroundButtonCollider.SetActive(true);
        OnHandCardClickedHelper(overlayToDisplay, cardsSize);
    }

    private void OnHandCardClickedHelper(List<GameObject> cards, int count)
    {
        for(int i = 0; i < count; i++)
        {
            cards[i].SetActive(true);
        }
    }

    public void OnValueCardClicked()
    {
        backgroundButtonCollider.SetActive(false);
        
        OnValueCardClickedHelper(smallPlusCards);
        OnValueCardClickedHelper(bigPlusCards);
        OnValueCardClickedHelper(smallMinusCards);
        OnValueCardClickedHelper(bigMinusCards);
        OnValueCardClickedHelper(multiplicationCards);
        OnValueCardClickedHelper(divisionCards);
        OnValueCardClickedHelper(ceasarCards);
    }

    private void OnValueCardClickedHelper(List<GameObject> cards)
    {
        foreach (var card in cards)
        {
            card.SetActive(false);
        }
    }
}
