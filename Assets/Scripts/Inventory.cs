using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public static Inventory inventory;
    public Character[] currentCharacter;
    private int positionCurrentCharacter;
    private void Start()
    {
        if (inventory == null) {
            inventory = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        positionCurrentCharacter = PlayerPrefs.GetInt("currentCharacter");
    }
    public Character GetCurrentCharacter()
    {
        return currentCharacter[positionCurrentCharacter];
    }
    public void SetCurrentChracter(int positionCurrentCharacter)
    {
        PlayerPrefs.SetInt("currentCharacter", positionCurrentCharacter);
        this.positionCurrentCharacter = positionCurrentCharacter;
    }
}
