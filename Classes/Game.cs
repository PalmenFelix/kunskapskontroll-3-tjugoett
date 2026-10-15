class Game
{
    private ConsoleUI ui = new ConsoleUI();
    private List<Player> players = new();
    public void SetUpPlayers()
    {
        int number = ui.AskNumberOfPlayers();
        for (int i = 1; i <= number; i++)
        {
            string name = ui.AskPlayerName(i);
            Player player = new Player(name);
            players.Add(player);
        }
    }
}