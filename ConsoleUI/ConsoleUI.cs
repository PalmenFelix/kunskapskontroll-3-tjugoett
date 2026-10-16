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

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}
    
