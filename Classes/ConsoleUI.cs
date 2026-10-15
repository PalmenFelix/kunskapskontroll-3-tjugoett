class ConsoleUI
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
}
    public void ShowMenu()
    {
        Console.WriteLine("   =====================");
        Console.WriteLine(" //                     \\\\");
        Console.WriteLine("||   Välkommen till 21   ||");
        Console.WriteLine(" \\\\                     //");
        Console.WriteLine("   =====================");
        Console.WriteLine();
        Console.WriteLine("1. Starta spelet");
        Console.WriteLine("2. Regler");
        Console.WriteLine("3. Avsluta spelet");
    }
    
}
