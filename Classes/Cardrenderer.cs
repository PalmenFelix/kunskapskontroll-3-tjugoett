

public class CardRenderer
{
    public string[] RenderCard(Card card)
    {
        string rank = GetRankSymbol(card.Rank);
        string suit = GetSuitSymbol(card.Suit);

        return new string[]
        {
            "┌───────┐",
            $"│{rank,-7}│",
            "│       │",
            $"│   {suit}   │",
            "│       │",
            $"│{rank,7}│",
            "└───────┘"
        };
    }

    public string[] RenderCardBack()
    {
        return new string[]
        {
            "┌───────┐",
            "│▒▒▒▒▒▒▒│",
            "│▒▒▒▒▒▒▒│",
            "│▒▒▒▒▒▒▒│",
            "│▒▒▒▒▒▒▒│",
            "│▒▒▒▒▒▒▒│",
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

    private string GetRankSymbol(Rank rank)
    {
        return rank switch
        {
            Rank.ace => "A",
            Rank.two => "2",
            Rank.three => "3",
            Rank.four => "4",
            Rank.five => "5",
            Rank.six => "6",
            Rank.seven => "7",
            Rank.eight => "8",
            Rank.nine => "9",
            Rank.ten => "10",
            Rank.jack => "J",
            Rank.queen => "Q",
            Rank.king => "K",
            _ => "?"
        };
    }
}