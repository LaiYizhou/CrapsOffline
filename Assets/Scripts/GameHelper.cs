﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHelper : MonoBehaviour
{

    public static GameHelper Instance;

    [SerializeField] private List<Sprite> diceSpriteList;
    [SerializeField] private List<Sprite> chipSpriteList;
    [SerializeField] private List<Sprite> chipDarkSpriteList;

    public static Vector3 ChipOnDragPosOffset = new Vector3(0.0f, 10.0f, 0.0f);
    public static Vector3 ChipOnDragScale = new Vector3(0.2f, 0.2f, 0.2f);
    public static long StartCoins = 1000000L;

    private List<long> chipValueList = new List<long>()
    {   10L, 100L, 200L, 500L,
        1000L, 2000L, 5000L, 10000L, 20000L, 25000L, 50000L, 100000L, 250000L, 500000L,
        1000000L, 1250000L, 2000000L, 2500000L, 5000000L, 10000000L, 12500000L, 25000000L, 50000000L, 100000000L
    };

    /// <summary>
    /// Key: level (Start with 1)
    /// </summary>
    private Dictionary<int, CrapSceneInfo> crapSceneInfoDictionary = new Dictionary<int, CrapSceneInfo>()
        {
            {1, new CrapSceneInfo(1, 100L, 100*100L, 100*500L, new List<EChip>(){EChip._100, EChip._1K, EChip._2K, EChip._5K, EChip._10K})},
            {2, new CrapSceneInfo(2, 1000L, 1000*100L, 1000*500L, new List<EChip>(){EChip._1K, EChip._2K, EChip._5K, EChip._10K, EChip._50K})},
            {3, new CrapSceneInfo(3, 10000L, 10000*100L, 10000*500L, new List<EChip>(){EChip._10K, EChip._20K, EChip._50K, EChip._100K, EChip._500K})},
            {4, new CrapSceneInfo(4, 50000L, 50000*100L, 50000*500L, new List<EChip>(){EChip._50K, EChip._100K, EChip._250K, EChip._500K, EChip._2_5M})},
            {5, new CrapSceneInfo(5, 250000L, 250000*100L, 250000*500L, new List<EChip>(){EChip._250K, EChip._500K, EChip._1_25M, EChip._2_5M, EChip._12_5M})},
            {6, new CrapSceneInfo(6, 1000000L, 1000000*100L, 1000000*500L, new List<EChip>(){EChip._1M, EChip._2M, EChip._5M, EChip._10M, EChip._50M})},
        };

    private Dictionary<EGameStage, List<EArea>> validEAreaDictionary = new Dictionary<EGameStage, List<EArea>>()
        {
            {EGameStage.ComeOut, new List<EArea>(){EArea.PassLine, EArea.DontPassH, EArea.DontPassV} },
        };

    public static Player player;

    // Use this for initialization
    void Start ()
	{
	    Instance = this;

	    player = new Player();

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

    public CrapSceneInfo GetCrapSceneInfo(int index)
    {
        if (index > 0 && index <= crapSceneInfoDictionary.Count)
            return crapSceneInfoDictionary[index];
        else
            return null;
    }

    public List<EArea> GetValidAreaList(EGameStage eGameStage)
    {
        if (validEAreaDictionary.ContainsKey(eGameStage))
            return validEAreaDictionary[eGameStage];
        else
            return null;
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

    public Sprite GetChipSprite(EChip eChip)
    {
        if ((int) eChip >= 0 && (int) eChip < (int) EChip.Count)
        {
            return chipSpriteList[(int) eChip];
        }
        else
        {
            return null;
        }

    }

    public Sprite GetChipDarkSprite(EChip eChip)
    {
        if ((int)eChip >= 0 && (int)eChip < (int)EChip.Count)
        {
            return chipDarkSpriteList[(int)eChip];
        }
        else
        {
            return null;
        }
    }

    public long GetChipValue(EChip eChip)
    {
        if ((int)eChip >= 0 && (int)eChip < (int)EChip.Count)
        {
            return chipValueList[(int)eChip];
        }
        else
        {
            return -1;
        }
    }

}

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
            return number1+number2;
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

public class CrapSceneInfo
{
    public int level;

    public long minLine;

    public long maxBet;
    public long tableLimit;

    public List<EChip> CandiChipList;

    public CrapSceneInfo(int level, long minLine, long maxBet, long tableLimit, List<EChip> candiChipList)
    {
        this.level = level;

        this.minLine = minLine;
        this.maxBet = maxBet;
        this.tableLimit = tableLimit;

        this.CandiChipList = candiChipList;

    }

    public CrapSceneInfo()
    {

    }

}
