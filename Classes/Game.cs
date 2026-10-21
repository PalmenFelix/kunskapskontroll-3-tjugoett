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
        selectedPlayers.Clear(); // 8. need to clear the selected players before joining a new game 
        // otherwise it add the same players.
        int number = UI.AskNumberOfPlayers();
        for (int i = 1; i <= number; i++)
        {
            string name =UI.AskPlayerName(i);
            
            Player? player = players.FirstOrDefault(
            p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        

    if (player == null) // 6 . an existing player isnt added to players again
        {
            player = new Player(name);
            players.Add(player);
        }
        
        selectedPlayers.Add(player);
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

        Deck deck = new Deck(random);
        deck.Shuffle();

        Dealer dealer = new Dealer(rules.DealerHitsSoft17);

    // Keep playing rounds until the player chooses to stop.
    bool playing = true;

    while (playing)
        {
        Round round = new Round(selectedPlayers, dealer, deck, UI);
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

