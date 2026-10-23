using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Linq;

public class Round
{
    public List<Player> Players { get; }
    public Dealer Dealer { get; }
    public Shoe Shoe { get; }
    public CardCounter Counter { get; }
    public ConsoleUI UI { get; }
    public RuleSet Rules { get; }
    public List<Outcome> Results { get; } = new List<Outcome>();
    public Dictionary<Player, int> Bets { get; } = new();

    public Round(List<Player> players, Dealer dealer, Shoe shoe, CardCounter counter, ConsoleUI consoleUI)
    {
        if (players == null)
            throw new ArgumentNullException(nameof(players));
        
        if (players.Count < 1 || players.Count > 5)
            throw new ArgumentException("Det måste finnas mellan 1 och 5 spelare.", nameof(players));

        Players = players;
        Dealer = dealer;
        Shoe = shoe;
        Counter = counter;
        UI = consoleUI;
        Rules = new RuleSet();
    }

    // StartRound: startar rundan
    public void StartRound()
    {
        foreach (Player player in Players.ToList())
        {
            if (player.Chips.Balance <= 0) // Spelare med 0 marker eller mindre får inte delta i nästa runda
                {
                    UI.PlayerRemoved(player);
                    Players.Remove(player);
                }       
        }

        if (Players.Count == 0) // Avsluta rundan om alla spelare har slut på marker
            {
                UI.ShowMessage("Alla spelare har slut på marker. Spelet avslutas.");
                return;
            }
        
        foreach (Player player in Players)
        {
            player.Hand.Cards.Clear();
        }

        Dealer.StartNewRound();
        Results.Clear();

        if (Shoe.NeedsShuffle)
        {
            Shoe.Reset();
            Counter.Reset();

            UI.ShowMessage(
                "Skiljekortet har nåtts. Skon blandas om.");
        }


        PlaceBets(); // Tillkallar metoden PlaceBets()

        DealCards();
     

       
        foreach (Player player in Players)
        {
            UI.ShowDealerHand(Dealer);
            UI.ShowPlayerHand(player);

            if (player.Hand.IsBlackjack)
            {
                UI.ShowBlackjackMessage(player);
            }

            PlayerTurn(player);
        }

        DealerTurn();
        UI.ShowDealerHand(Dealer); // need the UI to get the dealer cards
        UI.ShowCardCount(Counter, Shoe.GetCount());

        if (Dealer.Hand.IsBlackjack)
        {
            UI.ShowDealerBlackjackMessage();
        }
        foreach (Player player in Players)
        {
            Outcome result = CheckWinner(player);  // Beräkna resultatet och betalar ut eventuell vinst eller psuh
            Results.Add(result);
            PayOut(player, result);

            switch (result)
            {
                case Outcome.Win:
                case Outcome.Blackjack:
                    player.Wins++;
                    break;

                case Outcome.Lose:
                    player.Losses++;
                    break;

                case Outcome.Push:
                    player.Pushes++;
                    break;
            }
            UI.ShowMessage($"{player.Name}: {result}");
        }
    }

    private Card DealVisibleCard()
    {
        Card card = Shoe.Deal();

        Counter.Observe(card);

        return card;
    }

    // DealCards: delar ut kort till spelare och givare
    private void DealCards()
    {
        foreach (Player player in Players)
        {
            player.Hand.Cards.Add(DealVisibleCard());
        }

        Dealer.Hand.Cards.Add(DealVisibleCard());

        foreach (Player player in Players)
        {
            player.Hand.Cards.Add(DealVisibleCard());
        }

        Dealer.Hand.Cards.Add(Shoe.Deal());
    }

    // PlayerTurn: hanterar spelarens tur
    private void PlayerTurn(Player player)
    {
        bool playing = true;

        while (playing && !player.Hand.IsBust && !player.Hand.IsBlackjack)
        {
            Move move = player.Strategy.MakeYourMove(player.Hand, Dealer.Hand.Cards[0]);

            if (move == Move.Hit)
            {
                PlayerHit(player);
            }
            else
            {
                playing = false;
            }
        }
        if (player.Hand.IsBust)
        {
            UI.ShowMessage($"{player.Name} blev tjock!");
        }
    }

    private void PlayerHit(Player player)
    {
        player.Hand.Cards.Add(DealVisibleCard());

        UI.ShowPlayerHand(player); //Wanna show the new card rigth
    }

    // DealerTurn: Dealerns tur
    private void DealerTurn()
    {
        Dealer.RevealHoleCard();
        Card holeCard = Dealer.Hand.Cards[1];
        Counter.Observe(holeCard);

        while (Dealer.ShouldHit() && !Dealer.Hand.IsBust)
        {
            Dealer.Hand.Cards.Add(DealVisibleCard());
        }
        if (Dealer.Hand.IsBust)
        {
            UI.ShowMessage($"{Dealer.Name} blev tjock!");
        }
    }

    // CheckWinner: kollar vem som vinner rundan
    private Outcome CheckWinner(Player player)
    {
        if (player.Hand.IsBust)
        {
            return Outcome.Lose;
        }
        else if (Dealer.Hand.IsBust)
        {
            return player.Hand.IsBlackjack ? Outcome.Blackjack : Outcome.Win;
        }
        else if (player.Hand.IsBlackjack && Dealer.Hand.IsBlackjack)
        {
            return Outcome.Push;
        }
        else if (!Rules.NaturalBeatsMultiCard21 && player.Hand.Value == 21 && Dealer.Hand.Value == 21)
        {
            return Outcome.Push;
        }
        else if (player.Hand.IsBlackjack)
        {
            return Outcome.Blackjack;
        }
        else if (Dealer.Hand.IsBlackjack)
        {
            return Outcome.Lose;
        }
        else if (player.Hand.Value > Dealer.Hand.Value)
        {
            return Outcome.Win;
        }
        else if (player.Hand.Value < Dealer.Hand.Value)
        {
            return Outcome.Lose;
        }
        else
        {
            return Outcome.Push;
        }
    }

    // Feature-4: Betting chips

    public void PlaceBets()
    {
        Bets.Clear(); // Rensa föregående rundas insatser

        foreach (Player player in Players)
        {
            UI.ShowChips(player); // Visa spelarens saldo innan insatsen väljs

            int bet = UI.AskBet(player); // Frågar hur mycket spelaren vill satsa

            if (player.Chips.RemoveChips(bet)) // Dra insatsen från saldot och spara den om satsningen var giltig
            {
                Bets[player] = bet; 
            }
        }
    }

    public void PayOut(Player player, Outcome result) // Betalar ut marker beroende på resultatet och spelarens insats
    {
        int bet = Bets[player]; // Vad spelaren satstade
        
        if (result == Outcome.Win) // Vanlig vinst: betalar 1:1
        {
            player.Chips.AddChips(bet * 2);
        }
        else if (result == Outcome.Push) // Push: Får tillbaka markerna man satsade
        {
            player.Chips.AddChips(bet);
        }
        else if (result == Outcome.Blackjack) // Blackjack: betalar 2:3
        {
            player.Chips.AddChips(bet + (bet * 3 / 2));
        }
    }
}
