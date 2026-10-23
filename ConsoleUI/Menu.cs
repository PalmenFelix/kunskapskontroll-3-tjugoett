class Menu
{
    public int ShowMenu()
    {
        while (true)
        {
        Console.WriteLine("   =====================");
        Console.WriteLine(" //                     \\\\");
        Console.WriteLine("||   Välkommen till 21   ||");
        Console.WriteLine(" \\\\                     //");
        Console.WriteLine("   =====================");
        Console.WriteLine();
        Console.WriteLine("1. Starta spelet");
        Console.WriteLine("2. Avsluta spelet");
        if(int.TryParse(Console.ReadLine(), out int choice)
        && choice >=1 && choice <= 2)
            {
                return choice;
            }
            Console.WriteLine("Ogiltigt val, skriv 1 eller 2!");
        }
    }
    
}

