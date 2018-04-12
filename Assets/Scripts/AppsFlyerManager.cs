using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AppsFlyerManager : MonoBehaviour {

    public static AppsFlyerManager Instance;

    // Use this for initialization
    void Start ()
    {

        Instance = this;

        Init();

	}

    private void Init()
    {
        Application.runInBackground = true;
        DontDestroyOnLoad(this);

    #if UNITY_IOS

		AppsFlyer.setAppsFlyerKey ("c6S7FBQfBhj7Do6WTG4jte");
		AppsFlyer.setAppID ("1358980931");
		//AppsFlyer.setIsDebug (true);
		AppsFlyer.getConversionData ();
		AppsFlyer.trackAppLaunch ();


    #elif UNITY_ANDROID

		AppsFlyer.init ("c6S7FBQfBhj7Do6WTG4jte");
		//AppsFlyer.setAppID ("YOUR_APP_ID"); 

		// for getting the conversion data
		AppsFlyer.loadConversionData("StartUp");

		// for in app billing validation
        // AppsFlyer.createValidateInAppListener ("AppsFlyerTrackerCallbacks", "onInAppBillingSuccess", "onInAppBillingFailure"); 
		//For Android Uninstall
		//AppsFlyer.setGCMProjectNumber ("YOUR_GCM_PROJECT_NUMBER");

    #endif
    }


    // Update is called once per frame
    //   void Update () {
    //	if (Input.GetKeyDown (KeyCode.Escape)) {
    //		//go to background when pressing back button
    //		#if UNITY_ANDROID
    //		AndroidJavaObject activity = 
    //			new AndroidJavaClass("com.unity3d.player.UnityPlayer")
    //				.GetStatic<AndroidJavaObject>("currentActivity");
    //		activity.Call<bool>("moveTaskToBack", true);
    //		#endif
    //	}


    //	#if UNITY_IOS 
    //	if (!tokenSent) { 
    //		byte[] token = UnityEngine.iOS.NotificationServices.deviceToken;           
    //		if (token != null) {     
    //		//For iOS uninstall
    //			AppsFlyer.registerUninstall (token);
    //			tokenSent = true;
    //		}
    //	}    
    //	#endif
    //}

    //A custom event tracking

    public void TrackIAP(string price, string count)
    {

        Dictionary<string, string> purchaseEvent = new Dictionary<string, string>();
        purchaseEvent.Add("af_currency", "USD");
        purchaseEvent.Add("af_revenue", price);
        purchaseEvent.Add("af_quantity", count);
        AppsFlyer.trackRichEvent("af_purchase", purchaseEvent);

        //AFInAppEvents.LEVEL_ACHIEVED

    }

    public void TrackAd(string getCoin, string count)
    {
        Dictionary<string, string> adEvent = new Dictionary<string, string>();
        adEvent.Add("af_revenue", "0");
        adEvent.Add("af_quantity", count);
        AppsFlyer.trackRichEvent("af_param_1", adEvent);

        //AppsFlyer.trackEvent(AFInAppEvents.LOGIN);
    }

    //On Android ou can call the conversion data directly from your CS file, or from the default AppsFlyerTrackerCallbacks
    public void didReceiveConversionData(string conversionData) {
		print ("AppsFlyerTrackerCallbacks:: got conversion data = " + conversionData);
	}
	public void didReceiveConversionDataWithError(string error) {
		print ("AppsFlyerTrackerCallbacks:: got conversion data error = " + error);
	}

	public void onAppOpenAttribution(string validateResult) {
		print ("AppsFlyerTrackerCallbacks:: got onAppOpenAttribution  = " + validateResult);
	}

	public void onAppOpenAttributionFailure (string error) {
		print ("AppsFlyerTrackerCallbacks:: got onAppOpenAttributionFailure error = " + error);
	}

}
