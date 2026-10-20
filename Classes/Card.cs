using System;

public class Card 
{
    public Suit Suit;
    public Rank Rank;

    public int Value 
    {
        get
        {
            switch(Rank)
            {
                case Rank.two:
                    return 2;
                case Rank.three:
                    return 3;
                case Rank.four:
                    return 4;
                case Rank.five:
                    return 5;
                case Rank.six:
                    return 6;
                case Rank.seven:
                    return 7;
                case Rank.eight:
                    return 8;
                case Rank.nine:
                    return 9;
                case Rank.ten:
                case Rank.jack:
                case Rank.queen:
                case Rank.king:
                    return 10;
                case Rank.ace:
                    return 11;

                default:
                throw new ArgumentOutOfRangeException();
            }
        }
    }

//public override string ToString() // We wanna display the cards also otherwise our Ui wouldn't write anything
    // {
    // return $"{Rank} of {Suit}";
    // }
    public string RankName => Rank switch
    {
        Rank.two => "2",
        Rank.three => "3",
        Rank.four => "4",
        Rank.five => "5",
        Rank.six => "6",
        Rank.seven => "7",
        Rank.eight => "8",
        Rank.nine => "9",
        Rank.ten => "10",
        Rank.jack => "Jack",
        Rank.queen => "Queen",
        Rank.king => "King",
        Rank.ace => "Ace",
        _ => Rank.ToString()
    };

    public override string ToString() => ToString(Value);

    public string ToString(int shownValue) => $"{RankName} of {Suit} ({shownValue})";
}
