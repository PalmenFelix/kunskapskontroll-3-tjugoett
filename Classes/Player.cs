public class Player : Participant
{
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Pushes { get; set; }
    public IPlayerStrategy Strategy { get; set; } = new SimpleStrategy();

    public Player(string name)
        : base(name, ParticipantType.Player)
    {
    }
}