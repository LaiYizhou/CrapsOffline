using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class GameAchievement : MonoBehaviour
{

    
    [SerializeField] private bool isRoundStart;
    public bool IsRoundStart
    {
        get
        {
            return isRoundStart;
        }

        set
        {
            isRoundStart = value;
        }
    }




    /// <summary>
    /// key - eArea that chip on, value -  is Win
    /// </summary>
    private Dictionary<EArea, bool> eAreaWinDictionay = new Dictionary<EArea, bool>();
    public bool IsWinRound;

    /// <summary>
    /// index begins with 1, because the achievement.Id begins with 1
    /// </summary>
    private List<AchievementInfo> gameAchievementInfoList = new List<AchievementInfo>()
    {
        new AchievementInfo(),
        new AchievementInfo(1, "Play 30 rounds", 30, 6000),
        new AchievementInfo(2, "Win 18 rounds", 18, 6000),
        new AchievementInfo(3, "Bet on PASS LINE and win 8 rounds", 8, 4500),
        new AchievementInfo(4, "Bet on COME and win 8 rounds", 8, 4500),
        new AchievementInfo(5, "Bet on FIELD and win 8 rounds", 8, 4500),
        new AchievementInfo(6, "Bet on BIG 6 and win 5 rounds", 5, 5000),
        new AchievementInfo(7, "Bet on BIG 8 and win 5 rounds", 5, 5000),
        new AchievementInfo(8, "Bet on HARD WAY and win 5 rounds", 5, 5500),
        new AchievementInfo(9, "Bet on ANY CRAPS and win 3 rounds", 3, 5500),
        new AchievementInfo(10, "Bet on ANY SEVEN and win 3 rounds", 3, 5500)

    };

    [Header("Test")]
    [SerializeField] private Button achievementsButton;
    [SerializeField] private Button resetAchievementsButton;
    [SerializeField] private Transform showPanelTransform;
    [SerializeField] private Text showText;

    public void SetWinDictionary(EArea eArea)
    {
        if (eAreaWinDictionay.ContainsKey(eArea))
        {
            eAreaWinDictionay[eArea] = true;
        }
        else
        {
            eAreaWinDictionay.Add(eArea, true);
        }
    }

    public bool GetWinDictionary(EArea eArea)
    {
        if (eAreaWinDictionay.ContainsKey(eArea))
            return eAreaWinDictionay[eArea];
        else
            return false;
    }

    public void AchievementValueAddOne(int index)
    {
        if (index > 0 && index < gameAchievementInfoList.Count)
        {
            gameAchievementInfoList[index].CurrentValue++;
        }
    }


    // Use this for initialization
    void Start ()
    {
        achievementsButton.onClick.AddListener(OnAchievementsButtonClicked);
        resetAchievementsButton.onClick.AddListener(OnResetAchievementsButtonClicked);
    }

    public void ResetRoundCounter()
    {
        IsWinRound = false;
        eAreaWinDictionay = new Dictionary<EArea, bool>();
    }

    public void Init()
    {
        IsRoundStart = false;
        ResetRoundCounter();
    }

    private void OnResetAchievementsButtonClicked()
    {
        for (int i = 1; i < gameAchievementInfoList.Count; i++)
        {
            gameAchievementInfoList[i].Reset();
        }
    }

    private void OnAchievementsButtonClicked()
    {
        if (showPanelTransform.gameObject.activeInHierarchy)
        {
            showPanelTransform.gameObject.SetActive(false);
        }
        else
        {
            StringBuilder sb = new StringBuilder();

            showPanelTransform.gameObject.SetActive(true);
            for (int i = 1; i < gameAchievementInfoList.Count; i++)
            {
                sb.Append(gameAchievementInfoList[i].ToString()).Append(System.Environment.NewLine);
            }

            showText.text = sb.ToString();
        }
    }

}





public class AchievementInfo
{
    public int Id;
    public string Description;
    public int TargetValue;
    public long RewardChips;



    public AchievementInfo()
    {

    }

    public AchievementInfo(int id, string description, int targetValue, long rewardChips)
    {
        Id = id;
        Description = description;
        TargetValue = targetValue;
        RewardChips = rewardChips;
    }

    private string idExtention
    {
        get { return string.Format("Achievement{0}", Id.ToString()); }
    }

    public int CurrentValue
    {
        get
        {
            return PlayerPrefs.HasKey(idExtention) ? PlayerPrefs.GetInt(idExtention) : 0;
        }

        set
        {
            if (value > TargetValue)
                value = TargetValue;

            PlayerPrefs.SetInt(idExtention, value);
        }
    }

    public void Reset()
    {
        CurrentValue = 0;
    }

    public override string ToString()
    {
        return string.Format("#Id: {0}; #Desc: {1};\n #Cur: {2}; Tar: {3}; #Chips: {4}\n",
            idExtention, Description, CurrentValue, TargetValue, RewardChips);
    }
}