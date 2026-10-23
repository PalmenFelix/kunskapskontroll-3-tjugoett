public class ProStrategy : IPlayerStrategy
{
    // Grundstrategi för hård hand (inget ess räknas som 11)
    // Kolumner = givarens öppna kort: 2 3 4 5 6 7 8 9 10 Ess
    private string[] hardTable =
    {
        "HHHHHHHHHH", // 11 eller mindre
        "HHSSSHHHHH", // 12
        "SSSSSHHHHH", // 13
        "SSSSSHHHHH", // 14
        "SSSSSHHHHH", // 15
        "SSSSSHHHHH", // 16
        "SSSSSSSSSS"  // 17 eller mer
    };

    // Grundstrategi för mjuk hand (ett ess räknas som 11)
    private string[] softTable =
    {
        "HHHHHHHHHH", // 17 eller mindre
        "SSSSSSSHHH", // 18
        "SSSSSSSSSS"  // 19 eller mer
    };

    public Move MakeYourMove(Hand hand, Card dealersOpenCard)
    {
        int column = dealersOpenCard.Value - 2;
        string row;

        if (hand.IsSoft)
        {
            int points = Math.Clamp(hand.Value, 17, 19);
            row = softTable[points - 17];
        }
        else
        {
            int points = Math.Clamp(hand.Value, 11, 17);
            row = hardTable[points - 11];
        }

        if (row[column] == 'H')
        {
            return Move.Hit;
        }
        return Move.Stand;
    }
}