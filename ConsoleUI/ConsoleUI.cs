
using System;

public class ConsoleUI
{
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

        int acesAsOne = player.Hand.AcesCountedAsOne;

        foreach (Card card in player.Hand.Cards)
        {
            int shown = card.Value;

            if (card.Rank == Rank.ace && acesAsOne > 0)
            {
                shown = 1;
                acesAsOne--;
            }

            Console.WriteLine(card.ToString(shown));
        }

        Console.WriteLine($"Total: {player.Hand.Value}");
    }

    public void ShowDealerHand(Dealer dealer)
    {
        Console.WriteLine("Givarens hand:");

        int acesAsOne = dealer.Hand.AcesCountedAsOne;

        for (int i = 0; i < dealer.Hand.Cards.Count; i++)
        {
            // Hide the dealer's second card until it is revealed.
            if (i == 1 && !dealer.HoleCardRevealed)
            {
                Console.WriteLine("[Dolt kort]");
                continue;
            }

            Card card = dealer.Hand.Cards[i];
            int shown = card.Value;

            if (card.Rank == Rank.ace && acesAsOne > 0)
            {
                shown = 1;
                acesAsOne--;
            }

            Console.WriteLine(card.ToString(shown));
        }

        // Do not reveal the dealer's total while the hole card is hidden.
        if (dealer.HoleCardRevealed)
        {
            Console.WriteLine($"Total: {dealer.Hand.Value}");
        }
        else if (dealer.Hand.Cards.Count > 0)
        {
            Console.WriteLine(
                $"Synligt värde: {dealer.Hand.Cards[0].Value}");
        }
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowChips(Player player)
    {
        Console.WriteLine($"{player.Name} har {player.Chips.Balance} marker.");
    }

    public int AskBet(Player player)
    {
        while (true)
        {
            Console.WriteLine($"Hur mycket vill du satsa? (1 - {player.Chips.Balance}): ");
            if (int.TryParse(Console.ReadLine(), out int bet) && bet > 0 && bet <= player.Chips.Balance)
            {
                return bet;
            }
            
            Console.WriteLine("Ogiltlig satsning");
        }
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
