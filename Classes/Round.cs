public class Round
{
    public Player Player { get; }
    public Dealer Dealer { get; }
    public Deck Deck { get; }
    public Outcome? Result { get; private set; }

    public Round(Player player, Dealer dealer, Deck deck)
    {
        Player = player;
        Dealer = dealer;
        Deck = deck;
    }


    //Start Round: startar rundan
    public void StartRound()
    {
        DealCards();
        PlayersTurns();
        DealerTurn();
        Result = CheckWinner();
    }

    // DealCards: delar ut kort till spelare och givare
    private void DealCards()
    {
        Player.Hand.Cards.Add(Deck.Deal());
        Dealer.Hand.Cards.Add(Deck.Deal());
        Player.Hand.Cards.Add(Deck.Deal());
        Dealer.Hand.Cards.Add(Deck.Deal());
    }

    // PlayerTurn: hanterar spelarens tur
    private void PlayerTurn()
    {
        bool playing = true;

        while (playing && !Player.Hand.IsBust && !Player.Hand.IsBlackjack)
        {
            string? input = // Från ConsoleUI

            if (input == // Från ConsoleUI)
            {
                PlayerHit();
            }
            else if (input == // Från ConsoleUI)
            {
                PlayerStand();
                playing = false;
            }
            else
            {
                // Ogiltigt val ska frågas igen, från ConsoleUI
            }
        }
    }   

    private void PlayerHit()
        {
            Player.Hand.Cards.Add(Deck.Deal());
        }

    private void PlayerStand()
        {
            // Körs i PlayerTurn
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
    private Outcome CheckWinner()
    {
        if (Player.Hand.IsBust)
        {
            return Outcome.Lose;
        }
        else if (Dealer.Hand.IsBust)
        {
            return Outcome.Win;
        }
        else if (Player.Hand.IsBlackjack && Dealer.Hand.IsBlackjack)
        {
            return Outcome.Push;
        }
        else if (Player.Hand.IsBlackjack)
        {
            return Outcome.Blackjack;
        }
        else if (Dealer.Hand.IsBlackjack)
        {
            return Outcome.Lose;
        }
        else if (Player.Hand.Value > Dealer.Hand.Value)
        {
            return Outcome.Win;
        }
        else if (Player.Hand.Value < Dealer.Hand.Value)
        {
            return Outcome.Lose;
        }
        else
        {
            return Outcome.Push;
        }
    }
}