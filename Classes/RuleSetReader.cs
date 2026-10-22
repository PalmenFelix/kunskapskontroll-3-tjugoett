
public class RuleSetReader
{
    private const int CardsPerDeck = 52;
    private const int MaxDecks = 8;
    private const int MinReshuffleThreshold = 15;
    private string filePath = "houserules.txt";
    public List<string> Warnings { get; } = new List<string>();
    public RuleSet Load()
    {
        Warnings.Clear();
        RuleSet rules = new RuleSet();

        if (!File.Exists(filePath))
        {
            Warnings.Add("Hittade inte houserules.txt. Standardregler används");
            return rules;
        }

        string[] lines;
        try
        {
            lines = File.ReadAllLines(filePath);
        }
        catch (IOException)
        {
            Warnings.Add("Det gick inte att läsa houserules.txt. Standardregler används");
            return rules;
        }
        catch (UnauthorizedAccessException)
        {
            Warnings.Add("Det gick inte att läsa houserules.txt. Standardregler används");
            return rules;
        }

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();

            if (line == "" || line.StartsWith("#"))
                continue;
            string[] parts = line.Split('=', 2);
            if (parts.Length != 2)
            {
                Warnings.Add($"Rad utan '=' ignorerades: {line}");
                continue;
            }
            string key = parts[0].Trim();
            string value = parts[1].Trim();

            switch (key)
            {
                case "DealerHitsSoft17":
                    if (bool.TryParse(value, out bool hitsSoft17))
                        rules.DealerHitsSoft17 = hitsSoft17;
                    else
                        Warnings.Add($"Ogiltigt värde för DealerHitsSoft17: {value}");
                    break;
                case "NumberOfDecks":
                    if (int.TryParse(value, out int decks) && decks >= 1 && decks <= MaxDecks)
                        rules.NumberOfDecks = decks;
                    else
                        Warnings.Add($"NumberOfDecks måste vara 1-{MaxDecks}, fick: {value}");
                    break;
                case "ReshuffleThreshold":
                    if (int.TryParse(value, out int threshold))
                        rules.ReshuffleThreshold = threshold;
                    else
                        Warnings.Add($"ReshuffleThreshold måste vara ett heltal, fick: {value}");
                    break;
                case "NaturalBeatsMultiCard21":
                    if (bool.TryParse(value, out bool naturalBeats))
                        rules.NaturalBeatsMultiCard21 = naturalBeats;
                    else
                        Warnings.Add($"Ogiltigt värde för NaturalBeatsMultiCard21: {value}");
                    break;
                default:
                    Warnings.Add($"Okänd regel ignorerades: {key}");
                    break;
            }
        }
        int totalCards = rules.NumberOfDecks * CardsPerDeck;

        if (rules.ReshuffleThreshold < MinReshuffleThreshold || rules.ReshuffleThreshold >= totalCards)
        {
            Warnings.Add($"ReshuffleThreshold måste vara minst {MinReshuffleThreshold} och mindre än {totalCards}. Standardvärdet {MinReshuffleThreshold} används");
            rules.ReshuffleThreshold = MinReshuffleThreshold;
        }
        return rules;
    }
}