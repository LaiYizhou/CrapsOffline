using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameHourlyGift : MonoBehaviour
{

    private const int HourlyGiftCoin = 1000;

    /// <summary>
    /// second
    /// 60 * 60 seconds in an hour
    /// 60 * 5 seconds in 5 minutes
    /// </summary>
    private const int HourlyGiftInterval = 60 * 60;

    [SerializeField] private Button hourlyGiftButton;
    [SerializeField] private Sprite readySprite;
    [SerializeField] private Sprite notReadySprite;
    [SerializeField] private Text hourlyGiftTimeText;
    [SerializeField] private ParticleSystem hourlyGiftButtonParticleSystem;

    private DateTime lastGetHourlyGiftTime;
    public DateTime LastGetHourlyGiftTime
    {
        get
        {

            if (PlayerPrefs.HasKey("LastGetHourlyGiftTime"))
            {
                string s = PlayerPrefs.GetString("LastGetHourlyGiftTime");
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
            lastGetHourlyGiftTime = value;
            long l = lastGetHourlyGiftTime.ToBinary();
            string s = l.ToString();
            PlayerPrefs.SetString("LastGetHourlyGiftTime", s);
        }
    }


    [SerializeField]
    private int gainHourlyGiftCount;
    public int GainHourlyGiftCount
    {
        get
        {
            return PlayerPrefs.HasKey("GainHourlyGiftCount") ? PlayerPrefs.GetInt("GainHourlyGiftCount") : 0;
        }

        set
        {
            gainHourlyGiftCount = value;
            PlayerPrefs.SetInt("GainHourlyGiftCount", gainHourlyGiftCount);
        }
    }

    public DateTime NextGetHourlyGiftTime
    {
        get { return LastGetHourlyGiftTime + TimeSpan.FromSeconds(HourlyGiftInterval); }
    }

    // Use this for initialization
    void Start ()
    {

        hourlyGiftButton.onClick.AddListener(OnHourlyGiftButtonClicked);

        InitHourlyGift();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitHourlyGift()
    {
        TimeSpan ts = DateTime.Now - NextGetHourlyGiftTime;
        double totalSecond = ts.TotalSeconds;

        if (totalSecond > 0)
        {
            SetButtonActive();
        }
        else
        {
            SetButtonInActive();
            StartCoroutine(HourlyGiftTimeDown());
        }
    }



    private void OnHourlyGiftButtonClicked()
    {

        GainHourlyGiftCount++;

        SetButtonInActive();

        LastGetHourlyGiftTime = DateTime.Now;
        ShowHourlyGiftTimeText(HourlyGiftInterval);

        StartCoroutine(HourlyGiftTimeDown());


        //GameHelper.Instance.ShowAddCoins(HourlyGiftCoin, false);
        GameHelper.Instance.coinCollectEffect.RunEffect(HourlyGiftCoin, 
            new Vector3(432.0f, -254.0f),
            new Vector3(35.0f, -130.0f),
            new Vector3(-137.0f, 80.0f));


        if (GainHourlyGiftCount > 1)
        {
            int index = Random.Range(0, 100);
            if (index > 50)
            {
                StartCoroutine(DelayShowInterstitial());
            }

        }

    }

    IEnumerator DelayShowInterstitial()
    {
        yield return new WaitForSeconds(2.3f);

        IronSourceControl.Instance.ShowInterstitial();
    }

    private IEnumerator HourlyGiftTimeDown()
    {
        TimeSpan ts = DateTime.Now - NextGetHourlyGiftTime;
        double totalSecond = ts.TotalSeconds;

        if (totalSecond > 0)
        {
            SetButtonActive();
             yield break;
        }
        else
        {
           
            totalSecond = -1 * totalSecond;

            ShowHourlyGiftTimeText((int)totalSecond);

            yield return new WaitForSecondsRealtime(1.0f);
            StartCoroutine(HourlyGiftTimeDown());


        }
   
    }

    private void ShowHourlyGiftTimeText(int totalSecond)
    {
        hourlyGiftTimeText.gameObject.SetActive(true);

        TimeSpan temp = new TimeSpan(0, 0, totalSecond);
        hourlyGiftTimeText.text = string.Format("{0}:{1}",
            temp.Minutes < 10 ? string.Format("0{0}", temp.Minutes.ToString()) : temp.Minutes.ToString(),
            temp.Seconds < 10 ? string.Format("0{0}", temp.Seconds.ToString()) : temp.Seconds.ToString());
    }

    private void SetButtonActive()
    {
        hourlyGiftButtonParticleSystem.gameObject.SetActive(true);

        hourlyGiftTimeText.gameObject.SetActive(false);
        hourlyGiftButton.interactable = true;
        hourlyGiftButton.GetComponent<Image>().sprite = readySprite;

        hourlyGiftButton.GetComponent<ButtonScaleEffect>().SetIsScaleEffect(true);

    }

    private void SetButtonInActive()
    {

        hourlyGiftButtonParticleSystem.gameObject.SetActive(false);

        hourlyGiftButton.interactable = false;
        hourlyGiftButton.GetComponent<Image>().sprite = notReadySprite;

        hourlyGiftButton.GetComponent<ButtonScaleEffect>().SetIsScaleEffect(false);
    }

}
