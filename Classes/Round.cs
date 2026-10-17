using System;
using System.Collections.Generic;

public class Round
{
    public List<Player> Players { get; }
    public Dealer Dealer { get; }
    public Deck Deck { get; }
    public ConsoleUI UI { get; }
    public List<Outcome> Results { get; } = new List<Outcome>();

    public Round(List<Player> players, Dealer dealer, Deck deck, ConsoleUI consoleUI)
    {
        if (players == null)
            throw new ArgumentNullException(nameof(players));
        
        if (players.Count < 1 || players.Count > 5)
            throw new ArgumentException("Det måste finnas mellan 1 och 5 spelare.", nameof(players));

        Players = players;
        Dealer = dealer;
        Deck = deck;
        UI = consoleUI;
    }

    // StartRound: startar rundan
    public void StartRound()
    {
        UI.ShowMessage("DEBUG: StartRound() started");//Some debugg again to verifie
        foreach (Player player in Players)
        {
            player.Hand.Cards.Clear();
        }

        Dealer.StartNewRound();
        Results.Clear();
        UI.ShowMessage(
            $"Cards remaining before reset check: {Deck.GetCount()}");

        if (Deck.GetCount() < 15)
        {
            UI.ShowMessage("Reshuffling deck!");
            Deck.Reset();
            UI.ShowMessage($"Cards after reset: {Deck.GetCount()}");//to verfie we have it reseted
        }
    DealCards();

    foreach (Player player in Players)
        {
    UI.ShowPlayerHand(player);
    PlayerTurn(player);
        }

        DealerTurn();
        UI.ShowDealerHand(Dealer);

        foreach (Player player in Players)
        {
            Outcome result = CheckWinner(player);
            Results.Add(result);

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

    // DealCards: delar ut kort till spelare och givare
    private void DealCards()
    {
        foreach (Player player in Players)
        {
            player.Hand.Cards.Add(Deck.Deal());
        }

        Dealer.Hand.Cards.Add(Deck.Deal());

        foreach (Player player in Players)
        {
            player.Hand.Cards.Add(Deck.Deal());
        }

        Dealer.Hand.Cards.Add(Deck.Deal());
    }

    // PlayerTurn: hanterar spelarens tur
    private void PlayerTurn(Player player)
    {
        bool playing = true;

        while (playing && !player.Hand.IsBust && !player.Hand.IsBlackjack)
        {
            string input = UI.GetPlayerChoice().Trim().ToLower();

            if (input == "h")
            {
                PlayerHit(player);
            }
            else if (input == "s")
            {
                playing = false;
            }
            else
            {
                UI.ShowMessage("Ogiltigt val. Skriv H eller S.");
            }
        }
    }

    private void PlayerHit(Player player)
    {
        player.Hand.Cards.Add(Deck.Deal());
        UI.ShowPlayerHand(player); //Wanna show the new card rigth
    }

    // DealerTurn: Dealerns tur
    private void DealerTurn()
    {
        Dealer.RevealHoleCard();

        while (Dealer.ShouldHit() && !Dealer.Hand.IsBust)
        {
            Dealer.Hand.Cards.Add(Deck.Deal());
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
}
