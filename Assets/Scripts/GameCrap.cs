﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using DG.Tweening;
using UnityEngine.UI;

public enum EGameStage
{

    PointOn_ComePointOn,

    PointOn_ComePointOff,

    PointOff_ComePointOn,

    PointOff_ComePointOff,


}

public class GameCrap : MonoBehaviour
{

    [SerializeField] private int levelId;
    public int LevelId
    {
        get { return levelId; }
    }
    [SerializeField] private bool isPointOn;
    public bool IsPointOn
    {
        get { return isPointOn; }
    }
    [SerializeField] private bool isComePointOn;
    public bool IsComePointOn
    {
        get { return isComePointOn; }
    }

    public EGameStage CurrentGameStage
    {
        get
        {
           if(IsPointOn && IsComePointOn)
                return EGameStage.PointOn_ComePointOn;
           else if(IsPointOn && !IsComePointOn)
                return EGameStage.PointOn_ComePointOff;
           else if(!IsPointOn && IsComePointOn)
                return EGameStage.PointOff_ComePointOn;
           else
                return EGameStage.PointOff_ComePointOff;
        }
    }

    private DiceState currentDiceState;
    public DiceState CurrentDiceState
    {
        get
        {
            return currentDiceState;
        }

        set
        {
            currentDiceState = value;
            StartCoroutine(DelayCheck());
            
        }
    }

    IEnumerator DelayCheck()
    {
        yield return new WaitUntil(() => diceManager.IsInBox == true);

        CheckChips();

        //UpdateComePoints() is called in UpdateGameStage();
        UpdateGameStage();

        chipsManager.ClearTableCurrentChipList();
    }

    [SerializeField]
    private List<int> crpasComePointsList = new List<int>();

    // Current all Chips on the TableArea
    public List<EArea> CurrentAllChipsTableAreaList = new List<EArea>();

    [Header("Managers")]
    public ChipsManager chipsManager;
    public CrapsTableAreaManager crapsTableAreaManager;
    public HistoryPanelManager historyPanelManager;
    public DiceManager diceManager;

    [Header("CrapsPoint")]
    public int CurrentCrapsPointValue;
   
    [SerializeField] private List<Vector3> crapPointPosList;
    [SerializeField] private Image crapsPointImage;
    [SerializeField] private Sprite crapsPointOffSprite;
    [SerializeField] private Sprite crapsPointOnSprite;


    [Space(10)]
    [SerializeField] private Button backButton;
    [SerializeField] private Text coinsText;
    [SerializeField] private Button addCoinButton;
    [SerializeField] private Button cornerAddCoinButton;
    [SerializeField] private Button guideButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button adButton;
    [SerializeField] private Text gameStateText;

    public long OneRollResult;

    public void SetGameStateText(long number, bool isOnBet)
    {
        if (isOnBet)
        {
            gameStateText.text = "You bet " + GameHelper.CoinLongToString(number);
        }
        else
        {
            if (number > 0)
            {
                gameStateText.text = "You won " + GameHelper.CoinLongToString(number);

                AudioControl.Instance.PlaySound(AudioControl.EAudioClip.WinChip);
            }
            else if (number < 0)
            {
                gameStateText.text = "You Lost " + GameHelper.CoinLongToString(-number);
            }
            else
            {
                //gameStateText.text = "[Test] " + GameHelper.CoinLongToString(number);
            }
        }
    }

    public void OnBackButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        CanvasControl.Instance.gameSetting.Hide();
        CanvasControl.Instance.gameHall.gameObject.SetActive(true);

        ResetData();
    }

    public void OnAddCoinButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        CanvasControl.Instance.gameStore.Show();
    }

    public void OnCornerAddCoinButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        CanvasControl.Instance.gameStore.Show();
    }

    public void OnGuideButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        CanvasControl.Instance.gameTutorial.Show();
    }

    public void OnSettinButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        CanvasControl.Instance.gameSetting.Switch();
    }

    public void OnAdButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);
    }

    public void UpdatePlayerCoin()
    {
        //coinsText.text = GameHelper.CoinLongToString(GameHelper.player.Coins);

        long currentCoins = 0;
        long targetCoins = GameHelper.player.Coins;

        try
        {
            currentCoins = GameHelper.CoinStringToLong(coinsText.text);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            currentCoins = targetCoins;
        }

        if (currentCoins != targetCoins)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(DOTween.To(() => currentCoins,
                x =>
                {
                    currentCoins = x;
                    coinsText.text = GameHelper.CoinLongToString(currentCoins);
                },
                targetCoins, 1.0f));

            sequence.AppendCallback(() =>
            {
                coinsText.text = GameHelper.CoinLongToString(currentCoins);
            });
        }
        else
        {
            coinsText.text = GameHelper.CoinLongToString(currentCoins);
        }
    }

    private void ResetData()
    {
        this.levelId = 0;

        isComePointOn = false;
        SetPointOff();

        crpasComePointsList.Clear();
        CurrentAllChipsTableAreaList.Clear();

        chipsManager.Clear(false);

    }

    public void Init(int levelId)
    {

        ResetData();

        CrapSceneInfo crapSceneInfo = GameHelper.Instance.GetCrapSceneInfo(levelId);

        this.levelId = crapSceneInfo.Level;
        chipsManager.BuildCandiChips(crapSceneInfo);
    }


    public void SetPointOn(int num)
    {
        isPointOn = true;
        MovePoint(num);
    }

    public void SetPointOff()
    {
        isPointOn = false;
        ResetPoint();
    }

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
	void Update () {
    
	}

    public void UpdateComePoints(int point)
    {
        if (crpasComePointsList.Contains(point))
            crpasComePointsList.Remove(point);
        else
            crpasComePointsList.Add(point);

        if (crapPointPosList.Count == 0 || crapPointPosList == null)
            isComePointOn = false;
        else
            isComePointOn = true;
    }

    public void AddChipArea(EArea eArea)
    {
        if(!CurrentAllChipsTableAreaList.Contains(eArea))
            CurrentAllChipsTableAreaList.Add(eArea);
    }

    public void RemoveChipArea(EArea eArea)
    {
        if (CurrentAllChipsTableAreaList.Contains(eArea))
            CurrentAllChipsTableAreaList.Remove(eArea);
    }

    public void UpdateGameStage()
    {

        if (!IsPointOn)
        {
            if(CurrentDiceState.IsPoint())
                SetPointOn(CurrentDiceState.Sum);
        }
        else
        {
            if (CurrentDiceState.IsAnySeven() || CurrentDiceState.Sum == CurrentCrapsPointValue)
                SetPointOff();
        }


        if (CurrentAllChipsTableAreaList.Contains(EArea.Come)
            || CurrentAllChipsTableAreaList.Contains(EArea.DontCome)

            || CurrentAllChipsTableAreaList.Contains(EArea.ComeOdds4)
            || CurrentAllChipsTableAreaList.Contains(EArea.ComeOdds5)
            || CurrentAllChipsTableAreaList.Contains(EArea.ComeOdds6)
            || CurrentAllChipsTableAreaList.Contains(EArea.ComeOdds8)
            || CurrentAllChipsTableAreaList.Contains(EArea.ComeOdds9)
            || CurrentAllChipsTableAreaList.Contains(EArea.ComeOdds10)

            || CurrentAllChipsTableAreaList.Contains(EArea.DontComeOdds4)
            || CurrentAllChipsTableAreaList.Contains(EArea.DontComeOdds5)
            || CurrentAllChipsTableAreaList.Contains(EArea.DontComeOdds6)
            || CurrentAllChipsTableAreaList.Contains(EArea.DontComeOdds8)
            || CurrentAllChipsTableAreaList.Contains(EArea.DontComeOdds9)
            || CurrentAllChipsTableAreaList.Contains(EArea.DontComeOdds10)
        )

            UpdateComePoints(CurrentDiceState.Sum);

    }

    private void MovePoint(int diceNumber)
    {
        if (diceNumber >= 4 && diceNumber <= 10 && diceNumber != 7)
        {
            Vector3 targetVector3 = crapPointPosList[diceNumber - 4];

            Sequence sequence = DOTween.Sequence();
            sequence.Append(crapsPointImage.GetComponent<RectTransform>().DOLocalMove(targetVector3, 0.5f));
            sequence.AppendCallback(() =>
            {
                crapsPointImage.sprite = crapsPointOnSprite;
                CurrentCrapsPointValue = diceNumber;
            });
        }
    }

    private void ResetPoint()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(crapsPointImage.GetComponent<RectTransform>().DOLocalMove(GameHelper.CrapsPointOriginalPos, 0.5f));
        sequence.AppendCallback(() =>
        {
            crapsPointImage.sprite = crapsPointOffSprite;
            CurrentCrapsPointValue = 0;
        });
    }

    public void CheckChips()
    {
        Debug.Log("### CheckChips...");

        CanvasControl.Instance.gameCrap.chipsManager.CheckChips();
        SetGameStateText(OneRollResult, false);
    }
}
