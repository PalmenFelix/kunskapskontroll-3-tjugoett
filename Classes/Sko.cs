using System;
using System.Collections.Generic;

public class Shoe
{
    private List<Card> cards = new List<Card>();

    private Random random;
    private int numberOfDecks;

    private int dealtCards;

    private int cutCardPosition;

    public bool NeedsShuffle { get; private set; }

    public Shoe(int numberOfDecks, Random random)
    {
        if (numberOfDecks < 1)
        {
            throw new ArgumentException(
                "Skon måste innehålla minst en kortlek.");
        }

        this.numberOfDecks = numberOfDecks;
        this.random = random;

        Reset();
    }

    private void BuildCards()
    {
        for (int deck = 0; deck < numberOfDecks; deck++)
        {
            foreach (Suit suit in Enum.GetValues<Suit>())
            {
                foreach (Rank rank in Enum.GetValues<Rank>())
                {
                    cards.Add(new Card
                    {
                        Suit = suit,
                        Rank = rank
                    });
                }
            }
        }
    }

    public void Reset()
    {
        cards.Clear();

        BuildCards();
        Shuffle();

        dealtCards = 0;
        NeedsShuffle = false;

        int lastQuarterStart = cards.Count * 3 / 4;

        cutCardPosition =
            random.Next(lastQuarterStart, cards.Count);
    }

    private void Shuffle()
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);

            Card temporaryCard = cards[i];

            cards[i] = cards[j];
            cards[j] = temporaryCard;
        }
    }

    public Card Deal()
    {
        if (cards.Count == 0)
        {
            throw new InvalidOperationException(
                "Det finns inga kort kvar i skon.");
        }

        if (dealtCards == cutCardPosition)
        {
            NeedsShuffle = true;
        }

        int lastIndex = cards.Count - 1;

        Card card = cards[lastIndex];

        cards.RemoveAt(lastIndex);
        dealtCards++;

        return card;
    }

    public int GetCount()
    {
        return cards.Count;
    }
}