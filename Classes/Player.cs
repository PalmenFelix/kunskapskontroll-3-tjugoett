public class Player : Participant
{
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Pushes { get; set; }

    public Player(string name)
        : base(name, ParticipantType.Player)
    {
    }

    // feature-4: Chips for players
    public Chips Chips { get; } = new Chips();
    
}