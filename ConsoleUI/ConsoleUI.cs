public class ConsoleUI
{
    public int AskNumberOfPlayers()
    {
        while(true)
        {
            Console.WriteLine("Hur många spelare? (1-5)");
            if (int.TryParse(Console.ReadLine(), out int numberOfPlayers) 
            && numberOfPlayers >= 1 && numberOfPlayers <= 5)
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
public void ShowPlayerHand(Player player) //Need to show our card in our hand so we know if hit or stand is the correct way 
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
        Console.WriteLine($"Total: {player.Hand.Value}"); // Yeah added the sum of all the card value
    }
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
    public void ShowDealerHand(Dealer dealer)
{
    Console.WriteLine("Givarens hand:");

    int acesAsOne = dealer.Hand.AcesCountedAsOne;

    foreach (Card card in dealer.Hand.Cards)
    {
        int shown = card.Value;
        if (card.Rank == Rank.ace && acesAsOne > 0)
        {
            shown = 1;
            acesAsOne--;
        }
        Console.WriteLine(card.ToString(shown));
    }

    Console.WriteLine($"Total: {dealer.Hand.Value}");
}
}

    
