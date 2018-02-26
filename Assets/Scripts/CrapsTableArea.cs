using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum EArea
{
    PassLine = 0,
    PassOdd,

    Six,
    Eight,

    DontPassH,
    DontPassOdd,
    DontPassV,

    Field,

    Come,
    DontCome,

    Point4,
    Point5,
    Point6,
    Point8,
    Point9,
    Point10,

    Lay4,
    Lay5,
    Lay6,
    Lay8,
    Lay9,
    Lay10,

    PlaceLoss4,
    PlaceLoss5,
    PlaceLoss6,
    PlaceLoss8,
    PlaceLoss9,
    PlaceLoss10,

    DontCome4,
    DontCome5,
    DontCome6,
    DontCome8,
    DontCome9,
    DontCome10,

    Come4,
    Come5,
    Come6,
    Come8,
    Come9,
    Come10,

    PlaceBets4,
    PlaceBets5,
    PlaceBets6,
    PlaceBets8,
    PlaceBets9,
    PlaceBets10,

    Hard7,

    Horn22,
    Horn55,
    Horn33,
    Horn44,
    Horn12,
    Horn56,
    Horn11,
    Horn66,

    AnyCraps,

    Count

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
                image.sprite = selectSprite;
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

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("OnTriggerEnter2D");

        State = EState.Select;
    }

    void OnTriggerStay2D(Collider2D coll)
    {
       Debug.Log("OnTriggerStay2D");

        State = EState.Select;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        Debug.Log("OnTriggerExit2D");

        State = EState.Normal;

    }
}
