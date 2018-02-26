using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{

    [SerializeField]
    private Button button;
    [Range(1.05f, 1.15f)]
    public float scale = 1.09f;

    // Use this for initialization
	void Start ()
	{

	    button = this.GetComponent<Button>();
        button.onClick.AddListener(OnClicked);
	}

    private void OnClicked()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(button.GetComponent<RectTransform>().DOScale(new Vector3(scale, scale, scale), 0.2f));
        sequence.AppendCallback(() =>
        {
            button.GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f);
        });



    }

    // Update is called once per frame
    void Update () {
		
	}
}
