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
    private List<GameSession.CardType> deck;
    private int lastClickedHandCardID;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
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

    public void OnHandCardClicked(int positionID)
    {
        backgroundButtonCollider.SetActive(true);

        lastClickedHandCardID = positionID;
        GameSession.CardType type = deck[positionID];
        List<GameObject> overlayToDisplay;
        
        switch (type)
        {
            case GameSession.CardType.SmallPlus:
                overlayToDisplay = smallMinusCards;
                break;
            case GameSession.CardType.BigPlus:
                overlayToDisplay = smallMinusCards;
                break;
            case GameSession.CardType.SmallMinus:
                overlayToDisplay = smallMinusCards;
                break;
            case GameSession.CardType.BigMinus:
                overlayToDisplay = smallMinusCards;
                break;
            case GameSession.CardType.Multiplication:
                overlayToDisplay = smallMinusCards;
                break;
            case GameSession.CardType.Division:
                overlayToDisplay = smallMinusCards;
                break;
            case GameSession.CardType.Ceasar:
                overlayToDisplay = smallMinusCards;
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

        // remove last clicked hand card
        handCards[lastClickedHandCardID].SetActive(false);
    }

    private void OnValueCardClickedHelper(List<GameObject> cards)
    {
        foreach (var card in cards)
        {
            card.SetActive(false);
        }
    }
}
