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
    
    private static bool isLogin;
    public static void SetScore(long score, string leaderBoard)

    {
   #if UNITY_ANDROID
        if (isLogin)
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
        if (isLogin)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            Login();
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
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        try
        {
            PlayGamesPlatform.Instance.Authenticate(succes => { isLogin = succes; });
           // Social.localUser.Authenticate();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
#endif
    }

}
