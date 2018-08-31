using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using Random = System.Random;

public class GameDailyGift : MonoBehaviour
{

    [SerializeField] private Transform dailyGiftItemsParentTransform;
    [SerializeField] private List<DailyGiftItem> dailyGiftItemList = new List<DailyGiftItem>();
    private List<int> dailyGiftCoinsList = new List<int>()
    {
        1000, 3000, 5000, 8000, 12000,
        3000, 5000, 7000, 10000, 14000,
        5000, 7000, 9000, 12000, 16000,
        6000, 8000, 10000, 13000, 17000,
        8000, 10000, 12000, 15000, 20000,
        10000, 12000, 14000, 17000, 25000
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
            return PlayerPrefs.HasKey("LoginCount") ? PlayerPrefs.GetInt("LoginCount") : 1;
        }

        set
        {
            loginCount = value;
            PlayerPrefs.SetInt("LoginCount", loginCount);
        }
    }

    [SerializeField] private int gainDailyGiftCount;
    public int GainDailyGiftCount
    {
        get
        {
            return PlayerPrefs.HasKey("GainDailyGiftCount") ? PlayerPrefs.GetInt("GainDailyGiftCount") : 0;
        }

        set
        {
            gainDailyGiftCount = value;
            PlayerPrefs.SetInt("GainDailyGiftCount", gainDailyGiftCount);
        }
    }


    [SerializeField] private Image coverImage;
    [SerializeField] private Transform panel;
    [SerializeField] private Transform originalPosTransform;

    [Space(5)]
    [SerializeField] private RectTransform scrollRectTransform;
    [SerializeField] private RectTransform viewPortRectTransform;

    /// <summary>
    /// has been rewareded in current open
    /// </summary>
    private bool isCurrentDailyGift;

    // Use this for initialization
    void Start ()
	{

	    InitDailyGifts();
	    GainDailyGifts();
	    

	}


    private float scaleDuration = 0.2f;
    private float hideDuration = 0.2f;
    
    public void Hide()
    {
        
        coverImage.gameObject.SetActive(false);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(panel.DOScale(Vector3.one * 1.02f, scaleDuration));

        sequence.Insert(scaleDuration, panel.DOScale(Vector3.one * 0.0f, hideDuration));

        sequence.Insert(scaleDuration, panel.DOLocalMove(originalPosTransform.localPosition, hideDuration));

        sequence.AppendCallback(() =>
        {
            this.gameObject.SetActive(false);

            Debug.Log("GainDailyGiftCount = " + GainDailyGiftCount + " ; isCurrentDailyGift = " + isCurrentDailyGift);

            if (GainDailyGiftCount > 1 && isCurrentDailyGift)
            {
                int p = UnityEngine.Random.Range(0, 100);
                if (p < GameHelper.GainDailyGifts_LoginSalePromotion_P)
                {
                    CanvasControl.Instance.gamePromotion.Show(GamePromotion.EPromotionType.LoginSale);
                }

            }

        });

    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        coverImage.gameObject.SetActive(true);
        isCurrentDailyGift = false;

        Sequence sequence = DOTween.Sequence();

        sequence.Insert(0.0f, panel.DOScale(Vector3.one * 1.02f, hideDuration));
        sequence.Insert(0.0f, panel.DOLocalMove(new Vector3(0.0f, 0.0f), hideDuration));

        sequence.Insert(hideDuration, panel.DOScale(Vector3.one, scaleDuration));

        int index = (LoginCount - 1) % 30;
        NevigateToCurrentDay(dailyGiftItemsParentTransform.GetChild(index).GetComponent<RectTransform>());

    }

    public void OnCloseButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        Hide();
    }

    /// <summary>
    /// second
    /// 60 * 60 * 24 seconds in a day
    /// 60 * 15 seconds in 15 minutes
    /// </summary>
    private const int dailyGiftInterval = 60 * 60 * 24;
    private void GainDailyGifts()
    {

        Debug.Log("Last = " + LastTime.ToString()+"  ;  Now = " + DateTime.Now.ToString());

        if (LastTime == DateTime.MinValue)
        {
            LoginCount = 1;
            GetDailyGift(LoginCount, DateTime.Now);
            Debug.Log("First Login : " + LoginCount);
        }
        else
        {
            TimeSpan ts = DateTime.Now - LastTime;
            double totalSecond = ts.TotalSeconds;

            Debug.Log("DeltaTime = " + ts.ToString());

            if (totalSecond > 0)
            {
                if (totalSecond >= dailyGiftInterval * 1.0 && totalSecond < dailyGiftInterval * 2.0)
                {
                    LoginCount++;
                    GetDailyGift(LoginCount, DateTime.Now);
                    Debug.Log("Several Login : " + LoginCount);

                    GameHelper.Instance.RewardedVideoCount = 0;
                }
                else if (totalSecond >= dailyGiftInterval * 2.0)
                {
                    LoginCount = 1;
                    GetDailyGift(LoginCount, DateTime.Now);
                    Debug.Log("Reset Login : " + LoginCount);

                    GameHelper.Instance.RewardedVideoCount = 0;
                }
                else
                {
                    for (int i = 0; i <= (LoginCount - 1) % 30; i++)
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

                    if((i + 1) % 5 == 0)
                        dailyGiftItem.SetImageType(DailyGiftItem.EImageType.FiveDay);

                    dailyGiftItemList.Add(dailyGiftItem);

                }
            }
            
        }
    }

    public void GetDailyGift(int loginCount, DateTime getTime)
    {

        int index = (loginCount - 1) % 30;
        NevigateToCurrentDay(dailyGiftItemsParentTransform.GetChild(index).GetComponent<RectTransform>());

        for (int i = 0; i<index; i++)
            dailyGiftItemList[i].SetMark(true);

        dailyGiftItemList[index].SetImageType(DailyGiftItem.EImageType.Today);

        StartCoroutine(DelayMark(index, getTime));
    }

    IEnumerator DelayMark(int index, DateTime getTime)
    {
        yield return new WaitForSeconds(1.0f);

        dailyGiftItemList[index].Mark();
        isCurrentDailyGift = true;
        LastTime = getTime;
        //LoginCount++;
        GainDailyGiftCount++;
    }

    private void NevigateToCurrentDay(RectTransform target)
    {
        Vector3 currentItemLocalPos = scrollRectTransform.InverseTransformVector(GetWorldPos(target));
        Vector3 targetLocalPos = scrollRectTransform.InverseTransformVector(GetWorldPos(viewPortRectTransform));

        Vector3 diff = targetLocalPos - currentItemLocalPos;
        diff.z = 0.0f;

        var newNormalizedPosition = new Vector2(diff.x / (dailyGiftItemsParentTransform.GetComponent<RectTransform>().rect.width - viewPortRectTransform.rect.width),
            diff.y / (dailyGiftItemsParentTransform.GetComponent<RectTransform>().rect.height - viewPortRectTransform.rect.height));

        newNormalizedPosition = scrollRectTransform.GetComponent<ScrollRect>().normalizedPosition - newNormalizedPosition;

        newNormalizedPosition.x = Mathf.Clamp01(newNormalizedPosition.x);
        newNormalizedPosition.y = Mathf.Clamp01(newNormalizedPosition.y);

        DOTween.To(() => scrollRectTransform.GetComponent<ScrollRect>().normalizedPosition, x => scrollRectTransform.GetComponent<ScrollRect>().normalizedPosition = x, newNormalizedPosition, 0.8f);

    }

    private Vector3 GetWorldPos(RectTransform target)
    {
        var pivotOffset = new Vector3(
            (0.5f - target.pivot.x) * target.rect.size.x,
            (0.5f - target.pivot.y) * target.rect.size.y,
            0f);

        var localPosition = target.localPosition + pivotOffset;

        return target.parent.TransformPoint(localPosition);
    }

}
