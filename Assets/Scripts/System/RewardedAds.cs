using System;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Mediation;

public class RewardedAds : MonoBehaviour
{
    public string androidAdUnitId = "Rewarded_Android";
    public string iosAdUnitId = "Rewarded_iOS";
    IRewardedAd rewardedAd;
    public string activity = "";
    public int itemSelection;

    public async void LoadRewardedAds()
    {
        // Instantiate a rewarded ad object with platform-specific Ad Unit ID
        if (Application.platform == RuntimePlatform.Android)
        {
            rewardedAd = new RewardedAd(androidAdUnitId);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            rewardedAd = new RewardedAd(iosAdUnitId);
        }
#if UNITY_EDITOR
        else
        {
            rewardedAd = new RewardedAd("myExampleAdUnitId");
        }
#endif

        // Subscribe callback methods to load events:
        rewardedAd.OnLoaded += AdLoaded;
        rewardedAd.OnFailedLoad += AdFailedToLoad;

        // Subscribe callback methods to show events:
        rewardedAd.OnShowed += AdShown;
        rewardedAd.OnFailedShow += AdFailedToShow;
        rewardedAd.OnUserRewarded += UserRewarded;
        rewardedAd.OnClosed += AdClosed;

        try
        {
            // Load an ad:
            await rewardedAd.LoadAsync();
            // Here our load succeeded.
        }
        catch (Exception e)
        {
            // Here our load failed.
            Debug.Log("reawrdeAd failed to load " + e.Message);
        }

    }

    // Implement load event callback methods:
    void AdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Ad loaded.");
        // Execute logic for when the ad has loaded
    }

    void AdFailedToLoad(object sender, LoadErrorEventArgs args)
    {
        Debug.Log("Ad failed to load.");
        // Execute logic for the ad failing to load.
    }

    // Implement show event callback methods:
    void AdShown(object sender, EventArgs args)
    {
        Debug.Log("Ad shown successfully.");
        // Execute logic for the ad showing successfully.
    }

    void UserRewarded(object sender, RewardEventArgs args)
    {
        // Execute logic for rewarding the user.
        //check activity
        switch (activity)
        {
            case "TryBuyPotion":
                GameManager.instance.RewardBuyItem(itemSelection);
                GameManager.instance.rewardedAdCount++;
                break;
            default:
                break;
        }

        Debug.Log("Ad has rewarded user.");
        Debug.Log($"Received reward: type:{args.Type}; amount:{args.Amount}");
        GameManager.instance.SaveState();
        GameManager.instance.charactermenu.UpdateMenu();
    }

    void AdFailedToShow(object sender, ShowErrorEventArgs args)
    {
        Debug.Log("Ad failed to show.");
        // Execute logic for the ad failing to show.
    }

    void AdClosed(object sender, EventArgs e)
    {
        Debug.Log("Ad is closed.");
        // Execute logic for the user closing the ad.
    }

    public async void ShowAd(string activityText, int item)
    {
        // Ensure the ad has loaded, then show it.
        if (rewardedAd.AdState == AdState.Loaded)
        {
            try
            {
                //change activity and item for reward
                activity = activityText;
                itemSelection = item;
                await rewardedAd.ShowAsync();
                // Here show succeeded.
            }
            catch (Exception e)
            {
                // Here show failed.
                Debug.Log("reawrdeAd failed to show " + e.Message);
            }
        }
    }
}