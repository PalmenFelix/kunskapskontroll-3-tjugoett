public enum ParticipantType
{
    Player,
    CPU,
    Dealer
}

public abstract class Participant
{
    public string Name { get; private set; }
    public ParticipantType Type { get; }
    public Hand Hand { get; private set; }

    protected Participant(string name, ParticipantType type)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Participant name cannot be empty.",
                nameof(name));

        Name = name;
        Type = type;
        Hand = new Hand();
    }
}