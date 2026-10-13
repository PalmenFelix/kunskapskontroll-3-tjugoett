class Deck
{
    private List <Card> cards = new List<Card>();
    private Random random;

    public Deck(Random random)
    {
        this.random = random;
        foreach (Suit suit in Enum.GetValues<Suit>())
        {
             foreach (Rank rank in Enum.GetValues<Rank>())
            {
                cards.Add(new Card(suit, rank));
            }
        }
    }
}