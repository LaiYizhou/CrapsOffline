using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Poker
{
    public EPokerColor Color;
    public EPokerNumber Number;

    public Poker()
    {

    }

    public Poker(EPokerColor color, EPokerNumber number)
    {
        this.Color = color;
        this.Number = number;
    }

    public override string ToString()
    {
        return string.Format("{0}{1}", Color.ToString(), Number.ToString());
    }

    public static bool operator ==(Poker p1, Poker p2)
    {
        return (p1.Color == p2.Color) && (p1.Number == p2.Number);
    }

    public static bool operator !=(Poker p1, Poker p2)
    {
        return (p1.Color != p2.Color) || (p1.Number != p2.Number);
    }

    public static bool operator >(Poker p1, Poker p2)
    {
        return p1.Number > p2.Number;
    }

    public static bool operator <(Poker p1, Poker p2)
    {
        return p1.Number < p2.Number;
    }


}

public enum EPokerColor
{
    Heart,
    Spade,
    Diamond,
    Club
}

public enum EPokerNumber
{
    _A = 1,
    _2,
    _3,
    _4,
    _5,
    _6,
    _7,
    _8,
    _9,
    _10,
    _J,
    _Q,
    _K
}


