using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class SystemCharacter : MonoBehaviour {

    private Character[] characters;
    public string [] nameCharactersPattern;
    public Text nameCharacter;
    public Text coinText;
    public Text priceText;
    public Button right, left, select;
    private Transform[] targetCharacter;
    private int currentCharacter = 0;
    private bool nextCharacter = false;
    private const float deltaDistanceTarget = 4f;
    private float speedX = 0.0f, smoothTime = 10f;
    private float currentX;
    private List<string> activeCharacters;
    public static SystemCharacter instance; 
    private void Start()
    {
        
        CreateCharacters();
        activeCharacters = ListaCharactersAtivos();
        CheckCharacter();
        coinText.text = "X"+PlayerPrefs.GetInt("Coins").ToString();
/*#if UNITY_STANDALONE_WIN
        smoothTime = 50f;
        Debug.Log("oerni3r3orno3iivr3");
#endif*/
        instance = this;
        
    }
    void Update() {
        if (nextCharacter) {
            float distance = Mathf.Abs(Camera.main.transform.position.x - currentX);
            if (distance <= 0.1f)
            {
                //Camera.main.transform.position = new Vector2(targetPosition.x+deltaDistanceTarget, Camera.main.transform.position.y);
                nextCharacter = false;
                return;
            }
            else
            {
                float newPosition = Mathf.SmoothDamp(Camera.main.transform.position.x, currentX, ref speedX, smoothTime * Time.deltaTime);
                Camera.main.transform.position = new Vector3(newPosition, Camera.main.transform.position.y, Camera.main.transform.position.z);
            }
        }
    }
    public void RightButton()
    {

        if (!nextCharacter)
        {
            currentCharacter++;
            if (currentCharacter < targetCharacter.Length)
            {
                currentX = targetCharacter[currentCharacter].position.x;
                nextCharacter = true;
                left.interactable = true;
                if (currentCharacter == targetCharacter.Length - 1)
                {
                    right.interactable = false;
                }
            }
            else
            {
                currentCharacter = targetCharacter.Length - 1;
            }
        }
        CheckCharacter();
        nameCharacter.text = characters[currentCharacter].nameCharacter;
    }
    public void LeftButton()
    {
        if (!nextCharacter)
        {
            currentCharacter--;
            if (currentCharacter >= 0)
            {
                currentX = targetCharacter[currentCharacter].position.x;
                nextCharacter = true;
                right.interactable = true;
                if (currentCharacter == 0)
                {
                    left.interactable = false;
                }
            }
            else
            {
                currentCharacter = 0;
            }
        }
        CheckCharacter();
        nameCharacter.text = characters[currentCharacter].nameCharacter;
    }
    private void CreateCharacters()
    {
        characters = Inventory.inventory.currentCharacter;
        GameObject parentObject = new GameObject("Character");
        int cont = 0;
        targetCharacter = new Transform[characters.Length];
        foreach (Character character in characters)
        {
            GameObject objeto = new GameObject(character.name);
            objeto.AddComponent<SpriteRenderer>();
            objeto.GetComponent<SpriteRenderer>().sprite = character.sprite;
            objeto.transform.localScale = new Vector3(0.4500741f, 0.4500741f, 0.4500741f);
            objeto.transform.position = new Vector3(cont * 4, 0, 0);
            objeto.transform.parent = parentObject.transform;
            targetCharacter[cont] = objeto.transform;
            cont++;
        }
        nameCharacter.text = characters[currentCharacter].nameCharacter;
        left.interactable = false;
    }
    public void SelectCharacter()
    {
        if (activeCharacters.Contains(characters[currentCharacter].nameCharacter))
        {
            Inventory.inventory.SetCurrentChracter(currentCharacter);
            SceneController.sceneController.LoadSceneController("Jogo");
            AudioController.audioController.SetAudioClipTheme();
        }
        else
        {
            if (characters[currentCharacter].typePrice == Character.TypePrice.coin)
            {
                int valorChar = Convert.ToInt32(characters[currentCharacter].price);
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - valorChar);
                PlayerPrefs.SetInt(characters[currentCharacter].nameCharacter, 1);
                activeCharacters.Add(characters[currentCharacter].nameCharacter);
                CheckCharacter();
                coinText.text = "X" + PlayerPrefs.GetInt("Coins").ToString();
            }else if (characters[currentCharacter].typePrice == Character.TypePrice.money) {

                Purchaser.instance.BuyProductID(characters[currentCharacter].NameID);

            }

            
        }
    }
    private List<string> ListaCharactersAtivos()
    {
        List<string> list = new List<string>();
        foreach(Character per in characters)
        {
            int active = PlayerPrefs.GetInt(per.nameCharacter);
            if (active==1) list.Add(per.nameCharacter);
        }
        foreach(string name in nameCharactersPattern)
        {
            list.Add(name);
        }
        return list;
    }
    private void CheckCharacter()
    {
        string name = characters[currentCharacter].nameCharacter;
        if (activeCharacters.Contains(name))
        {
            select.gameObject.GetComponentInChildren<Text>().text = "select";
            select.interactable = true;
            priceText.text = "";
        }
        else
        {
            if (characters[currentCharacter].typePrice == Character.TypePrice.coin)
            {
                select.gameObject.GetComponentInChildren<Text>().text = "purchase";
                int valorChar = Convert.ToInt32(characters[currentCharacter].price);
                priceText.text = "*" + valorChar.ToString();
                select.interactable = valorChar <= PlayerPrefs.GetInt("Coins");
            }else if (characters[currentCharacter].typePrice == Character.TypePrice.money)
            {
                select.interactable = true;
                select.gameObject.GetComponentInChildren<Text>().text = "purchase";
                int valorChar = Convert.ToInt32(characters[currentCharacter].price);
                priceText.text = string.Format("<color=#16FF00>{0}</color>", "$" + valorChar.ToString());
            }


        }
    }
    public void SetCurrentCharacterToInventory()
    {
        PlayerPrefs.SetInt(characters[currentCharacter].nameCharacter, 1);
        activeCharacters.Add(characters[currentCharacter].nameCharacter);
        CheckCharacter();
    }
}
