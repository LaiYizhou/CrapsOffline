using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public enum EGameStage
{
    //ComeOut = 2,

    //Point,

    //ComePoint

    PointOn_ComePointOn,

    PointOn_ComePointOff,

    PointOff_ComePointOn,

    PointOff_ComePointOff,


}

public class GameCrap : MonoBehaviour
{


   

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
            
            CheckChips();
            //UpdateComePoints();
            UpdateGameStage();

        }
    }

    [SerializeField]
    private List<int> crpasComePointsList = new List<int>();

    // Current all Chips on the TableArea
    public List<EArea> CurrentChipsTableAreaList = new List<EArea>();

    [Header("Managers")]
    public ChipsManager chipsManager;
    public CrapsTableAreaManager crapsTableAreaManager;
    public HistoryPanelManager historyPanelManager;

    [Header("CrapsPoint")]
    public int CurrentCrapsPointValue;
   
    [SerializeField] private List<Vector3> crapPointPosList;
    [SerializeField] private Image crapsPointImage;
    [SerializeField] private Sprite crapsPointOffSprite;
    [SerializeField] private Sprite crapsPointOnSprite;


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
    void Start () {

        //Test code
	    //CurrentGameStage = EGameStage.ComeOut;
        isComePointOn = false;
        SetPointOff();

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
        if(!CurrentChipsTableAreaList.Contains(eArea))
            CurrentChipsTableAreaList.Add(eArea);
    }

    public void RemoveChipArea(EArea eArea)
    {
        if (CurrentChipsTableAreaList.Contains(eArea))
            CurrentChipsTableAreaList.Remove(eArea);
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

        //if (!isComePointOn)
        //{

        //}

        //if (CurrentGameStage == EGameStage.ComeOut)
        //{
        //    if (CurrentChipsTableAreaList.Contains(EArea.Come) || CurrentChipsTableAreaList.Contains(EArea.DontCome))
        //    {
        //        if(CurrentDiceState.IsPoint())
        //            CurrentGameStage = EGameStage.ComePoint;
        //    }
        //}
        //else
        //{
        //    if (CurrentDiceState.IsAnySeven() || CurrentDiceState.Sum == CurrentCrapsPointValue)
        //    {
        //        CurrentGameStage = EGameStage.ComeOut;
        //        SetPointOff();
        //    }

        //}
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

        if (CurrentDiceState.IsNatural() || CurrentDiceState.IsCraps())
        {

        }
        else
        {

        }

        CanvasControl.Instance.gameCrap.chipsManager.CheckChips();
    }
}
