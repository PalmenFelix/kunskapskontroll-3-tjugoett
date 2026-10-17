class Game
{
    Random random = new Random();
    private Menu menu = new Menu();
    private ConsoleUI UI = new ConsoleUI();

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
            //Player player = new Player(name); would create another player , even tho im currently the same player so my stats would zero.
            //So  the load wouldn't get to load anything
            Player? player = players.FirstOrDefault(
            p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        

    if (player == null) // 6 . an existing player isnt added to players again
        {
            player = new Player(name);
            players.Add(player);
        }
        
        selectedPlayers.Add(player);
        Console.WriteLine($"DEBUG: Selected players = {selectedPlayers.Count}");
        }
    }
public void Start()
    {
        players = storage.Load();

if (!string.IsNullOrEmpty(storage.ErrorMessage))
        {
            UI.ShowMessage(storage.ErrorMessage);
        }            //players = storage.Load(); // Needs to have a Load method that stores the data to storage 
                    //ui.ShowMessage(storage.ErrorMessage);
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
// private void PlayGame()
//     {
//         SetUpPlayers();
//         Deck deck = new Deck(random);
//         deck.Shuffle();
//         Dealer dealer = new Dealer();
//         //Round round = new Round(players, dealer, deck, ui); contains all saved profiles, 
//         // whereas selectedPlayers contains only the players chosen for this game.
//         Round round = new Round(selectedPlayers, dealer, deck, ui);
//         round.StartRound();
//     if (!storage.Save(players))
//         {
//             ui.ShowMessage(storage.ErrorMessage);
//         }
//     }

private void PlayGame()
{
    SetUpPlayers();

    Deck deck = new Deck(random);
    deck.Shuffle();

    Dealer dealer = new Dealer();

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

