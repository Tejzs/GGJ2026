using UnityEngine;

public enum Suitd
{
    Spade = 1,
    Heart = 2,
    Club = 3,
    Diamond = 4
}

public class CardData : MonoBehaviour
{
    [Range(1, 13)]
    public int number;   // 1 = Ace, 13 = King

    public Suitd suit;

    [Range(1, 52)]
    public int id;
}