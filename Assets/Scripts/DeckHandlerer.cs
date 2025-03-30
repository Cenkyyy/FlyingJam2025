using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandlerer : MonoBehaviour
{
    [SerializeField] GameObject backgroundButtonCollider;

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
    private List<GameSession.CardType> deck;
    private int lastClickedHandCardID;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        wordEditor = FindObjectOfType<WordEditor>();
        backgroundButtonCollider.SetActive(false);
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

    public List<GameObject> GetHandCards()
    {
        return handCards;
    }


    public void OnHandCardClicked(int positionID)
    {
        backgroundButtonCollider.SetActive(true);
        
        lastClickedHandCardID = positionID;
        
        GameSession.CardType type = deck[positionID];
        wordEditor.SetClickedCardType(type);
        
        List<GameObject> overlayToDisplay;
        switch (type)
        {
            case GameSession.CardType.SmallPlus:
                overlayToDisplay = smallMinusCards;
                break;
            case GameSession.CardType.BigPlus:
                overlayToDisplay = bigPlusCards;
                break;
            case GameSession.CardType.SmallMinus:
                overlayToDisplay = smallMinusCards;
                break;
            case GameSession.CardType.BigMinus:
                overlayToDisplay = bigMinusCards;
                break;
            case GameSession.CardType.Multiplication:
                overlayToDisplay = multiplicationCards;
                break;
            case GameSession.CardType.Division:
                overlayToDisplay = divisionCards;
                break;
            case GameSession.CardType.Ceasar:
                overlayToDisplay = ceasarCards;
                break;
            default:
                return;
        }

        OnHandCardClickedHelper(overlayToDisplay);
    }

    private void OnHandCardClickedHelper(List<GameObject> cards)
    {
        foreach (var card in cards)
        {
            card.SetActive(true);
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
