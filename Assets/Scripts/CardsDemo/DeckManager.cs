using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private const int MAX_DECK = 52;
    private const int HAND_SIZE = 5;

    public List<Card> drawPile = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public Card[] hand = new Card[HAND_SIZE];
    public int handCount = 0;

    public void InitDeck()
    {
        drawPile.Clear();
        discardPile.Clear();
        handCount = 0;

        // Create 52 cards
        for (int s = 0; s < 4; s++)
        {
            for (int r = 1; r <= 13; r++)
            {
                drawPile.Add(new Card((Suit)s, r));
            }
        }

        // Initialize hand slots as empty
        for (int i = 0; i < HAND_SIZE; i++)
        {
            hand[i] = new Card();
            hand[i].isEmpty = true;
        }
    }

    public void ShuffleDeck()
    {
        for (int i = drawPile.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Card temp = drawPile[i];
            drawPile[i] = drawPile[j];
            drawPile[j] = temp;
        }
    }

    public int DrawCard()
    {
        if (drawPile.Count <= 0) return -1;

        // Find first empty slot
        int targetSlot = -1;
        for (int i = 0; i < HAND_SIZE; i++)
        {
            if (hand[i].isEmpty)
            {
                targetSlot = i;
                break;
            }
        }

        if (targetSlot == -1) return -1; // No empty slots

        Card drawn = drawPile[drawPile.Count - 1];
        drawPile.RemoveAt(drawPile.Count - 1);
        drawn.isEmpty = false;

        hand[targetSlot] = drawn;
        handCount++;

        return targetSlot;
    }

    public void DealHand()
    {
        // Mark all slots as empty first
        for (int i = 0; i < HAND_SIZE; i++)
        {
            hand[i] = new Card();
            hand[i].isEmpty = true;
        }
        handCount = 0;

        // Draw cards into empty slots
        for (int i = 0; i < HAND_SIZE; i++)
        {
            DrawCard();
        }
    }

    public void DiscardSelectedCards()
    {
        for (int i = 0; i < HAND_SIZE; i++)
        {
            if (!hand[i].isEmpty && hand[i].selected)
            {
                hand[i].selected = false;
                discardPile.Add(hand[i]);
                hand[i] = new Card();
                hand[i].isEmpty = true;
                handCount--;
            }
        }

        // Refill empty slots
        for (int i = 0; i < HAND_SIZE && drawPile.Count > 0; i++)
        {
            if (hand[i].isEmpty)
            {
                DrawCard();
            }
        }
    }

    public int CalculateDamage()
    {
        int damage = 0;
        for (int i = 0; i < HAND_SIZE; i++)
        {
            if (!hand[i].isEmpty && hand[i].selected)
                damage += hand[i].rank;
        }
        return damage;
    }
}
