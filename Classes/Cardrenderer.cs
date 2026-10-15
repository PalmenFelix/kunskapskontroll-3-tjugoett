
public class CardRenderer
{
    public string[] RenderCard(Card card)
    {
        string rank = card.Rank.ToString();

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
        Suit.Hearts => "♥",
        Suit.Diamonds => "♦",
        Suit.Clubs => "♣",
        Suit.Spades => "♠",
        _ => "?"
    };
    }
}
