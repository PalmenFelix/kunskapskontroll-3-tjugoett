public class RuleSet
{
    public bool DealerHitsSoft17 { get; set; } = false;
    public int NumberOfDecks { get; set; } = 1;
    public int ReshuffleThreshold { get; set; } = 15;
    public bool NaturalBeatsMultiCard21 { get; set; } = true;
}