using UnityEngine;
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

    private Canvas canvas;
    public Vector3 OriginalPos { get; private set; }

    [SerializeField] private CrapsTableArea touchedCrapsTableArea;

    [SerializeField] private Image chipImage;
    [SerializeField] private EChip ChipType;
    [SerializeField] private long value;



    public void Init(Chip chip, Vector3 originalPos)
    {
        Init(chip.ChipType);
        OriginalPos = originalPos;
    }

    public void Init(EChip eChip)
    {
        ChipType = eChip;
        value = GameHelper.Instance.GetChipValue(eChip);
        chipImage.sprite = GameHelper.Instance.GetChipSprite(eChip);
    }

    void Awake()
    {

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        OriginalPos = this.GetComponent<RectTransform>().anchoredPosition;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        CanvasControl.Instance.crapsTableAreaManager.ShowAllUIs();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //cg.blocksRaycasts = false;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(this.GetComponent<RectTransform>(), Input.mousePosition, canvas.worldCamera, out newPosition);

        transform.position = newPosition + GameHelper.ChipOnDragPosOffset;
        transform.localScale = GameHelper.ChipOnDragScale;

       
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        //Debug.Log("JustNow Pos : " + this.transform.localPosition);

        if (touchedCrapsTableArea != null && touchedCrapsTableArea.State != EState.Dark)
        {
            CanvasControl.Instance.chipsManager.BuildTableChip(this.transform.localPosition, this);
        }

        transform.localPosition = OriginalPos;
        transform.localScale = Vector3.one;
        CanvasControl.Instance.crapsTableAreaManager.ResetAllUIs();
    }

    public void TakeBack()
    {
        StartCoroutine(DelayDestroy());
        this.GetComponent<RectTransform>().DOLocalMove(OriginalPos, 0.5f);
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Chip | OnTriggerEnter2D : " + coll.name);

        touchedCrapsTableArea = coll.gameObject.GetComponent<CrapsTableArea>();

        if (touchedCrapsTableArea.State != EState.Dark)
            touchedCrapsTableArea.State = EState.Select;
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        Debug.Log("Chip | OnTriggerStay2D : " + coll.name);

        touchedCrapsTableArea = coll.gameObject.GetComponent<CrapsTableArea>();

        if (touchedCrapsTableArea.State != EState.Dark)
            touchedCrapsTableArea.State = EState.Select;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        Debug.Log("Chip | OnTriggerExit2D : " + coll.name);

        touchedCrapsTableArea = coll.gameObject.GetComponent<CrapsTableArea>();
        if (touchedCrapsTableArea.State != EState.Dark)
            touchedCrapsTableArea.State = EState.Normal;

        touchedCrapsTableArea = null;
       
    }
}
