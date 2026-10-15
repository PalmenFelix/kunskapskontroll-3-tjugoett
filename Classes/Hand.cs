card-worth-branch
using System.Collections.Generic;

public class Hand
{
    public List<Card> Cards { get; } = new List<Card>();

    public int Value
    {
        get
        {
            int value = 0;
            int aceWorth = 0;
            foreach (var card in Cards)
            {
                value += card.Value;
                if (card.Rank == Rank.ace)
                {
                    aceWorth++;
                }
            }
            while (value > 21 && aceWorth > 0)
            {
                value -= 10;
                aceWorth--;
            }

            return value;
        }
    }
    public bool IsBust => Value > 21;

    public bool IsBlackjack => Cards.Count == 2 && Value == 21;

    public bool IsSoft
{
    get
    {
        int value = 0;
        int aces = 0;

        foreach (var card in Cards)
        {
            value += card.Value;

            if (card.Rank == Rank.ace)
                aces++;
        }

        while (value > 21 && aces > 0)
        {
            value -= 10;
            aces--;
        }

        return aces > 0;
    }
}
                    
}
