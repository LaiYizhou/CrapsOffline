using UnityEngine;
using System.Collections;
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
    private Vector3 originalPos;

    [SerializeField] private Image chipImage;
    [SerializeField] private EChip ChipType;
    [SerializeField] private long value;


    public void Init(EChip eChip)
    {
        ChipType = eChip;
        value = GameHelper.Instance.GetChipValue(eChip);
        chipImage.sprite = GameHelper.Instance.GetChipSprite(eChip);
    }

    void Awake()
    {

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        originalPos = this.GetComponent<RectTransform>().anchoredPosition;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //cg.blocksRaycasts = false;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(this.GetComponent<RectTransform>(), Input.mousePosition, canvas.worldCamera, out newPosition);

        transform.position = newPosition + new Vector3(0.0f, 20.0f, 0.0f);
        //transform.position = newPosition;
        transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        //cg.blocksRaycasts = true;

        //if (CanvasControl.Instance.imageControl.IsCanFill())
        {
            //CanvasControl.Instance.imageControl.FillImage(this.gameObject.GetComponent<ShapeItem>());
            //StartCoroutine(DelayDestroy());
        }
        //else
        {
            //int sourceIndex = this.gameObject.GetComponent<ShapeItem>().SourceIndex;

            transform.localPosition = originalPos;
            transform.localScale = Vector3.one;
        }

    }
}
