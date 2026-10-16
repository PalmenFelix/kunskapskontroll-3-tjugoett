class Game
{
    Random random = new Random();
    private Menu menu = new Menu();
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
    public void Start()
    {
        bool running = true;
        while (running)
        {
            switch (menu.ShowMenu())
            {
            case 1: PlayGame(); break;
            case 2: running = false; break; 
            }
        }
    }
    private void PlayGame()
    {
        SetUpPlayers();
        Deck deck = new Deck(random);
        deck.Shuffle();
        Dealer dealer = new Dealer();
        Round round = new Round(players, dealer, deck, ui);
        round.StartRound();
    }
}

