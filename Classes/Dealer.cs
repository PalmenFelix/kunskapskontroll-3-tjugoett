public class Dealer : Participant
{
    public bool HoleCardRevealed { get; private set; }
    private readonly bool hitsSoft17;

    public Dealer(bool hitsSoft17) : base("Givaren", ParticipantType.Dealer)
    {
        this.hitsSoft17 = hitsSoft17;
        HoleCardRevealed = false;
    }

    public bool ShouldHit()
    {
        if (Hand.Value <= 16 || (Hand.Value == 17 && Hand.IsSoft && hitsSoft17))
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