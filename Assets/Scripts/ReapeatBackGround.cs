using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapeatBackGround : MonoBehaviour {

    public Transform background1, background2;
    public Sprite textura1, textura2;
    private Vector3 checkPosition;
    private Transform playerTransform;
    private bool chave = true;
    private void Start()
    {
        checkPosition = background1.position;
        playerTransform = GameObject.Find("Player").transform;
        if (Random.Range(0, 2)==1)
        {
            background1.GetComponent<SpriteRenderer>().sprite = textura1;
            background2.GetComponent<SpriteRenderer>().sprite = textura1;
        }
        else
        {
            background1.GetComponent<SpriteRenderer>().sprite = textura2;
            background2.GetComponent<SpriteRenderer>().sprite = textura2;
        }
    }
    private void Update()
    {
        if (playerTransform.position.x >= checkPosition.x)
        {
            if (chave)
            {
                float distanceX = background1.position.x + (background1.position.x - background2.position.x);
                background2.position = new Vector3(distanceX, background2.position.y, background2.position.z);
                checkPosition = background2.position;
                chave = false;
            }
            else
            {
                float distanceX = background2.position.x + (background2.position.x - background1.position.x);
                background1.position = new Vector3(distanceX, background1.position.y, background1.position.z);
                checkPosition = background1.position;
                chave = true;
            }
        }
    }
}
