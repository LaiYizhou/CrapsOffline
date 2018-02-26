using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Chip : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    //记录鼠标位置.
    Vector3 newPosition;

    private Canvas canvas;
    private Vector3 originalPos;

    [SerializeField]
    private long value;


    private void InitChip(long value)
    {
        this.value = value;
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
        //拖拽开始时记下自己的父物体.
        //myParent = transform.parent;

        //拖拽开始时禁用检测.
        //cg.blocksRaycasts = false;

        //this.transform.SetParent(tempParent);
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
