public class HumanStrategy : IPlayerStrategy
{
    private ConsoleUI ui;

    public HumanStrategy(ConsoleUI ui)
    {
        this.ui=ui;
    }

    public Move MakeYourMove(Hand hand, Card dealersOpenCard)
    {
        while (true)
        {
            string playerSays = ui.GetPlayerChoice().Trim().ToLower();
            if(playerSays == "h")
            {
                return Move.Hit;
            }
            else if (playerSays == "s")
            {
                return Move.Stand;
            }
            else
            {
                ui.ShowMessage("Ogiltigt val. Skriv H eller S.");
            }
        }
    }
}