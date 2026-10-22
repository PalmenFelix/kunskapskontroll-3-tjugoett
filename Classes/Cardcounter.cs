public class CardCounter
{
    public int RunningCount { get; private set; }

    public void Observe(Card card)
    {
        switch (card.Rank)
        {
            case Rank.two:
            case Rank.three:
            case Rank.four:
            case Rank.five:
            case Rank.six:
                RunningCount++;
                break;

            case Rank.seven:
            case Rank.eight:
            case Rank.nine:
                break;

            case Rank.ten:
            case Rank.jack:
            case Rank.queen:
            case Rank.king:
            case Rank.ace:
                RunningCount--;
                break;
        }
    }

    public double GetTrueCount(int remainingCards)
    {
        if (remainingCards <= 0)
        {
            return 0;
        }

        double remainingDecks = remainingCards / 52.0;

        return RunningCount / remainingDecks;
    }

    public string GetIndicator(int remainingCards)
    {
        double trueCount = GetTrueCount(remainingCards);

        if (trueCount > 0)
        {
            return "Gynnsamt för spelaren";
        }

        if (trueCount < 0)
        {
            return "Ogynnsamt för spelaren";
        }

        return "Neutralt läge";
    }

    public void Reset()
    {
        RunningCount = 0;
    }
}