using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using GooglePlayGames.BasicApi;
using GooglePlayGames;
#endif
using System;

public class PlayStore : MonoBehaviour
{

    public static PlayStore playStore;
    public void Start()
    {
        if (playStore == null)
        {
            playStore = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        Login();
    }
    public static void SetScore(long score, string leaderBoard)

    {
   #if UNITY_ANDROID
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            Social.ReportScore(score, leaderBoard, (sucess =>
            {
                if (sucess)
                {
                    Debug.Log("Deu certo");
                }
                else
                {
                    Debug.Log("Não deu certo");
                }
            }));
        }
#endif
    }
    public static void ShowLeaderBoard()

    {
#if UNITY_ANDROID
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            Login();
            if (PlayGamesPlatform.Instance.localUser.authenticated) Social.ShowLeaderboardUI();
        }
#endif
    }
     /*private static void Login()

     {
         try
         {
             PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
             PlayGamesPlatform.InitializeInstance(config);
             PlayGamesPlatform.Activate();
             Social.localUser.Authenticate(succes => { isLogin = succes; });
         }catch(System.Exception e)
         {
             texto.gameObject.SetActive(true);
             texto.text = e.ToString();
         }

     }*/
    public static void Login()
    {
#if UNITY_ANDROID
        
       
        try
        {
            //PlayGamesPlatform.Instance.Authenticate(succes => { isLogin = succes; });
            if (!PlayGamesPlatform.Instance.localUser.authenticated)
            {
                PlayGamesPlatform.Instance.Authenticate(succes => {  }, false);
                Debug.Log("LoginUser");
            }
            // Social.localUser.Authenticate();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
#endif
    }
   

}
