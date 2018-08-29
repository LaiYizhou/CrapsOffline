using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using JetBrains.Annotations;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class GameAchievement : MonoBehaviour
{


    #region AchievementData

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
    [HideInInspector] public List<AchievementInfo> GameAchievementInfoList = new List<AchievementInfo>()
    {
        new AchievementInfo(),
        //new AchievementInfo(1, "Play <size=32>30</size> rounds", 30, 6000),
        //new AchievementInfo(2, "Win <size=32>18</size> rounds", 18, 6000),
        //new AchievementInfo(3, "Bet on <size=32>PASS LINE</size> and win <size=32>8</size> rounds", 8, 4500),
        new AchievementInfo(1, "[Test] Play <size=32>3</size> rounds", 3, 6000),
        new AchievementInfo(2, "[Test] Win <size=32>2</size> rounds", 2, 6000),
        new AchievementInfo(3, "[Test] Bet on <size=32>PASS LINE</size> and win <size=32>2</size> rounds", 2, 4500),
        new AchievementInfo(4, "Bet on <size=32>COME</size> and win <size=32>8</size> rounds", 8, 4500),
        new AchievementInfo(5, "Bet on <size=32>FIELD</size> and win <size=32>8</size> rounds", 8, 4500),
        new AchievementInfo(6, "Bet on <size=32>BIG 6</size> and win <size=32>5</size> rounds", 5, 5000),
        new AchievementInfo(7, "Bet on <size=32>BIG 8</size> and win <size=32>5</size> rounds", 5, 5000),
        new AchievementInfo(8, "Bet on <size=32>HARD WAY</size> and win <size=32>5</size> rounds", 5, 5500),
        new AchievementInfo(9, "Bet on <size=32>ANY CRAPS</size> and win <size=32>3</size> rounds", 3, 5500),
        new AchievementInfo(10, "Bet on <size=32>ANY SEVEN</size> and win <size=32>3</size> rounds", 3, 5500)

    };

    [HideInInspector] public List<AchievementLevelInfo> GameAchievementLevelInfos = new List<AchievementLevelInfo>()
    {
        new AchievementLevelInfo(),
        new AchievementLevelInfo(1, 3, 9000),
        new AchievementLevelInfo(2, 6, 12000),
        new AchievementLevelInfo(3, 10, 18000)
    };

    [SerializeField] private int achievementLevel;
    public int AchievementLevel
    {
        get { return PlayerPrefs.HasKey("AchievementLevel") ? PlayerPrefs.GetInt("AchievementLevel") : 1; }

        private set
        {
            achievementLevel = value;
            PlayerPrefs.SetInt("AchievementLevel", achievementLevel);
        }
    }

    public int LastCompleteAchievementCount
    {
        get { return PlayerPrefs.HasKey("LastCompleteAchievementCount") ? PlayerPrefs.GetInt("LastCompleteAchievementCount") : 0; }

        private set
        {
            PlayerPrefs.SetInt("LastCompleteAchievementCount", value);
        }
    }

    public int CompleteAchievementCount
    {
        get
        {
            if (GameAchievementInfoList == null || GameAchievementInfoList.Count <= 0)
            {
                return 0;
            }
            else
            {
                int number = 0;
                for (int i = 1; i < GameAchievementInfoList.Count; i++)
                {
                    if (GameAchievementInfoList[i].IsComplete)
                        number++;
                }

                return number;
            }
        }
        
    }

    public int CurrentAchievementLevelTargetValue
    {
        get
        {

            if (GameAchievementLevelInfos == null || GameAchievementLevelInfos.Count <= 0)
                return 0;

            if (AchievementLevel < 1 || AchievementLevel > GameAchievementLevelInfos.Count)
                return 0;

            return GameAchievementLevelInfos[AchievementLevel].TargetValue;
        }
    }

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

    public void SaveLastCompleteAchievementCount()
    {
        LastCompleteAchievementCount = CompleteAchievementCount;
    }

    public void AchievementLevelAddOne()
    {
        AchievementLevel++;
        if (AchievementLevel > GameAchievementLevelInfos.Count - 1)
            AchievementLevel = GameAchievementLevelInfos.Count - 1;
    }

    public void AchievementValueAddOne(int index)
    {
        if (index > 0 && index < GameAchievementInfoList.Count)
        {
            GameAchievementInfoList[index].AddCurrentValue();
        }
    }

    public void ResetRoundCounter()
    {
        IsWinRound = false;
        eAreaWinDictionay = new Dictionary<EArea, bool>();
    }


    public void ResetAchievementsData()
    {
        for (int i = 1; i < CanvasControl.Instance.gameAchievement.GameAchievementInfoList.Count; i++)
        {
            CanvasControl.Instance.gameAchievement.GameAchievementInfoList[i].Reset();
        }

        AchievementLevel = 1;
        LastCompleteAchievementCount = 0;
    }

    #endregion

  
    [SerializeField] private Transform panel;

    [Header("TopPanel")]
    [SerializeField] private Text progressText;
    [SerializeField] private Image sliderBarImage;
    [SerializeField] private Transform lineContainerTransform;
    [SerializeField] private GameObject linePrefab;

    [Space(5)]
    [SerializeField] private Button giftButton;
    [SerializeField] private Image giftIconImage;
    [SerializeField] private SkeletonGraphic giftIconEffect;

    [Space(10)]
    [SerializeField] private List<AchievementsItem> achievementsItemList; //index begins with 1

    public void Init()
    {
        IsRoundStart = false;
        ResetRoundCounter();
    }

    public void DrawLine()
    {
        //int number = GameAchievementLevelInfos[AchievementLevel].TargetValue;
        //for (int i = 0; i < number - 1; i++)
        //{
        //    GameObject go = Instantiate(linePrefab);
        //    go.transform.SetParent(lineContainerTransform);
        //    go.transform.localPosition = Vector3.zero;
        //    go.transform.localScale = Vector3.one;
        //}
    }

    public void Show()
    {

        InitTopPanel();
        InitAchievements();

        panel.transform.localScale = Vector3.zero;

        this.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();

        sequence.Insert(0.0f, panel.DOScale(Vector3.one * 1.02f, 0.2f));
        sequence.Insert(0.0f, panel.DOLocalMove(new Vector3(0.0f, 0.0f), 0.2f));

        sequence.Insert(0.2f, panel.DOScale(Vector3.one, 0.2f));
    }

    private void InitTopPanel()
    {
        if (lineContainerTransform.childCount < GameAchievementLevelInfos[AchievementLevel].TargetValue)
        {
            DrawLine();
        }

        progressText.text = string.Format("{0}/{1}", CompleteAchievementCount,
            CurrentAchievementLevelTargetValue);

        sliderBarImage.fillAmount =
            CompleteAchievementCount / (float)CurrentAchievementLevelTargetValue;
        
        if (CompleteAchievementCount < CurrentAchievementLevelTargetValue)
        {
            giftIconImage.gameObject.SetActive(true);
            giftButton.interactable = false;
            giftIconEffect.gameObject.SetActive(false);
        }
        else
        {
            giftIconImage.gameObject.SetActive(false);
            giftButton.interactable = true;
            giftIconEffect.gameObject.SetActive(true);
            giftIconEffect.AnimationState.SetAnimation(0, "animation", true);
        }

    }

    private void InitAchievements()
    {
        if (achievementsItemList.Count == GameAchievementInfoList.Count)
        {
            Queue<AchievementInfo> highQueue = new Queue<AchievementInfo>();
            Queue<AchievementInfo> lowQueue = new Queue<AchievementInfo>();

            for (int i = 1; i < GameAchievementInfoList.Count; i++)
            {
                if(GameAchievementInfoList[i].IsCollected)
                    lowQueue.Enqueue(GameAchievementInfoList[i]);
                else
                    highQueue.Enqueue(GameAchievementInfoList[i]);
            }



            for (int i = 1; i < achievementsItemList.Count;)
            {
                while (highQueue.Count > 0)
                {
                    AchievementInfo info = highQueue.Dequeue();
                    achievementsItemList[i].Init(info);
                    i++;
                }

                while (lowQueue.Count > 0)
                {
                    AchievementInfo info = lowQueue.Dequeue();
                    achievementsItemList[i].Init(info);
                    i++;
                }
            } 

        }
    }

    public void OnGiftButtonClicked()
    {

        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        giftIconImage.gameObject.SetActive(false);
        giftIconEffect.gameObject.SetActive(true);
        giftIconEffect.AnimationState.SetAnimation(0, "animation2", false);

        StartCoroutine(DelayEffect());

    }

    IEnumerator DelayEffect()
    {
        yield return new WaitForSeconds(0.5f);

        Vector3 sourcePos = GameHelper.Instance.ToCanvasLocalPos(giftButton.transform.TransformPoint(Vector3.one));
        GameHelper.Instance.coinCollectEffect.RunEffect(GameAchievementLevelInfos[AchievementLevel].RewardChips,
            sourcePos,
            new Vector3(170.0f, 194.0f, 0.0f),
            new Vector3(-174.0f, 209.0f, 0.0f));

        yield return new WaitForSeconds(1.2f);

        AchievementLevelAddOne();
        InitTopPanel();
        CanvasControl.Instance.gameCrap.UpdateGameAchievementsEffect();
    }

    public void OnCloseButtonClicked()
    {

        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        this.gameObject.SetActive(false);

    }

}


public class AchievementLevelInfo
{
    public int Level;
    public int TargetValue;
    public long RewardChips;

    public AchievementLevelInfo()
    {
    }

    public AchievementLevelInfo(int level, int targetValue, long rewardChips)
    {
        Level = level;
        TargetValue = targetValue;
        RewardChips = rewardChips;
    }

    //private string stateExtention
    //{
    //    get { return string.Format("AchievementLevelState{0}", Level.ToString()); }
    //}

    //public bool IsCollected
    //{
    //    get
    //    {
    //        return PlayerPrefs.HasKey(stateExtention) && PlayerPrefs.GetInt(stateExtention) == 1;
    //    }
    //    set
    //    {
    //        PlayerPrefs.SetInt(stateExtention, value ? 1 : 0);
    //    }
    //}

    //public void Complete()
    //{
    //    if (!IsCollected)
    //    {
    //        IsCollected = true;
    //    }

    //}

}


//[Serializable]
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

    private string stateExtention
    {
        get { return string.Format("AchievementState{0}", Id.ToString()); }
    }

    public int CurrentValue
    {
        get
        {
            return PlayerPrefs.HasKey(idExtention) ? PlayerPrefs.GetInt(idExtention) : 0;
        }

        private set
        {
            if (value > TargetValue)
            {
                value = TargetValue;
            }

            PlayerPrefs.SetInt(idExtention, value);
        }
    }

    public bool IsCollected
    {
        get
        {
            return PlayerPrefs.HasKey(stateExtention) && PlayerPrefs.GetInt(stateExtention) == 1;
        }
        private set
        {
            PlayerPrefs.SetInt(stateExtention, value ? 1 : 0);
        }
    }

    public bool IsComplete
    {
        get { return CurrentValue >= TargetValue; }
    }

    public void Collect()
    {
        if (!IsCollected)
        {
            IsCollected = true;
        }
      
    }

    public void AddCurrentValue()
    {
        CurrentValue++;
    }

    public void Reset()
    {
        CurrentValue = 0;
        IsCollected = false;
    }

    public override string ToString()
    {
        return string.Format("#Id: {0}; #Desc: {1};\n #Cur: {2}; Tar: {3}; #Chips: {4}\n #IsCollected: {5}\n",
            idExtention, Description, CurrentValue, TargetValue, RewardChips, IsCollected);
    }
}