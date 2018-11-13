using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Character",menuName = "Character")]
public class Character : ScriptableObject {
    public enum TypePrice { coin, money }
    public string nameCharacter;
    public Sprite sprite, specialSprite;
    public bool lingua;
    public Vector3 postionLingua;
    public Vector3 postionX;
    public TypePrice typePrice;
    public string NameID;
    public string price;

}
