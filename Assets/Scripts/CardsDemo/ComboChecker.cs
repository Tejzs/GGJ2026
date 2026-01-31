using UnityEngine;

public struct ComboResult
{
    public bool hasCombo;
    public float multiplier;
    public string message;

    public ComboResult(bool combo, float mult, string msg)
    {
        hasCombo = combo;
        multiplier = mult;
        message = msg;
    }
}

public class ComboChecker
{
    public static ComboResult CheckCombos(Card[] hand)
    {
        // Count each rank in selected cards
        int[] rankCount = new int[14]; // Index 0 unused, 1-13 for ranks
        int selectedCount = 0;

        for (int i = 0; i < hand.Length; i++)
        {
            if (!hand[i].isEmpty && hand[i].selected)
            {
                rankCount[hand[i].rank]++;
                selectedCount++;
            }
        }

        // Check for specific combos
        // 3+ Aces (rank 1)
        if (rankCount[1] >= 3)
        {
            return new ComboResult(true, 3.0f, "TRIPLE ACES! 3x DAMAGE!");
        }

        // 3+ Kings (rank 13)
        if (rankCount[13] >= 3)
        {
            return new ComboResult(true, 2.5f, "TRIPLE KINGS! 2.5x DAMAGE!");
        }

        // 3+ Queens (rank 12)
        if (rankCount[12] >= 3)
        {
            return new ComboResult(true, 2.5f, "TRIPLE QUEENS! 2.5x DAMAGE!");
        }

        // 3+ of any same rank
        for (int rank = 2; rank <= 11; rank++)
        {
            if (rankCount[rank] >= 3)
            {
                return new ComboResult(true, 2.0f, $"TRIPLE {rank}'s! 2x DAMAGE!");
            }
        }

        // Pair of Aces
        if (rankCount[1] == 2)
        {
            return new ComboResult(true, 1.5f, "Pair of Aces! 1.5x damage");
        }

        // Any pair
        for (int rank = 1; rank <= 13; rank++)
        {
            if (rankCount[rank] == 2)
            {
                return new ComboResult(true, 1.3f, "Pair! 1.3x damage");
            }
        }

        return new ComboResult(false, 1.0f, "");
    }
}
