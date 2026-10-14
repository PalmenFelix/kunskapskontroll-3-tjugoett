
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
    public void Shuffle()
    {
        for(int i =cards.Count -1; i>0; i--)
        {
            int j =random.Next(0, i+1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }
}