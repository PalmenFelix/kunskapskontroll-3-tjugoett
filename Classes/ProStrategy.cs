public class ProStrategy : IPlayerStrategy
{
    public Move MakeYourMove(Hand hand, Card dealersOpenCard)
    {
        int myPoints = hand.Value;
        int dealerPoints = dealersOpenCard.Value;

        // Mjuk hand: ett ess räknas som 11
        if (hand.IsSoft)
        {
            if (myPoints >= 19)
            {
                return Move.Stand;
            }
            if (myPoints == 18 && dealerPoints <= 8)
            {
                return Move.Stand;
            }
            return Move.Hit;
        }

        // Hård hand: inget ess som räknas som 11
        if (myPoints >= 17)
        {
            return Move.Stand;
        }
        if (myPoints >= 13 && dealerPoints <= 6)
        {
            return Move.Stand;
        }
        if (myPoints == 12 && dealerPoints >= 4 && dealerPoints <= 6)
        {
            return Move.Stand;
        }
        return Move.Hit;
    }
}