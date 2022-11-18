using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    string _gameId;
    [SerializeField] bool _testMode = true;
    [SerializeField] string placementIdBanner = "Banner_Android";
    string placementIdInterstitial = "Interstitial_Android";
    string placementIdRewarded = "Rewarded_Android";
    public bool rewardPlayer = false;

    private void Awake()
    {
        if (Advertisement.isInitialized)
        {
            Debug.Log("Advertisement is Initialized");
            // call load ads
            //LoadRewardedAd();
            //LoadInerstitialAd();
        }
        else
        {
            //initialize ads - setup ads
            InitializeAds();
        }
    }
    //setup ads
    public void InitializeAds()
    {
        //set game id based on platform
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSGameId : _androidGameId;
        //set placement Id based on platform
        placementIdBanner = (Application.platform == RuntimePlatform.IPhonePlayer) ? "Banner_iOS" : "Banner_Android";
        placementIdInterstitial = (Application.platform == RuntimePlatform.IPhonePlayer) ? "Interstitial_iOS" : "Interstitial_Android";
        placementIdRewarded = (Application.platform == RuntimePlatform.IPhonePlayer) ? "Rewarded_iOS" : "Rewarded_Android";
        //intialize ads
        //this = IUnityAdsInitializationListener
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        //LoadInerstitialAd();
        LoadBannerAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
    //load inerstitial ads
    public void LoadInterstitialAd()
    {
        //load inerstitial ads - android
        //this = IUnityAdsLoadListener
        Advertisement.Load(placementIdInterstitial, this);
    }
    //load rewarded ads
    public void LoadRewardedAd()
    {
        //load rewarded ads - android
        //this = IUnityAdsLoadListener
        Advertisement.Load(placementIdRewarded, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("OnUnityAdsAdLoaded");
        //show ads
        //this = IUnityAdsShowListener
        Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }
    //when ads show fail
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure");
    }
    //when ads show start
    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("OnUnityAdsShowStart");
        //stop game
        Time.timeScale = 0;
        //hide banner ads
        Advertisement.Banner.Hide();
    }
    //when ads show clicked
    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("OnUnityAdsShowClick");
    }
    //when ads show complete
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete " + showCompletionState);
        //check if rewarded ads and ads status is complete
        if (placementId.Equals(placementIdRewarded) && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState))
        {
            Debug.Log("rewarded Player");
            //reward player
            rewardPlayer = true;
        }
        //start game
        Time.timeScale = 1;
        //show banner ads
        Advertisement.Banner.Show(placementIdBanner);
    }


    //load banner ads
    public void LoadBannerAd()
    {
        //set position ads
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        //load banner ads
        Advertisement.Banner.Load(placementIdBanner,
            new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            }
            );
    }

    void OnBannerLoaded()
    {
        Advertisement.Banner.Show(placementIdBanner);
    }

    void OnBannerError(string message)
    {

    }

}