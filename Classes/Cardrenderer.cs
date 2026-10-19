
public class CardRenderer
{
    public string[] RenderCard(Card card)
    {
        string rank = GetRankSymbol(card.Rank);

        return new string[]
        {
            "┌───────┐",
            $"│{rank,-7}│",
            $"│   {GetSuitSymbol(card.Suit)}   │",
            $"│{rank,7}│",
            "└───────┘"
        };
    }

    private string GetSuitSymbol(Suit suit)
    {
        return suit switch
        {
            Suit.Hearts => "💓", 
            Suit.Diamonds => "♦",
            Suit.Clubs => "🍀",
            Suit.Spades => "♠",
            _ => "?"
        };
    }
    private string GetRankSymbol(Rank rank)
    {
        return rank switch
        {
            Rank.Ace => "A",
            Rank.Two => "2",
            Rank.Three => "3",
            Rank.Four => "4",
            Rank.Five => "5",
            Rank.Six => "6",
            Rank.Seven => "7",
            Rank.Eight => "8",
            Rank.Nine => "9",
            Rank.Ten => "10",
            Rank.Jack => "J",
            Rank.Queen => "Q",
            Rank.King => "K",
            _ => "?"
        };
    }
}
 