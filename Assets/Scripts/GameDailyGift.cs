using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class GameDailyGift : MonoBehaviour
{
    [SerializeField] private Transform dailyGiftItemsParentTransform;
    private List<int> dailyGiftCoinsList = new List<int>()
    {
        1000, 3000, 3000, 5000, 10000,
        3000, 5000, 5000, 6000, 15000,
        5000, 6000, 6000, 8000, 20000,
        6000, 8000, 8000, 10000, 25000,
        8000, 10000, 10000, 12000, 30000,
        10000, 12000, 12000, 15000, 50000
    };

    private DateTime lastTime;

    [SerializeField] private int SaverInt;
    [SerializeField] private List<bool> SaverList;

    [SerializeField] private Image coverImage;
    [SerializeField] private Transform panel;

	// Use this for initialization
	void Start ()
	{

        //lastTime.ToString();
	    //GetDailyGift(3);

        DateTime dt1 = new DateTime(2018, 3, 13);
        DateTime dt2 = new DateTime(2018, 3, 12);

        Debug.Log((dt2 - dt1).TotalSeconds);

        //Debug.Log(lastTime);

	    InitDailyGifts();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private float scaleDuration = 0.2f;
    private float hideDuration = 0.2f;

    public void Hide()
    {

        coverImage.gameObject.SetActive(false);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(panel.DOScale(Vector3.one * 1.02f, scaleDuration));

        sequence.Insert(scaleDuration, panel.DOScale(Vector3.one * 0.0f, hideDuration));
        sequence.Insert(scaleDuration, panel.DOLocalMove(new Vector3(-480.0f, 284.0f, 0.0f), hideDuration));

    }

    public void Show()
    {
        coverImage.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();

        sequence.Insert(0.0f, panel.DOScale(Vector3.one * 1.02f, hideDuration));
        sequence.Insert(0.0f, panel.DOLocalMove(Vector3.zero, hideDuration));

        sequence.Insert(hideDuration, panel.DOScale(Vector3.one, scaleDuration));

        
    }

    public void OnCloseButtonClicked()
    {
        Hide();
    }

    private void InitDailyGifts()
    {
        if (dailyGiftCoinsList.Count == dailyGiftItemsParentTransform.childCount)
        {
            for (int i = 0; i < dailyGiftCoinsList.Count; i++)
            {
                DailyGiftItem dailyGiftItem = dailyGiftItemsParentTransform.GetChild(i).GetComponent<DailyGiftItem>();

                if (dailyGiftItem != null)
                {
                    dailyGiftItem.SetImageType(DailyGiftItem.EImageType.Normal);
                    dailyGiftItem.SetText(i+1, dailyGiftCoinsList[i]);
                    dailyGiftItem.SetMark(false);

                    if((i+1)%5 == 0)
                        dailyGiftItem.SetImageType(DailyGiftItem.EImageType.FiveDay);
                }
            }
            
        }
    }

    public void GetDailyGift(int index)
    {
        if (index <= 30)
        {
            //int res = 0;

            int val = 1 << index;

            if (!SaverList[index])
            {
                SaverList[index] = true;
                SaverInt = SaverInt | val;
            }

        }
    }

}
