using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSourceControl : MonoBehaviour
{

    //#if UNITY_IOS
    //    public static string appKey = "6a4e1445";
    //#elif UNITY_ANDROID
    //    public static string appKey = "6cec3a75";
    //#endif

    public static string appKey = "6ee1d435";

    public static IronSourceControl Instance;
    public static bool IsShowInterstitalWhenBack = true;

    private bool isRewardedVideoReady;

    public bool IsRewardedVideoReady
    {
        get { return isRewardedVideoReady; }
        set
        {
            isRewardedVideoReady = value;
            if (CanvasControl.Instance != null)
                CanvasControl.Instance.UpdateRewardedButton();

        }
    }

    private bool isInterstitialReady;
    public bool IsInterstitialReady
    {
        get { return isInterstitialReady; }
        set
        {
            isInterstitialReady = value;
            if (CanvasControl.Instance != null)
                CanvasControl.Instance.UpdateInterstitial();

        }
        
    }

    // Use this for initialization
    void Start ()
    {

        Instance = this;

	    Debug.Log("MyAppStart Start called");

	    //IronSource tracking sdk
	    IronSource.Agent.reportAppStarted();

	    //Dynamic config example
	    IronSourceConfig.Instance.setClientSideCallbacks(true);

	    string id = IronSource.Agent.getAdvertiserId();
	    Debug.Log("IronSource.Agent.getAdvertiserId : " + id);

	    Debug.Log("IronSource.Agent.validateIntegration");
	    IronSource.Agent.validateIntegration();

	    Debug.Log("unity version" + IronSource.unityVersion());

	    //SDK init
	    Debug.Log("IronSource.Agent.init");
	    IronSource.Agent.setUserId("uniqueUserId");
	    IronSource.Agent.init(appKey);
        //IronSource.Agent.init (appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
        IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL);
        //IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO);


        IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
	    IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
	    IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
	    IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
	    IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
	    IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;

        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;


        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;

        //GameHelper.Instance.gameStart.Progress = Random.Range(0.4f, 0.6f);


        //LoadInterstitial();

    }


    public void DestroyBanner()
    {
        //if (GameHelper.Instance.IsBannerSwitchOn)
        {
            IronSource.Agent.destroyBanner();
            //CanvasControl.Instance.IsShowBanner = false;
        }
    }

    public void ShowBanner()
    {
        //if(GameHelper.Instance.IsBannerSwitchOn)
            IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
    }

    public void LoadInterstitial()
    {

        //if (GameHelper.Instance.IsInterstitialSwitchOn)
        {
            Debug.Log("LoadInterstitial");

            if (!GameHelper.player.IsPaid)
            {
                if (!IronSource.Agent.isInterstitialReady())
                    IronSource.Agent.loadInterstitial();
            }
        }

    }



    public void ShowInterstitial(float p = 1.0f)
    {

        //if (GameHelper.Instance.IsInterstitialSwitchOn)
        {
            Debug.Log("ShowInterstitial");

            if (!GameHelper.player.IsPaid)
            {
                if (IronSource.Agent.isInterstitialReady())
                {
                    if (Random.Range(0.0f, 1.0f) < p)
                        IronSource.Agent.showInterstitial();
                }
                else
                {
                    Debug.Log("IronSource.Agent.isInterstitialReady - False");
                }
            }
        }

    }


    public void ShowRewardedVideoButtonClicked()
    {
        Debug.Log("ShowRewardedVideoButtonClicked");
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            Debug.Log("IronSource.Agent.isRewardedVideoAvailable - False");
        }
    }






    //Invoked once the banner has loaded
    void BannerAdLoadedEvent()
    {
        Debug.Log("I got BannerAdLoaded");
        //GameHelper.Instance.Log("I got BannerAdLoaded");
        //CanvasControl.Instance.IsShowBanner = true;
    }
    //Invoked when the banner loading process has failed.
    //@param description - string - contains information about the failure.
    void BannerAdLoadFailedEvent(IronSourceError error)
    {
        //CanvasControl.Instance.IsShowBanner = false;
        Debug.Log("I got BannerAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
        //GameHelper.Instance.Log("I got BannerAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
        //GameHelper.Instance.IsBannerSwitchOn = false;
    }
    // Invoked when end user clicks on the banner ad
    void BannerAdClickedEvent()
    {
    }
    //Notifies the presentation of a full screen content following user click
    void BannerAdScreenPresentedEvent()
    {
    }
    //Notifies the presented screen has been dismissed
    void BannerAdScreenDismissedEvent()
    {
    }
    //Invoked when the user leaves the app
    void BannerAdLeftApplicationEvent()
    {

    }


    void InterstitialAdReadyEvent()
    {
        Debug.Log("I got InterstitialAdReadyEvent");
        IsInterstitialReady = true;
        //ShowText.GetComponent<UnityEngine.UI.Text>().color = UnityEngine.Color.blue;
    }

    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        Debug.Log("I got InterstitialAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
        LoadInterstitial();
    }

    void InterstitialAdShowSucceededEvent()
    {
        Debug.Log("I got InterstitialAdShowSucceededEvent");
        IsInterstitialReady = false;
        IsShowInterstitalWhenBack = false;
        AudioControl.Instance.StopBgMusic();
        //ShowText.GetComponent<UnityEngine.UI.Text>().color = UnityEngine.Color.red;
    }

    void InterstitialAdShowFailEvent(IronSourceError error)
    {
        Debug.Log("I got InterstitialAdShowFailEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
        //ShowText.GetComponent<UnityEngine.UI.Text>().color = UnityEngine.Color.red;
    }

    void InterstitialAdClickedEvent()
    {
        Debug.Log("I got InterstitialAdClickedEvent");
    }

    void InterstitialAdOpenedEvent()
    {
        Debug.Log("I got InterstitialAdOpenedEvent");
    }

    void InterstitialAdClosedEvent()
    {
        Debug.Log("I got InterstitialAdClosedEvent");

        AudioControl.Instance.PlayBgMusic();

        int index = Random.Range(0, 100);
        if (index < GameHelper.CloseInsterstitial_RemoveAdPromotion_P)
        {
            CanvasControl.Instance.gamePromotion.Show(GamePromotion.EPromotionType.RemoveAd);
        }

        LoadInterstitial();
    }



    void RewardedVideoAvailabilityChangedEvent(bool canShowAd)
    {
        Debug.Log("I got RewardedVideoAvailabilityChangedEvent, value = " + canShowAd);
        IsRewardedVideoReady = canShowAd;

        if (canShowAd)
        {
            //ShowText.GetComponent<UnityEngine.UI.Text>().color = UnityEngine.Color.blue;
           
        }
        else
        {
            //ShowText.GetComponent<UnityEngine.UI.Text>().color = UnityEngine.Color.red;
        }
    }

    void RewardedVideoAdOpenedEvent()
    {
        Debug.Log("I got RewardedVideoAdOpenedEvent");

        CanvasControl.Instance.gameRewardedVideo.gameObject.SetActive(false);
        AudioControl.Instance.StopBgMusic();
    }

    void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
    {
        Debug.Log("I got RewardedVideoAdRewardedEvent, amount = " + ssp.getRewardAmount() + " name = " + ssp.getRewardName());
        //userTotalCredits = userTotalCredits + ssp.getRewardAmount();
        //GameHelper.player.ChangeCoins(ssp.getRewardAmount());
        GameHelper.IsShowRewardedVideoCoins = true;
        IsShowInterstitalWhenBack = false;

        GameHelper.RewardedVideoCoin = ssp.getRewardAmount();
        //AmountText.GetComponent<UnityEngine.UI.Text>().text = "" + userTotalCredits;
    }

    void RewardedVideoAdClosedEvent()
    {
        Debug.Log("I got RewardedVideoAdClosedEvent");

        GameHelper.Instance.RewardedVideoCount++;
        Debug.Log("### GameHelper.Instance.RewardedVideoCount = " + GameHelper.Instance.RewardedVideoCount);

        AudioControl.Instance.PlayBgMusic();

        GameHelper.Instance.coinCollectEffect.RunEffect(GameHelper.RewardedVideoCoin,
            new Vector3(0.0f, 0.0f),
            new Vector3(-95.0f, 100.0f),
            new Vector3(-140.0f, 190));

        AppsFlyerManager.Instance.TrackAd(GameHelper.RewardedVideoCoin.ToString(), "1");

        
    }

    void RewardedVideoAdStartedEvent()
    {
        Debug.Log("I got RewardedVideoAdStartedEvent");
    }

    void RewardedVideoAdEndedEvent()
    {
        Debug.Log("I got RewardedVideoAdEndedEvent");
    }

    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
        Debug.Log("I got RewardedVideoAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void RewardedVideoAdClickedEvent(IronSourcePlacement ssp)
    {
        Debug.Log("I got RewardedVideoAdClickedEvent, name = " + ssp.getRewardName());
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnApplicationPause(bool isPaused)
    {
        //Debug.Log("OnApplicationPause = " + isPaused);
        //if (isPaused)
        //{
        //    if (IronSourceControl.Instance != null)
        //        IronSourceControl.Instance.LoadInterstitial();
        //}
        //else
        //{
        //    Debug.Log("IsShowInterstitalWhenBack = " + IsShowInterstitalWhenBack);
        //    if (IsShowInterstitalWhenBack)
        //    {
        //        if (IronSourceControl.Instance != null)
        //            IronSourceControl.Instance.ShowInterstitial(0.5f);
        //    }
        //    else
        //    {
        //        IsShowInterstitalWhenBack = true;
        //    }
        //}

    }
}
