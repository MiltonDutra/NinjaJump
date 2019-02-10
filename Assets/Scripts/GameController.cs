using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Animator animUI;
    public Text score, pontos, coin;
    public static GameController gameController;
    public int scoreValor=0;
    //Sistema de hit
    public GameObject hitGameObject;
    private int comboHit = 1;
    private bool contHit = false;
    private float hitTime=2f;
    private float currentTime;
    public Slider slider;
    public Image barImage;
    public Text[] hitText;
    public Color stage1, stage2, stage3;
    public Animator animHit;
    public GameObject smokePlayer;
    public GameObject playerPrefab;
    private bool secondChance = true;
	void Start () {

        gameController = this;
        secondChance = true;
        score.text = "0";
      //  SetHit();
	}
    private void Update()
    {
        if (contHit)
        {
            currentTime += Time.deltaTime;
            if (currentTime < hitTime)
            {
                slider.value = currentTime / hitTime;
            }
        }
    }

    public void GameOver()
    {
        if (secondChance)
        {
            //ADS.ShowRewardedAd();
            animUI.SetBool("SecondChance", true);
            secondChance = false;
        }
        else
        {


            string recordeValor = PlayerPrefs.GetInt("recorde").ToString();
            if (PlayerPrefs.GetInt("recorde") < scoreValor)
            {
                recordeValor = "NEW\nRECORDE\n" + scoreValor;
                PlayerPrefs.SetInt("recorde", scoreValor);
            }
            else
            {
                recordeValor = "RECORDE\n" + recordeValor;
            }
            PlayStore.SetScore(PlayerPrefs.GetInt("recorde"), GPGSIds.leaderboard_ranking);
            animUI.SetTrigger("GameOver");

            recordeValor = string.Format("<color=#16FF00>{0}</color>", recordeValor);
            pontos.text = "POINT\n" + scoreValor.ToString() + "\n" + recordeValor;
            //atual.text = scoreValor.ToString();
            smokePlayer.SetActive(false);
            hitGameObject.SetActive(false);
            StartCoroutine(CoinSetText());
            //ADS.ShowRewardedAd();
        }

    }
    public void Score()
    {
        scoreValor+= comboHit*1;
        score.text = scoreValor.ToString();
        SetHit();
    }
    IEnumerator ComboHit()
    {
        comboHit = 1;
        currentTime = 0;
        contHit = true;
        hitGameObject.SetActive(true);
        hitTime = 2f;
        HitType();
        yield return new WaitUntil(()=> currentTime >= hitTime);
        contHit = false;
        comboHit = 1;
        hitGameObject.SetActive(false);
        Ninja.comboForceTroque = 0;
        smokePlayer.SetActive(false);
        NinjaController.Instance.SetSpecialSprite(false);
    }
    private void SetHit()
    {
        if (contHit)
        {
            currentTime = 0;
            HitType();
        }
        else
        {
           
            StartCoroutine(ComboHit());
        }
    }
    private void HitType()
    {
        
        if (comboHit < 3)
        {
            Ninja.comboForceTroque += 2;
            hitTime = 2.1f;
            hitText[0].color = stage1;
            hitText[1].color = stage1;
            barImage.color = stage1;
            hitText[0].text = "combo";
            animHit.SetTrigger("hit");
        }
        else
            if (comboHit < 5)
        {
            hitTime = 2.1f;
            smokePlayer.SetActive(true);
            Ninja.comboForceTroque += 2;
            hitText[0].color = stage2;
            hitText[1].color = stage2;
            barImage.color = stage2;
            hitText[0].text = "perfect!";
            animHit.SetTrigger("hit");
        }
        else
        {
            hitTime = 2f;
            Ninja.comboForceTroque += 2;
            hitText[0].color = stage3;
            hitText[1].color = stage3;
            barImage.color = stage3;
            hitText[0].text = "fatality";
            animHit.SetTrigger("hit");
            NinjaController.Instance.SetSpecialSprite(true);
        }
        if (comboHit >= 5)
        {
            hitText[1].text = "MAX X" + comboHit.ToString();
        }
        else
        {
            hitText[1].text = "X" + comboHit.ToString();
            comboHit++;
        }
    }
    IEnumerator CoinSetText()
    {
        int coinBegin = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", coinBegin + scoreValor);
        int currentCoin = PlayerPrefs.GetInt("Coins");
        while (coinBegin < currentCoin)
        {
            coinBegin += 2;
            coin.text = "X"+coinBegin.ToString();
            yield return null;
        }
        coin.text = "X" + currentCoin.ToString();
    }
    
    public void SetResetGame()
    {
        NinjaController.Instance.ResetPlayer();
        animUI.SetBool("SecondChance", false);
    }
    
   
}
