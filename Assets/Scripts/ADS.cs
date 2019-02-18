using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class ADS : MonoBehaviour
{
    public string placementId = "rewardedVideo";
    private static string gameId = "2759483";

    private bool testMode = false;
    public static ADS instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            if (Monetization.isSupported)
            {
                Monetization.Initialize(gameId, testMode);
            }

        }
        else
        {
            Destroy(gameObject);
            return;
        }

                
        

    }
    public bool adIsReady()
    {
       return Monetization.IsReady(placementId);
    }
    public void ShowAd()
    {
        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
        ad.Show(options);
    }

    void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                GameController.gameController.SetResetGame();
                //NinjaController.Instance.gameObject.SetActive(true);
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");

                GameController.gameController.GameOver();
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                GameController.gameController.GameOver();
                break;

        }
    }
    
  
}
