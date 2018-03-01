using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public enum EGameStage
{
    ComeOut = 2,

    Point

}

public class GameCrap : MonoBehaviour
{

    public EGameStage CurrentGameStage;
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
            UpdateGameStage();

        }
    }

    // Current all Chips on the TableArea
    public List<EArea> CurrentChipsTableAreaList = new List<EArea>();

    [Header("Managers")]
    public ChipsManager chipsManager;
    public CrapsTableAreaManager crapsTableAreaManager;
    public HistoryPanelManager historyPanelManager;

    [Header("CrapsPoint")]
    public int CurrentCrapsPointValue;
    [SerializeField] private Vector3 crapsPointOriginalPos;
    [SerializeField] private List<Vector3> crapPointPosList;
    [SerializeField] private Image crapsPointImage;
    [SerializeField] private Sprite crapsPointOffSprite;
    [SerializeField] private Sprite crapsPointOnSprite;


    // Use this for initialization
    void Start () {

        //Test code
	    CurrentGameStage = EGameStage.ComeOut;

    }
	
	// Update is called once per frame
	void Update () {
	
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
        if (CurrentGameStage == EGameStage.ComeOut)
        {
            if (CurrentDiceState.IsPoint())
            {
                CurrentGameStage = EGameStage.Point;
                MovePoint(CurrentDiceState.Sum);
            }
        }
        else
        {
            if (CurrentDiceState.IsAnySeven() || CurrentDiceState.Sum == CurrentCrapsPointValue)
            {
                CurrentGameStage = EGameStage.ComeOut;
                ResetPoint();
            }

        }
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
        sequence.Append(crapsPointImage.GetComponent<RectTransform>().DOLocalMove(crapsPointOriginalPos, 0.5f));
        sequence.AppendCallback(() =>
        {
            crapsPointImage.sprite = crapsPointOffSprite;
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
