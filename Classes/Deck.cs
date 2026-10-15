
class Deck
{
    private List <Card> cards = new List<Card>();
    private Random random;

    public Deck(Random random)
    {
        this.random = random;
        BuildCards();

    }
    private void BuildCards()
    {
        foreach (Suit suit in Enum.GetValues<Suit>())
        {
             foreach (Rank rank in Enum.GetValues<Rank>())
            {
                cards.Add(new Card{Suit = suit, Rank=rank});
            }
        }
    }

    public void Reset()
    {
        cards.Clear();
        BuildCards();
        Shuffle();
    }
    public void Shuffle()
    {
        for(int i =cards.Count -1; i>0; i--)
        {
            int j =random.Next(0, i+1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }
    public Card Deal()
    {
        if (cards.Count == 0)
        {
            throw new InvalidOperationException("Kortleken är tom"); 
        }
        int lastIndex = cards.Count -1;
        Card card = cards[lastIndex];
        cards.RemoveAt(lastIndex);
        return card;
    }
    public int GetCount()
    {
        return cards.Count;
    }
}