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


    // StartRound: startar rundan
    public void StartRound()
    {
        Player.Hand.Cards.Clear();
        Dealer.Hand.Cards.Clear();
        
        DealCards();
        PlayerTurn();
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