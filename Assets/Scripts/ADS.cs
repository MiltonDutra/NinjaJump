using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Advertisements;
#endif

public class ADS : MonoBehaviour {
    
    public static void ShowRewardedAd()
    {
#if UNITY_ANDROID
        if (Advertisement.IsReady("video"))
        {
            if (Random.Range(1, 15)==2)
            {
                var options = new ShowOptions { resultCallback = HandleShowResult };
                Advertisement.Show("video", options);
                Time.timeScale = 0;
            }
        }
#endif
    }
#if UNITY_ANDROID
    private static void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
          
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
               
        }
        Time.timeScale = 1;
    }
#endif

}
