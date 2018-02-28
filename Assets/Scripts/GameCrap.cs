using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum EGameStage
{
    ComeOut = 2,

    Dont_Pass_Point = 4,
    Pass_Point = 6

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
        }
    }

    [Header("Managers")]
    public ChipsManager chipsManager;
    public CrapsTableAreaManager crapsTableAreaManager;
    public HistoryPanelManager historyPanelManager;

    [Header("CrapsPoint")]
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

    public void CheckChips()
    {
        Debug.Log("### CheckChips...");
       CanvasControl.Instance.gameCrap.chipsManager.CheckChips();
    }
}
