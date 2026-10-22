using System.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
class Game
{
    private Random random = new Random();
    private Menu menu = new Menu();
    private ConsoleUI UI = new ConsoleUI();
    private RuleSetReader ruleSetReader = new RuleSetReader();
    private RuleSet rules = new RuleSet();

    private GameStorage storage = new GameStorage();// neeed to call  on the method from gamestorage
    private List<Player> players = new List<Player>();
    private List<Player> selectedPlayers = new List<Player>(); // a list of the selected players in the game
    public void SetUpPlayers()
    {
        selectedPlayers.Clear();

        int number = UI.AskNumberOfPlayers();

        for (int i = 1; i <= number; i++)
        {
            string name = UI.AskPlayerName(i);
            int fighter = UI.PickYourFighter(i);

            if (fighter == 1)
            {
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
                else if (player.Chips.Balance == 0)
                {
                    player.Chips.SaveChips(1000);
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
        players = storage.Load(); // Laddar sparade spelare och deras chips
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
            return;
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

        Shoe shoe = new Shoe(4, random);

        CardCounter counter = new CardCounter();

        Dealer dealer = new Dealer(rules.DealerHitsSoft17);

    // Keep playing rounds until the player chooses to stop.
    bool playing = true;

    while (playing)
        {
        Round round = new Round(selectedPlayers, dealer, shoe, counter, UI);
        
        round.StartRound();

        if (selectedPlayers.Count == 0) // Avslutar spelsessionen om det inte finns några spelare
        {
            playing = false;
            break;
        } 

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
    } // Sparar spelarnas uppgifter och deras antal chips
}

