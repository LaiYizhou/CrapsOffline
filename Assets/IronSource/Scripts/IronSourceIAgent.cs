using System.Collections.Generic;

public interface IronSourceIAgent
{
	void reportAppStarted ();

	//******************* Base API *******************//

	void onApplicationPause (bool pause);

	void setAge (int age);

	void setGender (string gender);

	void setMediationSegment (string segment);

	string getAdvertiserId ();

	void validateIntegration ();

	void shouldTrackNetworkState (bool track);

	bool setDynamicUserId (string dynamicUserId);

	void setAdaptersDebug(bool enabled);

	//******************* SDK Init *******************//

	void setUserId (string userId);

	void init (string appKey);

	void init (string appKey, params string[] adUnits);

	void initISDemandOnly (string appKey, params string[] adUnits);

	//******************* RewardedVideo API *******************//

	void showRewardedVideo ();

	void showRewardedVideo (string placementName);

	bool isRewardedVideoAvailable ();
	
	bool isRewardedVideoPlacementCapped (string placementName);

	IronSourcePlacement getPlacementInfo (string name);
	
	void setRewardedVideoServerParams(Dictionary<string, string> parameters);

    void clearRewardedVideoServerParams();

    //******************* RewardedVideo DemandOnly API *******************//

	void showISDemandOnlyRewardedVideo (string instanceId);

	void showISDemandOnlyRewardedVideo (string instanceId, string placementName);

	bool isISDemandOnlyRewardedVideoAvailable (string instanceId);
    
	//******************* Interstitial API *******************//

	void loadInterstitial ();

	void showInterstitial ();

	void showInterstitial (string placementName);

	bool isInterstitialReady ();

	bool isInterstitialPlacementCapped (string placementName);

	//******************* Interstitial DemandOnly API *******************//

	void loadISDemandOnlyInterstitial (string instanceId);

	void showISDemandOnlyInterstitial (string instanceId);

	void showISDemandOnlyInterstitial (string instanceId, string placementName);

	bool isISDemandOnlyInterstitialReady (string instanceId);

	//******************* Offerwall API *******************//

	void showOfferwall ();

	void showOfferwall (string placementName);

	bool isOfferwallAvailable ();

	void getOfferwallCredits ();

	//******************* Banner API *******************//
	
	void loadBanner (IronSourceBannerSize size, IronSourceBannerPosition position);

	void loadBanner (IronSourceBannerSize size, IronSourceBannerPosition position, string placementName);

	void destroyBanner();

	void displayBanner();

	void hideBanner();

	bool isBannerPlacementCapped(string placementName);

	void setSegment(IronSourceSegment segment);
}

public static class IronSourceAdUnits
{
	public static string REWARDED_VIDEO { get { return "rewardedvideo"; } }

	public static string INTERSTITIAL { get { return "interstitial"; } }

	public static string OFFERWALL { get { return "offerwall"; } } 

	public static string BANNER { get { return "banner"; } } 
}

public enum IronSourceBannerSize
{
	BANNER = 1,
	LARGE_BANNER = 2,
	RECTANGLE_BANNER = 3,
	[System.Obsolete]
	TABLET_BANNER = 4,
	SMART_BANNER = 5
};

public enum IronSourceBannerPosition
{
	TOP = 1,
	BOTTOM = 2
};
