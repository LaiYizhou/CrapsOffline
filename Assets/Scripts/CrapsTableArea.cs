using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum EArea
{
    PassLine = 0,
    PassOdds,

    BigSix = 2,
    BigEight,

    DontPassH = 4,
    DontPassOdds,
    DontPassV,

    Field = 7,

    Come = 8,
    DontCome,

    Buy4 = 10,
    Buy5,
    Buy6,
    Buy8,
    Buy9,
    Buy10,

    Lay4 = 16,
    Lay5,
    Lay6,
    Lay8,
    Lay9,
    Lay10,

    PlaceLose4 = 22,
    PlaceLose5,
    PlaceLose6,
    PlaceLose8,
    PlaceLose9,
    PlaceLose10,

    DontComeOdds4 = 28,
    DontComeOdds5,
    DontComeOdds6,
    DontComeOdds8,
    DontComeOdds9,
    DontComeOdds10,

    ComeOdds4 = 34,
    ComeOdds5,
    ComeOdds6,
    ComeOdds8,
    ComeOdds9,
    ComeOdds10,

    PlaceWin4 = 40,
    PlaceWin5,
    PlaceWin6,
    PlaceWin8,
    PlaceWin9,
    PlaceWin10,

    AnySeven = 46,

    Hard22 = 47,
    Hard55,
    Hard33,
    Hard44,

    Horn12,
    Horn56,
    Horn11,
    Horn66,

    AnyCraps = 55,

    Count = 56

}

public enum EState
{
    Normal,
    Dark,
    Light,
    Select,

    Count
}

public class CrapsTableArea : MonoBehaviour
{

    public EArea AreaType;

    [SerializeField] private EState state;
    public EState State
    {
        get
        {
            return state;
        }

        set
        {
            state = value;
            UpdateUI();
        }
    }

    [Header("StateSprites")]
    [SerializeField] private Image image;
    [SerializeField] private Sprite darkSprite;
    [SerializeField] private Sprite lightSprite;
    [SerializeField] private Sprite selectSprite;

    // Use this for initialization
    void Start ()
    {
        InitUI();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void InitUI()
    {
        if(image == null)
            image = this.GetComponent<Image>();

        State = EState.Normal;
    }

    private void UpdateUI()
    {
        switch (State)
        {
            case EState.Normal:
                image.color = new Color(1.0f, 1.0f, 1.0f, 0.01f);
                image.sprite = selectSprite;
                break;
            case EState.Light:
                image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                image.sprite = lightSprite;
                break;
            case EState.Dark:
                image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                image.sprite = darkSprite;
                break;
            case EState.Select:
                image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                image.sprite = selectSprite;
                break;
            default:
                image.color = new Color(1.0f, 1.0f, 1.0f, 0.01f);
                image.sprite = selectSprite;
                break;
        }
    }

    public void ShowUI()
    {

        if(GameHelper.Instance.GetValidAreaList(CanvasControl.Instance.gameCrap.CurrentGameStage).Contains(this.AreaType))
            State = EState.Normal;
        else
            State = EState.Dark;
    }

    public void ResetUI()
    {
        State = EState.Normal;
    }

    //void OnTriggerEnter2D(Collider2D coll)
    //{
    //    Debug.Log("CrapsTableArea | OnTriggerEnter2D : " + coll.name);

    //    if(State != EState.Dark)
    //        State = EState.Select;
    //}

    //void OnTriggerStay2D(Collider2D coll)
    //{
    //    //Debug.Log("CrapsTableArea | OnTriggerStay2D : " + this.AreaType);

    //    if (State != EState.Dark)
    //        State = EState.Select;
    //}

    //void OnTriggerExit2D(Collider2D coll)
    //{
    //    //Debug.Log("CrapsTableArea | OnTriggerExit2D : " + this.AreaType);

    //    if (State != EState.Dark)
    //        State = EState.Normal;

    //}
}
