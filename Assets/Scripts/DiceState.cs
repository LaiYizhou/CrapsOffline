

public class DiceState
{
    private int number1;
    public int Number1
    {
        get
        {
            return number1;
        }

        set
        {
            number1 = value;
        }
    }

    private int number2;
    public int Number2
    {
        get
        {
            return number2;
        }

        set
        {
            number2 = value;
        }
    }

    public int Sum
    {
        get
        {
            return number1 + number2;
        }
    }

    /// <summary>
    /// the Sum is 2, 3 or 12
    /// </summary>
    /// <returns></returns>
    public bool IsCraps()
    {
        return Sum == 2 || Sum == 3 || Sum == 12;
    }

    /// <summary>
    /// the Sum is 7 or 11 
    /// </summary>
    /// <returns></returns>
    public bool IsNatural()
    {
        return Sum == 7 || Sum == 11;
    }

    /// <summary>
    /// the Sum is 4, 5, 6, 8, 9 or 10
    /// </summary>
    /// <returns></returns>
    public bool IsPoint()
    {
        return !IsCraps() && !IsNatural();
    }

    public bool IsBigSix()
    {
        return Sum == 6;
    }

    public bool IsBigEight()
    {
        return Sum == 8;
    }

    public bool IsAnySeven()
    {
        return Sum == 7;
    }

    public bool IsAnyCraps()
    {
        return IsCraps();
    }

    public bool IsHard()
    {
        return Number1 == Number2;
    }

    public bool IsField()
    {
        return Sum == 2 || Sum == 3 || Sum == 4 || Sum == 9 || Sum == 10 || Sum == 11 || Sum == 12;
    }

    //public bool IsHard_2_2()
    //{
    //    return Number1 == 2 && Number2 == 2;
    //}

    //public bool IsHard_5_5()
    //{
    //    return Number1 == 5 && Number2 == 5;
    //}

    //public bool IsHard_3_3()
    //{
    //    return Number1 == 3 && Number2 == 3;
    //}

    //public bool IsHard_4_4()
    //{
    //    return Number1 == 4 && Number2 == 4;
    //}

    public bool IsHorn_1_2()
    {
        return (Number1 == 1 && Number2 == 2) || (Number1 == 2 && Number2 == 1);
    }

    public bool IsHorn_5_6()
    {
        return (Number1 == 5 && Number2 == 6) || (Number1 == 6 && Number2 == 5);
    }

    public bool IsHorn_1_1()
    {
        return Sum == 2;
    }

    public bool IsHorn_6_6()
    {
        return Sum == 12;
    }

    public DiceState(int number1, int number2)
    {
        this.Number1 = number1;
        this.Number2 = number2;
    }
}