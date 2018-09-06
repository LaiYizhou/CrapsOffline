using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;
using UnityEngine.UI;


public class PokerItem : MonoBehaviour
{

    public static Color BlackColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    public static Color RedColor = new Color(231.0f/255.0f, 0.0f, 16.0f/255.0f, 1.0f);

    [SerializeField] private Poker poker;
    public Poker Poker
    {
        get
        {
            return poker;
        }
    }

    [SerializeField] private Color showColor;
    [SerializeField] private bool isShow;
    public bool IsShow
    {
        get
        {
            return isShow;
        }
    }



    [SerializeField] private Image numberImage;
    [SerializeField] private Image colorImage;
    [SerializeField] private Image iconImage;

    [Space(5)]
    [SerializeField] private Image coverImage;
    [SerializeField] private SkeletonGraphic shineEffect;
    [SerializeField] private SkeletonGraphic scratchEffect;

    [Space(5)]
    [SerializeField] private Button pokerButton;



    public void Show()
    {
        if (coverImage != null)
        {
            coverImage.gameObject.SetActive(false);
            isShow = true;
        }

      
    }

    public void Cover()
    {
        if (coverImage != null)
        {
            coverImage.gameObject.SetActive(true);
            isShow = false;
        }

    }

    public void OnPokerButtonClicked()
    {
        if (!isShow)
        {
            pokerButton.interactable = false;
            StartCoroutine(DelayScratchEffect());
        }
     
    }

    IEnumerator DelayScratchEffect()
    {
        shineEffect.gameObject.SetActive(false);

        Show();

        scratchEffect.gameObject.SetActive(true);
        scratchEffect.AnimationState.SetAnimation(0, "animation2", false);

        yield return new WaitForSeconds(1.5f);

        scratchEffect.gameObject.SetActive(false);

        yield return new WaitForEndOfFrame();

        CanvasControl.Instance.gameScratcher.ScratchOnePoker(poker);

    }

    public void Shine()
    {
        shineEffect.gameObject.SetActive(true);
        shineEffect.AnimationState.SetAnimation(0, "animation", false);
    }

    public void Init(Poker p)
    {

        poker = p;

        if (poker.Color == EPokerColor.Heart || poker.Color == EPokerColor.Diamond)
            showColor = RedColor;
        else
            showColor = BlackColor;

        numberImage.sprite = GameHelper.Instance.GetPokerNumberSprite(poker);
        numberImage.color = showColor;

        colorImage.sprite = GameHelper.Instance.GetPokerColorSprite(poker);

        iconImage.sprite = GameHelper.Instance.GetPokerIconSprite(poker);
        iconImage.SetNativeSize();

        if(shineEffect != null)
            shineEffect.gameObject.SetActive(false);

        if(scratchEffect != null)
            scratchEffect.gameObject.SetActive(false);

        Cover();

    }


}

