using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class Tip : MonoBehaviour
{

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Text tipText;

    public void ShowTip(Vector3 pos, string s)
    {
        tipText.transform.GetComponent<RectTransform>().anchoredPosition = pos;
        tipText.transform.GetComponent<RectTransform>().localScale = Vector3.one*0.8f;
        canvasGroup.alpha = 0.0f;

        tipText.text = s;

        Vector3 targetPos = pos + new Vector3(0.0f, 25.0f, 0.0f);

        Sequence sequence = DOTween.Sequence();

        sequence.Insert(0.0f, tipText.transform.GetComponent<RectTransform>().DOLocalMove(targetPos, 0.8f));
        sequence.Insert(0.0f, tipText.transform.GetComponent<RectTransform>().DOScale(Vector3.one, 0.8f));
        sequence.Insert(0.0f, canvasGroup.DOFade(1.0f, 0.5f));

        sequence.Insert(0.8f, canvasGroup.DOFade(0.0f, 1.5f));

    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
