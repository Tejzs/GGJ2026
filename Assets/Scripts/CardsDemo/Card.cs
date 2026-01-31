using UnityEngine;

public enum Suit
{
    HEARTS,
    DIAMONDS,
    CLUBS,
    SPADES
}

[System.Serializable]
public class Card
{
    public Suit suit;
    public int rank; // 1-13
    public bool selected;
    public bool isEmpty;

    public Card(Suit s, int r)
    {
        suit = s;
        rank = r;
        selected = false;
        isEmpty = false;
    }

    public Card()
    {
        isEmpty = true;
        selected = false;
    }

    public Color GetSuitColor()
    {
        if (suit == Suit.HEARTS || suit == Suit.DIAMONDS)
            return Color.red;
        return Color.black;
    }

    public string GetSuitSymbol()
    {
        switch (suit)
        {
            case Suit.HEARTS: return "♥";
            case Suit.DIAMONDS: return "♦";
            case Suit.CLUBS: return "♣";
            case Suit.SPADES: return "♠";
            default: return "?";
        }
    }
}
