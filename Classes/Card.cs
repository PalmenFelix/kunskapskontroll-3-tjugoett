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

}
