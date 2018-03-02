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
    public static Vector3 CrapsPointOriginalPos = new Vector3(277.0f, 130.0f, 0.0f);
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

    /// <summary>
    /// Odds -- Returned Payment
    /// zero: it's Odds can't be calculated only according the EArea, should calculate it Both AREA and DICE
    /// pos: it is Odds
    /// neg: it is Odds and should remove 5% commission
    /// </summary>
    private Dictionary<EArea, float> areaOddsDictionary = new Dictionary<EArea, float>()
    {
        { EArea.PassLine, 1.0f},
        { EArea.PassOdds, 0.0f}, //zero

        { EArea.BigSix, 1.0f},
        { EArea.BigEight, 1.0f},

        { EArea.DontPassH, 1.0f},
        { EArea.DontPassOdds, 0.0f}, //zero
        { EArea.DontPassV, 1.0f},

        { EArea.Field, 0.0f},

        { EArea.Come, 1.0f},
        { EArea.DontCome, 1.0f},

        { EArea.Buy4, -2.0f},
        { EArea.Buy5, -1.5f},
        { EArea.Buy6, -1.2f},
        { EArea.Buy8, -1.2f},
        { EArea.Buy9, -1.5f},
        { EArea.Buy10, -2.0f},

        { EArea.Lay4, -1.0f/2.0f},
        { EArea.Lay5, -2.0f/3.0f},
        { EArea.Lay6, -5.0f/6.0f},
        { EArea.Lay8, -5.0f/6.0f},
        { EArea.Lay9, -2.0f/3.0f},
        { EArea.Lay10, -1.0f/2.0f},

        { EArea.PlaceLose4, 5.0f/11.0f},
        { EArea.PlaceLose5, 5.0f/8.0f},
        { EArea.PlaceLose6, 4.0f/5.0f},
        { EArea.PlaceLose8, 4.0f/5.0f},
        { EArea.PlaceLose9, 5.0f/8.0f},
        { EArea.PlaceLose10, 5.0f/11.0f},


        { EArea.DontComeOdds4, -1.0f/2.0f},
        { EArea.DontComeOdds5, -2.0f/3.0f},
        { EArea.DontComeOdds6, -5.0f/6.0f},
        { EArea.DontComeOdds8, -5.0f/6.0f},
        { EArea.DontComeOdds9, -2.0f/3.0f},
        { EArea.DontComeOdds10, -1.0f/2.0f},


        { EArea.ComeOdds4, -2.0f},
        { EArea.ComeOdds5, -1.5f},
        { EArea.ComeOdds6, -1.2f},
        { EArea.ComeOdds8, -1.2f},
        { EArea.ComeOdds9, -1.5f},
        { EArea.ComeOdds10, -2.0f},


        { EArea.PlaceWin4, 1.8f},
        { EArea.PlaceWin5, 1.4f},
        { EArea.PlaceWin6, 7.0f/6.0f},
        { EArea.PlaceWin8, 7.0f/6.0f},
        { EArea.PlaceWin9, 1.4f},
        { EArea.PlaceWin10, 1.8f},

        { EArea.AnySeven, 4.0f},

        { EArea.Hard22, 7.0f},
        { EArea.Hard55, 7.0f},
        { EArea.Hard33, 9.0f},
        { EArea.Hard44, 9.0f},

        { EArea.Horn12, 30.0f},
        { EArea.Horn56, 15.0f},
        { EArea.Horn11, 15.0f},
        { EArea.Horn66, 30.0f},

        { EArea.AnyCraps, 7.0f},



    };

    public static Player player;

    // Use this for initialization
    void Start ()
	{
	    Instance = this;

	    player = new Player();

        //Debug.Log(basicPassAreaList.Count+basicComeAreaList.Count+allComeOddsAreaList.Count+allDontComeOddsAreaList.Count+multiRollAreaList.Count+singleRollAreaList.Count);

	    //Debug.Log(areaOddsDictionary.Count);

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

    public long GetOdds(Chip chip, EArea eArea, DiceState diceState)
    {
        if (areaOddsDictionary.ContainsKey(eArea))
        {
            float value = areaOddsDictionary[eArea];

            if (value > 0)
            {
                return (long)((value + 1) * chip.Value);
            }
            else if (value < 0)
            {
                return (long) ((-value + 1) * chip.Value - chip.Value * 0.05f);
            }
            else
            {
                if (eArea == EArea.Field)
                {
                    if (diceState.Sum == 3 || diceState.Sum == 4 || diceState.Sum == 9 || diceState.Sum == 10 ||
                        diceState.Sum == 11)
                        return 2L * chip.Value;
                    else if (diceState.Sum == 2 || diceState.Sum == 12)
                        return 3L * chip.Value;
                }
                else
                {
                    return 0L;
                }
            }

        }

        return 0L;
    }

    public CrapSceneInfo GetCrapSceneInfo(int index)
    {
        if (index > 0 && index <= crapSceneInfoDictionary.Count)
            return crapSceneInfoDictionary[index];
        else
            return null;
    }

    private List<EArea> basicPassAreaList = new List<EArea>(){ EArea.PassLine, EArea.DontPassH, EArea.DontPassV };

    private List<EArea> basicComeAreaList = new List<EArea>(){EArea.Come, EArea.DontCome};

    private List<EArea> allComeOddsAreaList = new List<EArea>()
    {
        EArea.ComeOdds4, EArea.ComeOdds5, EArea.ComeOdds6, EArea.ComeOdds8, EArea.ComeOdds9, EArea.ComeOdds10
    };

    private List<EArea> allDontComeOddsAreaList = new List<EArea>()
    {
        EArea.DontComeOdds4, EArea.DontComeOdds5, EArea.DontComeOdds6, EArea.DontComeOdds8, EArea.DontComeOdds9, EArea.DontComeOdds10
    };

    private List<EArea> multiRollAreaList = new List<EArea>()
    {
        EArea.Hard22, EArea.Hard33, EArea.Hard44, EArea.Hard55, EArea.BigSix, EArea.BigEight,
        EArea.PlaceWin4, EArea.PlaceWin5, EArea.PlaceWin6, EArea.PlaceWin8, EArea.PlaceWin9, EArea.PlaceWin10,
        EArea.PlaceLose4, EArea.PlaceLose5, EArea.PlaceLose6, EArea.PlaceLose8, EArea.PlaceLose9, EArea.PlaceLose10,
        EArea.Buy4, EArea.Buy5, EArea.Buy6, EArea.Buy8, EArea.Buy9, EArea.Buy10,
        EArea.Lay4, EArea.Lay5, EArea.Lay6, EArea.Lay8, EArea.Lay9, EArea.Lay10
    };

    private List<EArea> singleRollAreaList = new List<EArea>()
    {
        EArea.AnySeven, 
        EArea.Horn11, EArea.Horn12, EArea.Horn56, EArea.Horn66,
        EArea.AnyCraps,
        EArea.Field
    };



    /// <summary>
    /// EArea lastChipEArea = EArea.Count means : para lastChipEArea is null
    /// </summary>
    /// <param name="eGameStage"></param>
    /// <param name="lastChipEArea"></param>
    /// <returns></returns>
    public List<EArea> GetValidAreaList(EGameStage eGameStage, EArea lastChipEArea = EArea.Count)
    {
        if (lastChipEArea == EArea.Count)
        {
            List<EArea> currentChipsTableAreaList = CanvasControl.Instance.gameCrap.CurrentChipsTableAreaList;
            List<EArea> resAreaList = new List<EArea>();

            if (CanvasControl.Instance.gameCrap.CurrentGameStage == EGameStage.PointOff_ComePointOff
                || CanvasControl.Instance.gameCrap.CurrentGameStage == EGameStage.PointOff_ComePointOn)
            {
                if (currentChipsTableAreaList.Contains(EArea.PassLine) ||
                    currentChipsTableAreaList.Contains(EArea.DontPassH) ||
                    currentChipsTableAreaList.Contains(EArea.DontPassV))
                {
                    resAreaList.AddRange(basicPassAreaList);
                    resAreaList.AddRange(singleRollAreaList);
                    resAreaList.AddRange(multiRollAreaList);
                }
                else
                {
                    resAreaList.AddRange(basicPassAreaList);
                }

                return resAreaList;
            }

            if (CanvasControl.Instance.gameCrap.CurrentGameStage == EGameStage.PointOn_ComePointOff)
            {
                resAreaList.AddRange(basicComeAreaList);
                resAreaList.AddRange(singleRollAreaList);
                resAreaList.AddRange(multiRollAreaList);

                if (currentChipsTableAreaList.Contains(EArea.PassLine))
                    resAreaList.Add(EArea.PassOdds);

                if (currentChipsTableAreaList.Contains(EArea.DontPassH) || currentChipsTableAreaList.Contains(EArea.DontPassV))
                    resAreaList.Add(EArea.DontPassOdds);

                return resAreaList;
            }

            if (CanvasControl.Instance.gameCrap.CurrentGameStage == EGameStage.PointOn_ComePointOn)
            {
                resAreaList.AddRange(basicComeAreaList);
                resAreaList.AddRange(singleRollAreaList);
                resAreaList.AddRange(multiRollAreaList);

                if (currentChipsTableAreaList.Contains(EArea.PassLine))
                    resAreaList.Add(EArea.PassOdds);

                if (currentChipsTableAreaList.Contains(EArea.DontPassH) || currentChipsTableAreaList.Contains(EArea.DontPassV))
                    resAreaList.Add(EArea.DontPassOdds);

                foreach (EArea eArea in currentChipsTableAreaList)
                {
                    if(allComeOddsAreaList.Contains(eArea) || allDontComeOddsAreaList.Contains(eArea))
                        resAreaList.Add(eArea);
                }

                return resAreaList;
            }

        }

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
