using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{

    [SerializeField]
    private Image image;
    [Range(1.05f, 1.15f)]
    public float scale = 1.09f;

    // Use this for initialization
	void Start ()
	{
	    if (image == null)
	    {
	        image = this.GetComponent<Image>();
	    }

	    this.GetComponent<Button>().onClick.AddListener(OnClicked);
	}

    private void OnClicked()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(image.GetComponent<RectTransform>().DOScale(new Vector3(scale, scale, scale), 0.2f));
        sequence.AppendCallback(() =>
        {
            image.GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f);
        });



    }

    // Update is called once per frame
    void Update () {
		
	}
}
