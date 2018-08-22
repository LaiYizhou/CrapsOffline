using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScaleEffect : MonoBehaviour {

    [SerializeField] private Image image;

    [SerializeField] private bool isScaleEffect;
    [Range(1.05f, 1.15f)] public float scaleFac = 1.09f;
    [Range(0.6f, 2.0f)] public float scaleTime = 0.8f;
    [Range(-1, 10)] public int scaleCount = -1;
    public LoopType loopType = LoopType.Yoyo;

    // Use this for initialization
    void Start () {

        if (image == null)
        {
            image = this.GetComponent<Image>();
        }

        if(image != null && isScaleEffect)
            StartScaleEffect();
    }

    public void SetIsScaleEffect(bool flag)
    {
        this.isScaleEffect = flag;
        UpdateState();
    }

    private void StartScaleEffect()
    {
        image.GetComponent<RectTransform>().DOScale(Vector3.one * scaleFac, scaleTime).SetLoops(scaleCount, loopType);
    }

    private void UpdateState()
    {
        if (isScaleEffect)
        {
            this.GetComponent<RectTransform>().DOKill();
            image.GetComponent<RectTransform>().localScale = Vector3.one;
            StartScaleEffect();
        }
        else
        {
            this.GetComponent<RectTransform>().DOKill();
            image.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
}
