public class Dealer : Participant
{
    public bool HoleCardRevealed { get; private set; }

    public Dealer() : base("Givaren", ParticipantType.Dealer)
    {
        HoleCardRevealed = false;
    }

    public bool ShouldHit()
    {
        if (Hand.Value <= 16)
        {
            return true;
        }

        return false;
    }

    public void RevealHoleCard()
    {
        HoleCardRevealed = true;
    }

    public void StartNewRound()
    {
        Hand.Cards.Clear();
        HoleCardRevealed = false;
    }
}