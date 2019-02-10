using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
using GoogleMobileAds.Api;

public class GoogleMobileAdsScript : MonoBehaviour
{
    private RewardBasedVideoAd rewardBasedVideo;
    public static GoogleMobileAdsScript googleMobileAdsScript;
    public void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-4667817269432409~5004096622";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
            string appId = "unexpected_platform";
#endif
        if (googleMobileAdsScript == null)
        {
            googleMobileAdsScript = this;
            DontDestroyOnLoad(gameObject);
            //Monetization.Initialize(gameId, testMode);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
        this.rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        this.rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        this.rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
        this.RequestRewardBasedVideo();
    }
    public void UserOptToWatchAd()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
    }
    private void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4667817269432409/5394144712";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);

        
    }
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        GameController.gameController.SetResetGame();
    }

    public void HandleRewardBasedVideoClosed(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
        this.RequestRewardBasedVideo();
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
        GameController.gameController.GameOver();
    }
    public void HandleRewardBasedVideoLeftApplication(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
        GameController.gameController.GameOver();
    }
}*/
