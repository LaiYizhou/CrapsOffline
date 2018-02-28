

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

    public DiceState(int number1, int number2)
    {
        this.Number1 = number1;
        this.Number2 = number2;
    }
}