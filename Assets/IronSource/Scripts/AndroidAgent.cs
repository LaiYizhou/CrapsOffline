﻿#if UNITY_ANDROID
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AndroidAgent : IronSourceIAgent
{
	private static AndroidJavaObject _androidBridge; 
	private readonly static string AndroidBridge = "com.ironsource.unity.androidbridge.AndroidBridge";
	private const string REWARD_AMOUNT = "reward_amount";
	private const string REWARD_NAME = "reward_name";
	private const string PLACEMENT_NAME = "placement_name";

	public AndroidAgent ()
	{
		Debug.Log ("AndroidAgent ctr");
	}
	
#region IronSourceIAgent implementation
	private AndroidJavaObject getBridge ()
	{
		if (_androidBridge == null)
			using (var pluginClass = new AndroidJavaClass( AndroidBridge ))
				_androidBridge = pluginClass.CallStatic<AndroidJavaObject> ("getInstance");
		
		return _androidBridge;
	}

	public void reportAppStarted ()
	{
		getBridge ().Call ("reportAppStarted");
	}

	//******************* Base API *******************//

	public void onApplicationPause (bool pause)
	{
		if (pause) 
		{
			getBridge ().Call ("onPause");
		}
		else
		{                   
			getBridge ().Call ("onResume");
		}
	}

	public void setAge (int age)
	{
		getBridge ().Call ("setAge", age);
	}

	public void setGender (string gender)
	{
		getBridge ().Call ("setGender", gender);
	}

	public void setMediationSegment (string segment)
	{
		getBridge ().Call ("setMediationSegment", segment);
	}

	public string getAdvertiserId ()
	{
		return getBridge ().Call<string> ("getAdvertiserId");
	}

	public void validateIntegration ()
	{
		getBridge ().Call ("validateIntegration");
	}

	public void shouldTrackNetworkState (bool track)
	{
		getBridge ().Call ("shouldTrackNetworkState", track);
	}

	public bool setDynamicUserId (string dynamicUserId)
	{
		return getBridge ().Call<bool> ("setDynamicUserId", dynamicUserId);
	}

	public void setAdaptersDebug(bool enabled)
	{
		getBridge ().Call ("setAdaptersDebug", enabled);
	}

	//******************* SDK Init *******************//

	public void setUserId(string userId) {
		getBridge ().Call ("setUserId", userId);
	}

	public void init(string appKey)
	{
		getBridge ().Call ("setPluginData", "Unity", IronSource.pluginVersion (), IronSource.unityVersion ());
		getBridge ().Call ("init", appKey);
	}

	public void init (string appKey, params string[] adUnits)
	{
		getBridge ().Call ("setPluginData", "Unity", IronSource.pluginVersion (), IronSource.unityVersion ());
		getBridge ().Call ("init", appKey, adUnits);
	}

	public void initISDemandOnly (string appKey, params string[] adUnits)
	{
		getBridge ().Call ("setPluginData", "Unity", IronSource.pluginVersion (), IronSource.unityVersion ());
		getBridge ().Call ("initISDemandOnly", appKey, adUnits);
	}

	//******************* RewardedVideo API *******************//

	public void showRewardedVideo ()
	{
		getBridge ().Call ("showRewardedVideo");
	}

	public void showRewardedVideo (string placementName)
	{
		getBridge ().Call ("showRewardedVideo", placementName);
	}

	public bool isRewardedVideoAvailable ()
	{
		return getBridge ().Call<bool> ("isRewardedVideoAvailable");
	}

	public bool isRewardedVideoPlacementCapped (string placementName)
	{
		return getBridge ().Call<bool> ("isRewardedVideoPlacementCapped", placementName);
	}

	public IronSourcePlacement getPlacementInfo (string placementName)
	{
		string placementInfo = getBridge ().Call<string> ("getPlacementInfo", placementName);
		IronSourcePlacement pInfo = null;
		if (placementInfo != null) {
			Dictionary<string,object> pInfoDic = IronSourceJSON.Json.Deserialize (placementInfo) as Dictionary<string,object>;
			string pName = pInfoDic [PLACEMENT_NAME].ToString ();
			string rName = pInfoDic [REWARD_NAME].ToString ();
			int rAmount = Convert.ToInt32 (pInfoDic [REWARD_AMOUNT].ToString ());

			pInfo = new IronSourcePlacement (pName, rName, rAmount);		
		}
		return pInfo;
	}

    public void setRewardedVideoServerParams(Dictionary<string, string> parameters) {
		string json = IronSourceJSON.Json.Serialize (parameters);
		getBridge ().Call ("setRewardedVideoServerParams", json);
    }

    public void clearRewardedVideoServerParams() {
		getBridge ().Call ("clearRewardedVideoServerParams");
    }

	//******************* RewardedVideo DemandOnly API *******************//

	public void showISDemandOnlyRewardedVideo (string instanceId)
	{
		getBridge ().Call ("showISDemandOnlyRewardedVideo",instanceId);
	}

	public void showISDemandOnlyRewardedVideo (string instanceId, string placementName)
	{
		getBridge ().Call ("showISDemandOnlyRewardedVideo", instanceId, placementName);
	}

	public bool isISDemandOnlyRewardedVideoAvailable (string instanceId)
	{
		return getBridge ().Call<bool> ("isISDemandOnlyRewardedVideoAvailable", instanceId);
	}

	//******************* Interstitial API *******************//

	public void loadInterstitial ()
	{
		getBridge ().Call ("loadInterstitial");
	}

	public void showInterstitial ()
	{
		getBridge ().Call ("showInterstitial");
	}

	public void showInterstitial (string placementName)
	{
		getBridge ().Call ("showInterstitial", placementName);
	}

	public bool isInterstitialReady ()
	{
		return getBridge ().Call<bool> ("isInterstitialReady");
	}

	public bool isInterstitialPlacementCapped (string placementName)
	{
		return getBridge ().Call<bool> ("isInterstitialPlacementCapped", placementName);
	}

	//******************* Interstitial DemandOnly API *******************//

	public void loadISDemandOnlyInterstitial (string instanceId)
	{
		getBridge ().Call ("loadISDemandOnlyInterstitial",instanceId);
	}

	public void showISDemandOnlyInterstitial (string instanceId)
	{
		getBridge ().Call ("showISDemandOnlyInterstitial",instanceId);
	}

	public void showISDemandOnlyInterstitial (string instanceId, string placementName)
	{
		getBridge ().Call ("showISDemandOnlyInterstitial", instanceId, placementName);
	}

	public bool isISDemandOnlyInterstitialReady (string instanceId)
	{
		return getBridge ().Call<bool> ("isISDemandOnlyInterstitialReady",instanceId);
	}

	//******************* Offerwall API *******************//

	public void showOfferwall ()
	{
		getBridge ().Call ("showOfferwall");
	}

	public void showOfferwall (string placementName)
	{
		getBridge ().Call ("showOfferwall", placementName);
	}

	public void getOfferwallCredits ()
	{
		getBridge ().Call ("getOfferwallCredits");
	}

	public bool isOfferwallAvailable ()
	{
		return getBridge ().Call<bool> ("isOfferwallAvailable");
	}

	//******************* Banner API *******************//

	public void loadBanner (IronSourceBannerSize size, IronSourceBannerPosition position)
	{
		getBridge ().Call ("loadBanner", (int)size, (int)position);
	}
	
	public void loadBanner (IronSourceBannerSize size, IronSourceBannerPosition position, string placementName)
	{
		getBridge ().Call ("loadBanner", (int)size, (int)position, placementName);
	}
	
	public void destroyBanner()
	{
		getBridge ().Call ("destroyBanner");
	}

    public void displayBanner()
    {
       getBridge ().Call ("displayBanner");
    }

    public void hideBanner()
    {
       getBridge ().Call ("hideBanner");
    }
	
	public bool isBannerPlacementCapped(string placementName)
	{
		return getBridge ().Call<bool> ("isBannerPlacementCapped", placementName);
	}

	public void setSegment(IronSourceSegment segment)
	{
		Dictionary <string,string> dict = segment.getSegmentAsDict ();
		string json = IronSourceJSON.Json.Serialize (dict);
		getBridge ().Call ("setSegment", json);
	}

	public void setConsent(bool consent)
	{
		getBridge().Call("setConsent",consent);
	}

#endregion
}

#endif

