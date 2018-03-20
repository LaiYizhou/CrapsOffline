using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class GameDailyGift : MonoBehaviour
{
    [SerializeField] private Transform dailyGiftItemsParentTransform;
    [SerializeField] private List<DailyGiftItem> dailyGiftItemList = new List<DailyGiftItem>();
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
    public DateTime LastTime
    {
        get
        {

            if (PlayerPrefs.HasKey("LastTime"))
            {
                string s = PlayerPrefs.GetString("LastTime");
                long l = long.Parse(s);
                return DateTime.FromBinary(l);
            }
            else
            {
                return DateTime.MinValue;
            }

        }
        set
        {
            lastTime = value;
            long l = lastTime.ToBinary();
            string s = l.ToString();
            PlayerPrefs.SetString("LastTime", s);
        }
    }

    [SerializeField] private int loginCount;
    public int LoginCount
    {
        get
        {
            return PlayerPrefs.HasKey("LoginCount") ? PlayerPrefs.GetInt("LoginCount") : 0;
        }

        set
        {
            loginCount = value;
            PlayerPrefs.SetInt("LoginCount", loginCount);
        }
    }

   

    [SerializeField] private Image coverImage;
    [SerializeField] private Transform panel;

	// Use this for initialization
	void Start ()
	{

	    InitDailyGifts();
	    GainDailyGifts();

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
        this.gameObject.SetActive(true);
        coverImage.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();

        sequence.Insert(0.0f, panel.DOScale(Vector3.one * 1.02f, hideDuration));
        sequence.Insert(0.0f, panel.DOLocalMove(new Vector3(0.0f, 0.0f), hideDuration));

        sequence.Insert(hideDuration, panel.DOScale(Vector3.one, scaleDuration));

        
    }

    public void OnCloseButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        Hide();
    }

    private const int SecondsPerDay = 3600;
    private void GainDailyGifts()
    {

        Debug.Log("Last = " + LastTime.ToString()+"  ;  Now = " + DateTime.Now.ToString());

        if (LastTime == DateTime.MinValue)
        {
            LoginCount++;
            GetDailyGift(LoginCount);
            LastTime = DateTime.Now;

            Debug.Log("First Login : " + LoginCount);

            //Show();
        }
        else
        {
            TimeSpan ts = DateTime.Now - LastTime;
            double totalSecond = ts.TotalSeconds;

            Debug.Log("DeltaTime = " + ts.ToString());

            if (totalSecond > 0)
            {
                if (totalSecond >= SecondsPerDay * 1.0 && totalSecond < SecondsPerDay * 2.0)
                {

                    LoginCount++;
                    GetDailyGift(LoginCount);
                    LastTime = DateTime.Now;

                    Debug.Log("Several Login : " + LoginCount);
                    //Show();

                }
                else if (totalSecond >= SecondsPerDay * 2.0)
                {
                    LoginCount = 0;
                    LoginCount++;
                    GetDailyGift(LoginCount);
                    LastTime = DateTime.Now;

                    Debug.Log("Reset Login : " + LoginCount);
                    //Show();

                }
                else
                {
                    for (int i = 0; i < LoginCount; i++)
                        dailyGiftItemList[i].SetMark(true);

                    Debug.Log("Have Logined Today ！！！ " + LoginCount);
                    this.gameObject.SetActive(false);
                    Hide();
                    
                }
            }

        }
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

                    dailyGiftItemList.Add(dailyGiftItem);

                }
            }
            
        }
    }

    public void GetDailyGift(int loginCount)
    {
        int index = (loginCount - 1) % 30;
        
        for(int i = 0; i<index; i++)
            dailyGiftItemList[i].SetMark(true);

        dailyGiftItemList[index].SetImageType(DailyGiftItem.EImageType.Today);

        StartCoroutine(DelayMark(index));
    }

    IEnumerator DelayMark(int index)
    {
        yield return new WaitForSeconds(1.0f);

        dailyGiftItemList[index].Mark();
    }

}
