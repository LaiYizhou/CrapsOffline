﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum EChip
{
    _10 = 0,
    _100,
    _200,
    _500,

    _1K,
    _2K,
    _5K,
    _10K,
    _20K,
    _25K,
    _50K,
    _100K,
    _250K,
    _500K,

    _1M,
    _1_25M,
    _2M,
    _2_5M,
    _5M,
    _10M,
    _12_5M,
    _25M,
    _50M,
    _100M,

    Count

}

public class Chip : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    Vector3 newPosition;

    public Vector3 OriginalPos { get; private set; }

    // Only for show In Inspector Panel
    [SerializeField] private EArea onArea;

    public EArea OnArea
    {
        get
        {
            onArea = onCrapsTableArea.AreaType;
            return onCrapsTableArea.AreaType;
        }
        private set
        {
            onArea = value;
        }
    }

    [SerializeField] private CrapsTableArea onCrapsTableArea;

    [SerializeField] private Image chipImage;
    [SerializeField] private EChip chipType;
    [SerializeField] private long value;

    public EChip ChipType { get { return chipType; }}

    public long Value
    {
        get { return value; }

        private set { this.value = value; }
    }


    public void Init(Chip chip, Vector3 originalPos, CrapsTableArea area)
    {
        Init(chip.chipType);
        OriginalPos = originalPos;
        onCrapsTableArea = area;
        OnArea = onCrapsTableArea.AreaType;
    }

    public void Init(EChip eChip)
    {
        chipType = eChip;
        Value = GameHelper.Instance.GetChipValue(eChip);
        chipImage.sprite = GameHelper.Instance.GetChipSprite(eChip);
    }

    /// <summary>
    /// return bool : is continue to remain on the Table
    /// </summary>
    public bool Check()
    {
        switch (OnArea)
        {
            case EArea.PassLine:
                return PassLineCheck();
            case EArea.PassOdds:
                return PassLineOddsCheck();

            case EArea.BigSix:
                return BigCheck(6);
            case EArea.BigEight:
                return BigCheck(8);

            case EArea.DontPassH:
            case EArea.DontPassV:
                return DontPassLineCheck();
            case EArea.DontPassOdds:
                return DontPassLineOddsCheck();

            case EArea.Field:
                return FieldCheck();

            case EArea.Come:
                return ComeCheck();
            case EArea.DontCome:
                return DontComeCheck();

            case EArea.Buy4:
                return BuyCheck(4);
            case EArea.Buy5:
                return BuyCheck(5);
            case EArea.Buy6:
                return BuyCheck(6);
            case EArea.Buy8:
                return BuyCheck(8);
            case EArea.Buy9:
                return BuyCheck(9);
            case EArea.Buy10:
                return BuyCheck(10);

            case EArea.Lay4:
                return LayCheck(4);
            case EArea.Lay5:
                return LayCheck(5);
            case EArea.Lay6:
                return LayCheck(6);
            case EArea.Lay8:
                return LayCheck(8);
            case EArea.Lay9:
                return LayCheck(9);
            case EArea.Lay10:
                return LayCheck(10);

            case EArea.PlaceLose4:
                return PlaceLoseCheck(4);
            case EArea.PlaceLose5:
                return PlaceLoseCheck(5);
            case EArea.PlaceLose6:
                return PlaceLoseCheck(6);
            case EArea.PlaceLose8:
                return PlaceLoseCheck(8);
            case EArea.PlaceLose9:
                return PlaceLoseCheck(9);
            case EArea.PlaceLose10:
                return PlaceLoseCheck(10);

            case EArea.DontComeOdds4:
                return DontComeOddsCheck(4);
            case EArea.DontComeOdds5:
                return DontComeOddsCheck(5);
            case EArea.DontComeOdds6:
                return DontComeOddsCheck(6);
            case EArea.DontComeOdds8:
                return DontComeOddsCheck(8);
            case EArea.DontComeOdds9:
                return DontComeOddsCheck(9);
            case EArea.DontComeOdds10:
                return DontComeOddsCheck(10);

            case EArea.ComeOdds4:
                return ComeOddsCheck(4);
            case EArea.ComeOdds5:
                return ComeOddsCheck(5);
            case EArea.ComeOdds6:
                return ComeOddsCheck(6);
            case EArea.ComeOdds8:
                return ComeOddsCheck(8);
            case EArea.ComeOdds9:
                return ComeOddsCheck(9);
            case EArea.ComeOdds10:
                return ComeOddsCheck(10);

            case EArea.PlaceWin4:
                return PlaceWinCheck(4);
            case EArea.PlaceWin5:
                return PlaceWinCheck(5);
            case EArea.PlaceWin6:
                return PlaceWinCheck(6);
            case EArea.PlaceWin8:
                return PlaceWinCheck(8);
            case EArea.PlaceWin9:
                return PlaceWinCheck(9);
            case EArea.PlaceWin10:
                return PlaceWinCheck(10);

            case EArea.AnySeven:
                return AnySevenCheck();

            case EArea.Hard22:
                return HardCheck(4);
            case EArea.Hard33:
                return HardCheck(6);
            case EArea.Hard44:
                return HardCheck(8);
            case EArea.Hard55:
                return HardCheck(10);
           
            case EArea.Horn11:
                return Horn11Check();
            case EArea.Horn56:
                return Horn56Check();
            case EArea.Horn12:
                return Horn12Check();
            case EArea.Horn66:
                return Horn66Check();

            case EArea.AnyCraps:
                return AnyCrapsCheck();

            default:
                return true;
        }
    }

    #region All Areas Check

    private bool PassLineCheck()
    {
        if (!CanvasControl.Instance.gameCrap.IsPointOn)
        {
            if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsNatural())
            {
                return Win();
            }
            else if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsCraps())
            {
                return Lose();
            }
        }
        else
        {
            if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsAnySeven())
            {
                return Lose();
            }
            else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum ==
                     CanvasControl.Instance.gameCrap.CurrentCrapsPointValue)
            {
                return Win();
            }
            else
            {
                return true;
            }
        }

        return true;
    }

    private bool PassLineOddsCheck()
    {
        return PassLineCheck();
    }

    private bool DontPassLineCheck()
    {
        if (!CanvasControl.Instance.gameCrap.IsPointOn)
        {
            if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsNatural())
            {
                return Lose();
            }
            else if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsCraps())
            {
                if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == 12)
                    return ReturnPrincipal();
                else
                    return Win();
            }
        }
        else
        {
            if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsAnySeven())
            {
                return Win();
            }
            else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum ==
                     CanvasControl.Instance.gameCrap.CurrentCrapsPointValue)
            {
                return Lose();
            }
            else
            {
                return true;
            }
        }

        return true;
    }

    private bool ComeCheck()
    {
        if (CanvasControl.Instance.gameCrap.IsPointOn)
        {
            if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsNatural())
            {
                return Win();
            }
            else if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsCraps())
            {
                if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == 12)
                    return ReturnPrincipal();
                else
                    return Win();
            }
            else
            {
                return MovetoComePoint(CanvasControl.Instance.gameCrap.CurrentDiceState.Sum);
            }
        }

        return true;
    }

    private bool DontComeCheck()
    {
        if (CanvasControl.Instance.gameCrap.IsPointOn)
        {
            if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsNatural())
            {
                return Lose();
            }
            else if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsCraps())
            {
                return Lose();
            }
            else
            {
                return MovetoDontComePoint(CanvasControl.Instance.gameCrap.CurrentDiceState.Sum);
            }
        }

        return true;
    }

    private bool DontPassLineOddsCheck()
    {
        return DontPassLineCheck();
    }

    private bool BigCheck(int sum)
    {
        if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsAnySeven())
            return Lose();
        else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == sum)
        {
            return Win();
        }

        return true;
    }

    private bool HardCheck(int sum)
    {
        if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsAnySeven())
            return Lose();
        else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == sum)
        {
            if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsHard())
                return Win();
            else
                return Lose();
        }

        return true;
    }

    private bool FieldCheck()
    {
        if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsField())
            return Win();
        else
            return Lose();
    }

    private bool AnySevenCheck()
    {
        return CanvasControl.Instance.gameCrap.CurrentDiceState.IsAnySeven() ? Win() : Lose();
    }


    private bool AnyCrapsCheck()
    {
        return CanvasControl.Instance.gameCrap.CurrentDiceState.IsAnyCraps() ? Win() : Lose();
    }

    private bool Horn12Check()
    {
        return CanvasControl.Instance.gameCrap.CurrentDiceState.IsHorn_1_2() ? Win() : Lose();
    }

    private bool Horn56Check()
    {
        return CanvasControl.Instance.gameCrap.CurrentDiceState.IsHorn_5_6() ? Win() : Lose();
    }

    private bool Horn11Check()
    {
        return CanvasControl.Instance.gameCrap.CurrentDiceState.IsHorn_1_1() ? Win() : Lose();
    }

    private bool Horn66Check()
    {
        return CanvasControl.Instance.gameCrap.CurrentDiceState.IsHorn_6_6() ? Win() : Lose();
    }

    private bool PlaceWinCheck(int sum)
    {
        if (CanvasControl.Instance.gameCrap.IsPointOn)
        {
            if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsAnySeven())
            {
                return Lose();
            }
            else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == sum)
            {
                return Win();
            }
            else
            {
                return true;
            }
        }

        return true;
    }

    private bool BuyCheck(int sum)
    {
        return PlaceWinCheck(sum);
    }

    private bool PlaceLoseCheck(int sum)
    {
        if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsAnySeven())
        {
            return Win();
        }
        else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == sum)
        {
            return Lose();
        }
        else
        {
            return true;
        }
    }

    private bool ComeOddsCheck(int sum)
    {
        if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsAnySeven())
            return Lose();
        else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == sum)
            return Win();
        else
            return true;
    }

    private bool DontComeOddsCheck(int sum)
    {
        if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsAnySeven())
            return Win();
        else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == sum)
            return Lose();
        else
            return true;
    }

    private bool LayCheck(int sum)
    {
        return PlaceLoseCheck(sum);
    }

    #endregion

    void Awake()
    {

        OriginalPos = this.GetComponent<RectTransform>().anchoredPosition;

    }

    /// <summary>
    /// return false : this chip DON'T remain on the Table
    /// </summary>
    /// <returns></returns>
    private bool Win()
    {

        if (this.OnArea == EArea.PassLine || this.OnArea == EArea.DontPassH || this.OnArea == EArea.DontPassV)
        {
            CanvasControl.Instance.gameAchievement.IsWinRound = true;
            if (this.OnArea == EArea.PassLine)
            {
                CanvasControl.Instance.gameAchievement.SetWinDictionary(EArea.PassLine);
            }
        }
        else
        {
            CanvasControl.Instance.gameAchievement.SetWinDictionary(this.OnArea);
        }



        TakeBack();

        long winNumber = GameHelper.Instance.GetOdds(this, OnArea,
            CanvasControl.Instance.gameCrap.CurrentDiceState);

        Debug.Log("! ! ! WIN Coins : " + winNumber);
        GameHelper.player.ChangeCoins(winNumber);
        CanvasControl.Instance.gameCrap.OneRollWinAndLoseResult += winNumber;

        //if(GameHelper.Instance != null)
            //GameTestHelper.Instance.Log(string.Format("   [Win]  {0}  ;  {1}  ;  {2}   |   {3} = {4}", this.ChipType, this.Value, this.OnArea, "+"+winNumber, GameHelper.player.Coins));
       
        CanvasControl.Instance.gameCrap.RemoveChipArea(this.OnArea);

        return false;
    }

    /// <summary>
    /// return false : this chip DON'T remain on the Table
    /// </summary>
    /// <returns></returns>
    private bool Lose()
    {
        Debug.Log("! ! ! Lose Coins : " + this.Value);

        //if (GameHelper.Instance != null)
            //GameTestHelper.Instance.Log(string.Format("  [Lose]  {0}  ;  {1}  ;  {2}   |   {3} = {4}", this.ChipType, this.Value, this.OnArea, "lose", GameHelper.player.Coins));

        CanvasControl.Instance.gameCrap.OneRollWinAndLoseResult -= this.Value;

        LoseAndTakeAway();

        CanvasControl.Instance.gameCrap.RemoveChipArea(this.OnArea);

        return false;
    }

    private bool MovetoComePoint(int point)
    {

        RectTransform targetRct = CanvasControl.Instance.gameCrap.crapsTableAreaManager.GetComeOdds(point);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(this.GetComponent<RectTransform>().DOLocalMove(targetRct.anchoredPosition, 0.5f));
        sequence.AppendCallback(() =>
        {
            CanvasControl.Instance.gameCrap.RemoveChipArea(OnArea);
            onCrapsTableArea = targetRct.GetComponent<CrapsTableArea>();
            CanvasControl.Instance.gameCrap.AddChipArea(OnArea);
        });

        return true;
    }

    private bool MovetoDontComePoint(int point)
    {

        RectTransform targetRct = CanvasControl.Instance.gameCrap.crapsTableAreaManager.GetDontComeOdds(point);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(this.GetComponent<RectTransform>().DOLocalMove(targetRct.anchoredPosition, 0.5f));
        sequence.AppendCallback(() =>
        {
            CanvasControl.Instance.gameCrap.RemoveChipArea(OnArea);
            onCrapsTableArea = targetRct.GetComponent<CrapsTableArea>();
            CanvasControl.Instance.gameCrap.AddChipArea(OnArea);
        });

        return true;
    }

    /// <summary>
    /// return false : this chip DON'T remain on the Table
    /// </summary>
    /// <returns></returns>
    private bool ReturnPrincipal()
    {
        TakeBack();

        Debug.Log("! ! ! Return Coins : " + Value);
        
        GameHelper.player.ChangeCoins(Value);
        CanvasControl.Instance.gameCrap.OneRollWinAndLoseResult += this.Value;

        //if (GameHelper.Instance != null)
            //GameTestHelper.Instance.Log(string.Format("[Return]  {0}  ;  {1}  ;  {2}   |   {3} = {4}", this.ChipType, this.Value, this.OnArea, Value, GameHelper.player.Coins));


        CanvasControl.Instance.gameCrap.RemoveChipArea(this.OnArea);

        return false;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(CanvasControl.Instance.gameCrap.chipsManager.IsChipsCanDrag)
            CanvasControl.Instance.gameCrap.crapsTableAreaManager.ShowAllUIs();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {

        if (CanvasControl.Instance.gameCrap.chipsManager.IsChipsCanDrag)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(this.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out newPosition);

            transform.position = newPosition + GameHelper.ChipOnDragPosOffset;
            transform.localScale = GameHelper.ChipOnDragScale;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CanvasControl.Instance.gameCrap.chipsManager.IsChipsCanDrag)
        {

            if (onCrapsTableArea != null && onCrapsTableArea.State != EState.Dark)
            {

                Vector3 localPos = CanvasControl.Instance.gameCrap.chipsManager.GetTableChipPos(this.transform.localPosition);
                long betMax = GameHelper.Instance.GetCrapSceneInfo(CanvasControl.Instance.gameCrap.LevelId).BetMax;
                long tabelMax = GameHelper.Instance.GetCrapSceneInfo(CanvasControl.Instance.gameCrap.LevelId).TableMax;

                if (Value + CanvasControl.Instance.gameCrap.chipsManager.GetEAreaChipsValue(OnArea) > betMax)
                {
                    GameHelper.Instance.ShowTip(localPos, string.Format("Max Single Bet is {0}", GameHelper.CoinLongToString(betMax)));

                }
                else if (Value + CanvasControl.Instance.gameCrap.chipsManager.GetAllChipsValue() > tabelMax)
                {
                    GameHelper.Instance.ShowTip(localPos, string.Format("Max Total Bet is {0}", GameHelper.CoinLongToString(tabelMax)));
                }
                else
                {
                    if (this.Value <= GameHelper.player.Coins)
                    {
                        CanvasControl.Instance.gameCrap.chipsManager.BuildTableChip(this.transform.localPosition, this, onCrapsTableArea);
                        Debug.Log("! ! ! Use Coins : " + this.Value);
                        GameHelper.player.ChangeCoins(-1L * this.Value);
                    }
                    else
                    {
                        //CanvasControl.Instance.gameStore.Show();
                        CanvasControl.Instance.gamePromotion.Show(GamePromotion.EPromotionType.LoginSale);
                    }
                }
            }

            transform.localPosition = OriginalPos;
            transform.localScale = Vector3.one;
            CanvasControl.Instance.gameCrap.crapsTableAreaManager.ResetAllUIs();

        }
        
    }

    public void TakeBack()
    {
        StartCoroutine(DelayDestroy());
        this.GetComponent<RectTransform>().DOLocalMove(OriginalPos, 0.5f);
    }

    public void LoseAndTakeAway()
    {
        StartCoroutine(DelayDestroy());

        // HardCheck Code
        this.GetComponent<RectTransform>().DOLocalMove(new Vector3(0.0f, 450.0f, 0.0f), 1.0f);
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log("Chip | OnTriggerEnter2D : " + coll.name);

        onCrapsTableArea = coll.gameObject.GetComponent<CrapsTableArea>();

        if (onCrapsTableArea.State != EState.Dark)
            onCrapsTableArea.State = EState.Select;
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        //Debug.Log("Chip | OnTriggerStay2D : " + coll.name);

        onCrapsTableArea = coll.gameObject.GetComponent<CrapsTableArea>();

        if (onCrapsTableArea.State != EState.Dark)
            onCrapsTableArea.State = EState.Select;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        //Debug.Log("Chip | OnTriggerExit2D : " + coll.name);

        onCrapsTableArea = coll.gameObject.GetComponent<CrapsTableArea>();
        if (onCrapsTableArea.State != EState.Dark)
            onCrapsTableArea.State = EState.Normal;

        onCrapsTableArea = null;
       
    }
}
