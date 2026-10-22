

public class ConsoleUI
{
    private readonly CardRenderer cardRenderer = new CardRenderer();

    public int AskNumberOfPlayers()
    {
        while (true)
        {
            Console.WriteLine("Hur många spelare? (1-5)");

            if (int.TryParse(Console.ReadLine(), out int numberOfPlayers)
                && numberOfPlayers >= 1
                && numberOfPlayers <= 5)
            {
                return numberOfPlayers;
            }

            Console.WriteLine("Skriv in ett nummer mellan 1 och 5");
        }
    }

    public string AskPlayerName(int number)
    {
        while (true)
        {
            Console.WriteLine($"Döp spelare {number}:");
            string? playerName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(playerName))
            {
                Console.WriteLine("Spelarens namn får inte vara tomt");
            }
            else
            {
                return playerName;
            }
        }
    }

    public int PickYourFighter(int number)
    {
        while (true)
        {
            Console.WriteLine($"Spelare {number}:");
            Console.WriteLine("1. Människa");
            Console.WriteLine("2. Dator (enkel)");
            Console.WriteLine("3. Dator (proffs)");

            if (int.TryParse(Console.ReadLine(), out int choice)
                && choice >= 1
                && choice <= 3)
            {
                return choice;
            }

            Console.WriteLine("Skriv 1, 2 eller 3");
        }
    }

    public string GetPlayerChoice()
    {
        Console.WriteLine("H. Hit (dra kort)");
        Console.WriteLine("S. Stand (stanna)");
        Console.Write("Välj: ");

        return Console.ReadLine() ?? "";
    }

    public void ShowPlayerHand(Player player)
    {
        Console.WriteLine($"{player.Name}'s hand:");

        // Keep the ace logic: an ace can count as 1 or 11.
        int acesAsOne = player.Hand.AcesCountedAsOne;

        List<string[]> renderedCards = new List<string[]>();

        foreach (Card card in player.Hand.Cards)
        {
            // The card graphic shows A; Hand.Value calculates 1 or 11.
            renderedCards.Add(cardRenderer.RenderCard(card));

            if (card.Rank == Rank.ace && acesAsOne > 0)
            {
                // This ace is counted as 1 in the hand total.
                acesAsOne--;
            }
        }

        RenderCardsSideBySide(renderedCards);

        Console.WriteLine($"Total: {player.Hand.Value}");
    }

    public void ShowDealerHand(Dealer dealer)
    {
        Console.WriteLine("Givarens hand:");

        int acesAsOne = dealer.Hand.AcesCountedAsOne;

        List<string[]> renderedCards = new List<string[]>();

        for (int i = 0; i < dealer.Hand.Cards.Count; i++)
        {
            // Hide the dealer's second card until it is revealed.
            if (i == 1 && !dealer.HoleCardRevealed)
            {
                renderedCards.Add(cardRenderer.RenderCardBack());
                continue;
            }

            Card card = dealer.Hand.Cards[i];

            renderedCards.Add(cardRenderer.RenderCard(card));

            if (card.Rank == Rank.ace && acesAsOne > 0)
            {
                // This ace is counted as 1 in the hand total.
                acesAsOne--;
            }
        }

         RenderCardsSideBySide(renderedCards);

         // Do not reveal the dealer's total while the hole card is hidden.
         if (dealer.HoleCardRevealed)
         {
             Console.WriteLine($"Total: {dealer.Hand.Value}");
        }
        else if (dealer.Hand.Cards.Count > 0)
        {
            Console.WriteLine($"Synligt värde: {dealer.Hand.Cards[0].Value}");
        }
    }

    private void RenderCardsSideBySide(List<string[]> renderedCards)
    {
        if (renderedCards.Count == 0)
        {
            Console.WriteLine("(Inga kort)");
            return;
        }

        for (int row = 0; row < renderedCards[0].Length; row++)
        {
            foreach (string[] card in renderedCards)
            {
                Console.Write(card[row] + " ");
            }

            Console.WriteLine();
        }
    }

     public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowRules(RuleSet rules)
    {
        Console.WriteLine("Husregler:");
        Console.WriteLine(rules.DealerHitsSoft17
            ? "- Givaren köper på mjuk 17"
            : "- Givaren stannar på mjuk 17");
        Console.WriteLine($"- Antal lekar: {rules.NumberOfDecks}");
        Console.WriteLine($"- Korten blandas om när det är färre än {rules.ReshuffleThreshold} kort kvar");
        Console.WriteLine(rules.NaturalBeatsMultiCard21
            ? "- Naturlig 21 slår 21 med tre eller fler kort"
            : "- Naturlig 21 mot 21 med tre eller fler kort blir lika");
        Console.WriteLine();
    }
    
    public void ShowBlackjackMessage(Player player)
    {
        Console.WriteLine($"{player.Name} fick blackjack");
    }

    public void ShowDealerBlackjackMessage()
    {
        Console.WriteLine($"Dealern har blackjack :(");
    }
}
    

