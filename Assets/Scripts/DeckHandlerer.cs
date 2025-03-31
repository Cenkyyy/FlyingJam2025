using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DeckHandlerer : MonoBehaviour
{
    // Types of overlay cards
    [SerializeField] List<GameObject> smallPlusCards;
    [SerializeField] List<GameObject> bigPlusCards;
    [SerializeField] List<GameObject> smallMinusCards;
    [SerializeField] List<GameObject> bigMinusCards;
    [SerializeField] List<GameObject> multiplicationCards;
    [SerializeField] List<GameObject> divisionCards;
    [SerializeField] List<GameObject> ceasarCards;

    // Sprites of the operation cards without values
    [SerializeField] List<Sprite> operationSprites;

    // Collider for overlay cards
    [SerializeField] GameObject backgroundButtonCollider;

    // Current cards on hand
    [SerializeField] List<GameObject> handDisplay;

    private GameSession _myGameSession;
    private WordEditor _myWordEditor;

    private System.Random random = new System.Random();

    private List<GameSession.CardType> handCards = new();
    private List<GameSession.CardType> cardStockPile = new();

    private int lastClickedHandCardID;

    void Start()
    {
        _myGameSession = GameSession.Instance;

        _myWordEditor = FindObjectOfType<WordEditor>();

        // Card setup
        backgroundButtonCollider.SetActive(false);
        StartCoroutine(DisplayCardSpritesDelay());
    }

    // After scene is ran, sets hand cards on canvas active based on _myGameSession.handSize
    private void CardSetUp()
    {
        for (int i = 0; i < handDisplay.Count; i++)
        {
            if (i < _myGameSession.handSize)
            {
                handDisplay[i].SetActive(true);
            }
            else handDisplay[i].SetActive(false);
        }
    }

    // Deals cards to new current hand from stockpile
    private void DealCards()
    {
        handCards.Clear();

        for (int i = 0; i < _myGameSession.handSize; i++)
        {
            if (cardStockPile.Count == 0)
            {
                GetNewStockPile();
            }
            int randomIndex = random.Next(cardStockPile.Count);
            handCards.Add(cardStockPile[randomIndex]);
            cardStockPile.RemoveAt(randomIndex);
        }
    }

    private void GetNewStockPile()
    {
        cardStockPile = new List<GameSession.CardType>(_myGameSession.playerDeck);
    }

    // Coroutine that waits 1 second before dealing the cards
    private IEnumerator DisplayCardSpritesDelay()
    {
        yield return new WaitForSeconds(1f);  // Wait for 1 second
        CardSetUp();
        DealCards();
        DisplayCardSprites();
    }

    // Sets cards sprites and displays cards to be fully visible
    private void DisplayCardSprites()
    {
        for (int i = 0; i < _myGameSession.handSize; i++)
        {
            Image cardImage = handDisplay[i].GetComponent<Image>();

            int spriteIndex;
            switch (handCards[i])
            {
                case GameSession.CardType.SmallPlus:
                    spriteIndex = 0;
                    break;
                case GameSession.CardType.BigPlus:
                    spriteIndex = 1;
                    break;
                case GameSession.CardType.SmallMinus:
                    spriteIndex = 2;
                    break;
                case GameSession.CardType.BigMinus:
                    spriteIndex = 3;
                    break;
                case GameSession.CardType.Multiplication:
                    spriteIndex = 4;
                    break;
                case GameSession.CardType.Division:
                    spriteIndex = 5;
                    break;
                case GameSession.CardType.Ceasar:
                    spriteIndex = 6;
                    break;
                default:
                    return;
            }
            cardImage.sprite = operationSprites[spriteIndex];
        }

        SetHandCardsVisible();
    }
    
    // Sets cards opacity to 1
    private void SetHandCardsVisible()
    {
        foreach (var handCard in handDisplay)
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

    // When current hand is empty, creates new one
    public void GetNewHand()
    {
        StartCoroutine(DisplayCardSpritesDelay());
        _myGameSession.handsCount--;
    }

    public bool IsHandEmpty()
    {
        foreach (var card in handDisplay)
        {
            if (card.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    public void OnHandCardClicked(int positionID)
    {
        lastClickedHandCardID = positionID;
        GameSession.CardType type = handCards[positionID];

        _myWordEditor.SetClickedCardType(type);
        
        List<GameObject> overlayToDisplay;
        int cardsSize;
        switch (type)
        {
            case GameSession.CardType.SmallPlus:
                overlayToDisplay = smallPlusCards;
                cardsSize = _myGameSession.smallSignUpperBound;
                break;
            case GameSession.CardType.BigPlus:
                overlayToDisplay = bigPlusCards;
                cardsSize = _myGameSession.bigSignUpperBound - 5;
                break;
            case GameSession.CardType.SmallMinus:
                overlayToDisplay = smallMinusCards;
                cardsSize = _myGameSession.smallSignUpperBound;
                break;
            case GameSession.CardType.BigMinus:
                overlayToDisplay = bigMinusCards;
                cardsSize = _myGameSession.bigSignUpperBound - 5;
                break;
            case GameSession.CardType.Multiplication:
                overlayToDisplay = multiplicationCards;
                cardsSize = _myGameSession.multiplicationUpperBound;
                break;
            case GameSession.CardType.Division:
                overlayToDisplay = divisionCards;
                cardsSize = _myGameSession.divisionUpperBound;
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

    // Displays
    private void OnHandCardClickedHelper(List<GameObject> cards, int count)
    {
        for(int i = 0; i < count; i++)
        {
            cards[i].SetActive(true);
        }
    }

    // Turns off all overlays and disables hand card collider
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

    // Turns off card overlay
    private void OnValueCardClickedHelper(List<GameObject> cards)
    {
        foreach (var card in cards)
        {
            card.SetActive(false);
        }
    }

    // Returns card that was last clicked to be removed after its operation has been applied
    public GameObject GetLastClickedCard()
    {
        return handDisplay[lastClickedHandCardID];
    }
}
