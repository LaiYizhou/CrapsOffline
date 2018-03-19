#if UNITY_IPHONE || UNITY_IOS
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System;

public class iOSAgent : IronSourceIAgent
{
	[DllImport("__Internal")]
	private static extern void CFReportAppStarted ();

	[DllImport("__Internal")]
	private static extern void CFSetPluginData (string pluginType, string pluginVersion, string pluginFrameworkVersion);

	[DllImport("__Internal")]
	private static extern void CFSetAge (int age);

	[DllImport("__Internal")]
	private static extern void CFSetGender (string gender);

	[DllImport("__Internal")]
	private static extern void CFSetMediationSegment (string segment);

	[DllImport("__Internal")]
	private static extern string CFGetAdvertiserId ();
	
	[DllImport("__Internal")]
	private static extern void CFValidateIntegration ();
	
	[DllImport("__Internal")]
	private static extern void CFShouldTrackNetworkState (bool track);

	[DllImport("__Internal")]
	private static extern bool CFSetDynamicUserId (string dynamicUserId);

	[DllImport("__Internal")]
	private static extern void CFSetAdaptersDebug (bool enabled);

	//******************* SDK Init *******************//

	[DllImport("__Internal")]
	private static extern void CFSetUserId (string userId);

	[DllImport("__Internal")]
	private static extern void CFInit (string appKey);

	[DllImport("__Internal")]
	private static extern void CFInitWithAdUnits (string appKey, params string[] adUnits);

	[DllImport("__Internal")]
	private static extern void CFInitISDemandOnly (string appKey, params string[] adUnits);

	//******************* RewardedVideo API *******************//

	[DllImport("__Internal")]
	private static extern void CFShowRewardedVideo ();

	[DllImport("__Internal")]
	private static extern void CFShowRewardedVideoWithPlacementName (string placementName);

	[DllImport("__Internal")]
	private static extern bool CFIsRewardedVideoAvailable ();

	[DllImport("__Internal")]
	private static extern bool CFIsRewardedVideoPlacementCapped (string placementName);

	[DllImport("__Internal")]
	private static extern string CFGetPlacementInfo (string placementName);

	[DllImport("__Internal")]
	private static extern void CFSetRewardedVideoServerParameters (string jsonString);

	[DllImport("__Internal")]
	private static extern void CFClearRewardedVideoServerParameters ();

	//******************* RewardedVideo DemandOnly API *******************//

	[DllImport("__Internal")]
	private static extern void CFShowISDemandOnlyRewardedVideo (string instanceId);

	[DllImport("__Internal")]
	private static extern void CFShowISDemandOnlyRewardedVideoWithPlacementName (string instanceId, string placementName);

	[DllImport("__Internal")]
	private static extern bool CFIsDemandOnlyRewardedVideoAvailable (string instanceId);
	
	//******************* Interstitial API *******************//

	[DllImport("__Internal")]
	private static extern void CFLoadInterstitial ();

	[DllImport("__Internal")]
	private static extern void CFShowInterstitial ();

	[DllImport("__Internal")]
	private static extern void CFShowInterstitialWithPlacementName (string placementName);

	[DllImport("__Internal")]
	private static extern bool CFIsInterstitialReady ();

	[DllImport("__Internal")]
	private static extern bool CFIsInterstitialPlacementCapped (string placementName);

	//******************* Interstitial DemandOnly API *******************//

	[DllImport("__Internal")]
	private static extern void CFLoadISDemandOnlyInterstitial (string instanceId);

	[DllImport("__Internal")]
	private static extern void CFShowISDemandOnlyInterstitial(string instanceId);

	[DllImport("__Internal")]
	private static extern void CFShowISDemandOnlyInterstitialWithPlacementName (string instanceId, string placementName);

	[DllImport("__Internal")]
	private static extern bool CFIsDemandOnlyInterstitialReady (string instanceId);


	//******************* Offerwall API *******************//

	[DllImport("__Internal")]
	private static extern void CFShowOfferwall ();

	[DllImport("__Internal")]
	private static extern void CFShowOfferwallWithPlacementName (string placementName);

	[DllImport("__Internal")]
	private static extern void CFGetOfferwallCredits ();

	[DllImport("__Internal")]
	private static extern bool CFIsOfferwallAvailable ();

	//******************* Banner API *******************//

	[DllImport("__Internal")]
	private static extern void CFLoadBanner (int size, int position);
	
	[DllImport("__Internal")]
	private static extern void CFLoadBannerWithPlacementName (int size, int position, string placementName);
	
	[DllImport("__Internal")]
	private static extern void CFDestroyBanner ();

	[DllImport("__Internal")]
	private static extern void CFDisplayBanner ();

	[DllImport("__Internal")]
	private static extern void CFHideBanner ();
	
	[DllImport("__Internal")]
	private static extern bool CFIsBannerPlacementCapped (string placementName);

	[DllImport("__Internal")]
	private static extern void CFSetSegment(string json);


	public iOSAgent ()
	{	
	}

	#region IronSourceIAgent implementation
	public void reportAppStarted ()
	{
		CFReportAppStarted ();
	}

	//******************* Base API *******************//

	public void onApplicationPause (bool pause)
	{

	}

	public void setAge (int age)
	{
		CFSetAge (age);
	}
	
	public void setGender (string gender)
	{
		CFSetGender (gender);
	}

	public void setMediationSegment (string segment)
	{
		CFSetMediationSegment (segment);
	}

	public string getAdvertiserId ()
	{
		return CFGetAdvertiserId ();
	}
	
	public void validateIntegration ()
	{
		CFValidateIntegration ();
	}
	
	public void shouldTrackNetworkState (bool track)
	{
		CFShouldTrackNetworkState (track);
	}

	public bool setDynamicUserId (string dynamicUserId)
	{
		return CFSetDynamicUserId (dynamicUserId);
	}

	public void setAdaptersDebug(bool enabled)
	{
		CFSetAdaptersDebug (enabled);
	}

	//******************* SDK Init *******************//

	public void setUserId (string userId)
	{
		CFSetUserId (userId);
	}

	public void init (string appKey) 
	{
		CFSetPluginData ("Unity", IronSource.pluginVersion(), IronSource.unityVersion());
		CFInit (appKey);
	}

	public void init (string appKey, params string[] adUnits)
	{
		CFSetPluginData ("Unity", IronSource.pluginVersion(), IronSource.unityVersion());
		CFInitWithAdUnits (appKey, adUnits);
	}

	public void initISDemandOnly (string appKey, params string[] adUnits)
	{
		CFSetPluginData ("Unity", IronSource.pluginVersion(), IronSource.unityVersion());
		CFInitISDemandOnly (appKey, adUnits);
	}

	//******************* RewardedVideo API *******************//
	
	public void showRewardedVideo ()
	{
		CFShowRewardedVideo ();
	}

	public void showRewardedVideo (string placementName)
	{
		CFShowRewardedVideoWithPlacementName (placementName);
	}

	public bool isRewardedVideoAvailable ()
	{
		return CFIsRewardedVideoAvailable ();
	}

	public bool isRewardedVideoPlacementCapped (string placementName)
	{
		return CFIsRewardedVideoPlacementCapped (placementName);
	}

	public IronSourcePlacement getPlacementInfo (string placementName)
	{
		IronSourcePlacement sp = null;

		string spString = CFGetPlacementInfo (placementName);
		if (spString != null) {
			Dictionary<string,object> spDic = IronSourceJSON.Json.Deserialize (spString) as Dictionary<string,object>;
			string pName = spDic ["placement_name"].ToString ();
			string rewardName = spDic ["reward_name"].ToString ();
			int rewardAmount = Convert.ToInt32 (spDic ["reward_amount"].ToString ());
			sp = new IronSourcePlacement (pName, rewardName, rewardAmount);
		}

		return sp;
	}

    public void setRewardedVideoServerParams(Dictionary<string, string> parameters){
        string json = IronSourceJSON.Json.Serialize (parameters);
		CFSetRewardedVideoServerParameters (json);
	}

    public void clearRewardedVideoServerParams(){
		CFClearRewardedVideoServerParameters ();
	}

	//******************* RewardedVideo DemandOnly API *******************//

	public void showISDemandOnlyRewardedVideo (string instanceId) 
	{
		CFShowISDemandOnlyRewardedVideo(instanceId);
	}

	public void showISDemandOnlyRewardedVideo (string instanceId, string placementName)
	{
		CFShowISDemandOnlyRewardedVideoWithPlacementName(instanceId, placementName);
	}
	
	public bool isISDemandOnlyRewardedVideoAvailable (string instanceId)
	{
		return CFIsDemandOnlyRewardedVideoAvailable(instanceId);
	}

	//******************* Interstitial API *******************//

	public void loadInterstitial ()
	{
		CFLoadInterstitial ();
	}
	
	public void showInterstitial ()
	{
		CFShowInterstitial ();
	}

	public void showInterstitial (string placementName)
	{
		CFShowInterstitialWithPlacementName (placementName);
	}

	public bool isInterstitialReady ()
	{
		return CFIsInterstitialReady ();
	}

	public bool isInterstitialPlacementCapped (string placementName)
	{
		return CFIsInterstitialPlacementCapped (placementName);
	}

	//******************* Interstitial DemandOnly API *******************//

	public void loadISDemandOnlyInterstitial (string instanceId)
	{
		CFLoadISDemandOnlyInterstitial (instanceId);
	}
	
	public void showISDemandOnlyInterstitial (string instanceId)
	{
		CFShowISDemandOnlyInterstitial(instanceId);
	}

	public void showISDemandOnlyInterstitial (string instanceId, string placementName)
	{
		CFShowISDemandOnlyInterstitialWithPlacementName (instanceId, placementName);
	}

	public bool isISDemandOnlyInterstitialReady (string instanceId)
	{
		return CFIsDemandOnlyInterstitialReady (instanceId);
	}

	//******************* Offerwall API *******************//

	public void showOfferwall ()
	{
		CFShowOfferwall ();
	}

	public void showOfferwall (string placementName)
	{
		CFShowOfferwallWithPlacementName (placementName);
	}

	public void getOfferwallCredits ()
	{
		CFGetOfferwallCredits ();		
	}

	public bool isOfferwallAvailable ()
	{
		return CFIsOfferwallAvailable ();
	}

	//******************* Banner API *******************//

	public void loadBanner (IronSourceBannerSize size, IronSourceBannerPosition position)
	{
		CFLoadBanner ((int)size, (int)position);
	}
	
	public void loadBanner (IronSourceBannerSize size, IronSourceBannerPosition position, string placementName)
	{
		CFLoadBannerWithPlacementName ((int)size, (int)position, placementName);
	}
	
	public void destroyBanner ()
	{
		CFDestroyBanner ();
	}

	public void displayBanner ()
	{
		CFDisplayBanner ();
	}

	public void hideBanner ()
	{
		CFHideBanner ();
	}
	
	public bool isBannerPlacementCapped (string placementName)
	{
		return CFIsBannerPlacementCapped (placementName);
	}

	public void setSegment(IronSourceSegment segment){
		Dictionary <string,string> dict = segment.getSegmentAsDict ();
		string json = IronSourceJSON.Json.Serialize (dict);
		CFSetSegment (json);
	}

	#endregion
}
#endif
