class Game
{
    Random random = new Random();
    private Menu menu = new Menu();
    private ConsoleUI UI = new ConsoleUI();
    private RuleSetReader ruleSetReader = new RuleSetReader();
    private RuleSet rules = new RuleSet();

    private GameStorage storage = new GameStorage();// neeed to call  on the method from gamestorage
    private List<Player> players = new();
    private List<Player> selectedPlayers = new List<Player>(); // a list of the selected players in the game
    public void SetUpPlayers()
    {
        selectedPlayers.Clear();

        int number = UI.AskNumberOfPlayers();

        for (int i = 1; i <= number; i++)
        {
            int fighter = UI.PickYourFighter(i);

            if (fighter == 1)
            {
                string name = UI.AskPlayerName(i);

                Player? player = players.FirstOrDefault(
                    p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (player != null && selectedPlayers.Contains(player))
                {
                    UI.ShowMessage("Den spelaren är redan med. Välj ett annat namn.");
                    i--;
                    continue;
                }

                if (player == null)
                {
                    player = new Player(name);
                    players.Add(player);
                }

                player.Strategy = new HumanStrategy(UI);
                selectedPlayers.Add(player);
            }
            else if (fighter == 2)
            {
                Player bot = new Player($"Dator {i} (enkel)");
                bot.Strategy = new SimpleStrategy();
                selectedPlayers.Add(bot);
            }
            else
            {
                Player bot = new Player($"Dator {i} (proffs)");
                bot.Strategy = new ProStrategy();
                selectedPlayers.Add(bot);
            }
        }
    }
    
    public void Start()
    {
        players = storage.Load();
        rules = ruleSetReader.Load();

        foreach (string warning in ruleSetReader.Warnings)
        {
            UI.ShowMessage(warning);
        }
        UI.ShowRules(rules);

if (!string.IsNullOrEmpty(storage.ErrorMessage))
        {
            UI.ShowMessage(storage.ErrorMessage);
        }            
        bool running = true;
        
        while (running)
        {
            switch (menu.ShowMenu())
            {
                case 1:
                    PlayGame(); 
                    break;
                case 2: 
                    running = false; 
                    break; 
            }
        }
    }

private void PlayGame()
    {
    SetUpPlayers();

        Deck deck = new Deck(random, rules.NumberOfDecks);
        deck.Shuffle();

        Dealer dealer = new Dealer(rules.DealerHitsSoft17);

    // Keep playing rounds until the player chooses to stop.
    bool playing = true;

    while (playing)
        {
        Round round = new Round(selectedPlayers, dealer, deck, UI, rules);
        round.StartRound();

        UI.ShowMessage("Vill du spela en runda till? (j/n)");
        string answer = Console.ReadLine()?.Trim().ToLower() ?? "";

        if (answer != "j")
        {
            playing = false;
        }
        }

    if (!storage.Save(players))
        {
        UI.ShowMessage(storage.ErrorMessage);
        }
    }
}

