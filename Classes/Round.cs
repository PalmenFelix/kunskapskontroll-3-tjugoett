using System;
using System.Collections.Generic;

public class Round
{
    public List<Player> Players { get; }
    public Dealer Dealer { get; }
    public Deck Deck { get; }
    public ConsoleUI ConsoleUI { get; }
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
        ConsoleUI = consoleUI;
    }

    // StartRound: startar rundan
    public void StartRound()
    {
        foreach (Player player in Players)
        {
            player.Hand.Cards.Clear();
        }

        Dealer.StartNewRound();
        Results.Clear();

        if (Deck.GetCount() < 15)
        {
            Deck.Reset();
        }

        DealCards();

        foreach (Player player in Players)
        {
            PlayerTurn(player);
        }

        DealerTurn();

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
            string input = ConsoleUI.GetPlayerChoice().Trim().ToLower();

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
                ConsoleUI.ShowMessage("Ogiltigt val. Skriv H eller S.");
            }
        }
    }

    private void PlayerHit(Player player)
    {
        player.Hand.Cards.Add(Deck.Deal());
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

    // Feature-4: Betting chips

    public void PlaceBets()
    {

        foreach (Player player in Players)
        {
            ConsoleUI.ShowChips(player);

            int bet = ConsoleUI.AskBet(player);

            player.Chips.RemoveChips(bet);
        }
    }
}
