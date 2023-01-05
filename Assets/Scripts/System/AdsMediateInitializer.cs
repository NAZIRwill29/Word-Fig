using System;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Mediation;

public class AdsMediateInitializer : MonoBehaviour
{
    public string GAME_ID; //optional, we will autofetch the gameID if the project is linked in the dashboard
    public RewardedAds rewardedAds;

    public void OnInitializeClicked()
    {
        Initialize();
    }

    public async void Initialize()
    {
        try
        {
            InitializationOptions opt = new InitializationOptions();
            opt.SetGameId(GAME_ID);

            await UnityServices.InitializeAsync(opt);
            OnInitializationComplete();
        }
        catch (Exception e)
        {
            OnInitializationFailed(e);
        }
    }

    //this would likely be hooked to a UI button or a game event -TODO()
    public void ShowRewardedAd(string activityText, int item)
    {
        //call rewarded show Ads
        rewardedAds.ShowAd(activityText, item);
    }

    public void OnInitializationComplete()
    {
        // We recommend to load right after initialization according to docs
        rewardedAds.LoadRewardedAds();
        //
        Debug.Log("Init Success");
    }

    public void OnInitializationFailed(Exception e)
    {
        SdkInitializationError initializationError = SdkInitializationError.Unknown;
        if (e is InitializeFailedException initializeFailedException)
        {
            initializationError = initializeFailedException.initializationError;
        }

        Debug.Log($"{initializationError}:{e.Message}");
    }

}