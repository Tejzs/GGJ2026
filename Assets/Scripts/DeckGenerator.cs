using UnityEngine;
using System.Collections.Generic;

public class DeckGenerator : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform parent;

    public List<CardData> deck = new List<CardData>();

    void Start()
    {
        int id = 1;

        foreach (Suit suit in System.Enum.GetValues(typeof(Suit)))
        {
            for (int number = 1; number <= 13; number++)
            {
                GameObject cardObj = Instantiate(cardPrefab, parent);
                CardData card = cardObj.GetComponent<CardData>();

                card.suit = suit;
                card.number = number;
                card.id = id++;

                deck.Add(card);
            }
        }

        // Joker (optional)
        GameObject joker = Instantiate(cardPrefab, parent);
        CardData jokerData = joker.GetComponent<CardData>();
        jokerData.id = 53;
    }
}