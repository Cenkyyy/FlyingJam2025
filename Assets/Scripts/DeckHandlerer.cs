using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandlerer : MonoBehaviour
{
    [SerializeField] List<GameObject> smallPlusCards;
    [SerializeField] List<GameObject> bigPlusCards;
    [SerializeField] List<GameObject> smallMinusCards;
    [SerializeField] List<GameObject> bigMinusCards;
    [SerializeField] List<GameObject> multiplicationCards;
    [SerializeField] List<GameObject> divisionCards;
    [SerializeField] List<GameObject> ceasarCards;

    private GameSession gameSession;
    private List<GameSession.CardType> deck;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    public GameSession.CardType GetCardsType(int positionID)
    {
        return deck[positionID];
    }

    public int GetCardsValue(int positionID)
    {
        return 0;
    }

    public void OnHandCardClicked(int positionID)
    {
        
        // Make it not visible
        // Display overlay with all possible cards in the middle of the Screen
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
