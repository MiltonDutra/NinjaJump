using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLocalController : MonoBehaviour {

	public void LoadScene(string scene)
    {
        SceneController.sceneController.LoadSceneController(scene);
    }
    public void ShowRanking()
    {
        PlayStore.ShowLeaderBoard();
    }
    public void ShowAds()
    {
        ADS.instance.ShowAd();
        
    }
}
