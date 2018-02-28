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
            UpdateGameStage();
        }
    }

    [Header("Managers")]
    public ChipsManager chipsManager;
    public CrapsTableAreaManager crapsTableAreaManager;
    public HistoryPanelManager historyPanelManager;

    [Header("CrapsPoint")]
    private Vector3 crapsPointOriginalPos;
    [SerializeField] private Image crapsPointImage;
    [SerializeField] private Sprite crapsPointOffSprite;
    [SerializeField] private Sprite crapsPointOnSprite;


    // Use this for initialization
    void Start () {

        //Test code
	    CurrentGameStage = EGameStage.ComeOut;

        crapsPointOriginalPos = crapsPointImage.GetComponent<RectTransform>().localPosition;


    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateGameStage()
    {

    }

    public void CheckChips()
    {
        Debug.Log("### CheckChips...");
       CanvasControl.Instance.gameCrap.chipsManager.CheckChips();
    }
}
