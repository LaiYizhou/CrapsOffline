using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHelper : MonoBehaviour
{

    public static GameHelper Instance;

    [SerializeField]
    private List<Sprite> diceSpriteList;

	// Use this for initialization
	void Start ()
	{
	    Instance = this;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public DiceState RandomDice()
    {
        int number1 = Random.Range(1, 7);
        int number2 = Random.Range(1, 7);

        return new DiceState(number1, number2);

    }


    /// <summary>
    /// ! ! !Attention: diceSpriteList.Count == 7, and diceSpriteList[0] is null
    /// So, the DiceNumber can match the Index
    /// </summary>
    /// <param name="number">the DiceNumber (can match the Index) </param>
    /// <returns></returns>
    public Sprite GetDiceSprite(int number)
    {
        if (number >= 1 && number <= 6)
        {
            return diceSpriteList[number];
        }
        else
        {
            return null;
        }
    }
}

public class DiceState
{
    private int number1;
    private int number2;

    public int GetNumber1()
    {
        return number1;
    }

    public int GetNumber2()
    {
        return number2;
    }

    public DiceState(int number1, int number2)
    {
        this.number1 = number1;
        this.number2 = number2;
    }



}
