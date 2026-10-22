public class SimpleStrategy : IPlayerStrategy
{
    public Move MakeYourMove (Hand hand, Card dealersOpenCard)
    {
        if (hand.Value < 17)
        {
            return Move.Hit;
        }
        else
        {
            return Move.Stand;
        }
    }
}