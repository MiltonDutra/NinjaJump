using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNinja : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //AudioController.audioController.NinjaPlayEffect();
        }
    }
}
