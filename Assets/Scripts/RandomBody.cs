using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBody : MonoBehaviour {

    public GameObject[] objetos;
    public GameObject checkPlayer;
	void OnEnable () {
        int objetoEscolhido = Random.Range(0, objetos.Length);
        for(int cont = 0; cont<objetos.Length; cont++)
        {
            objetos[cont].SetActive(false);
        }
        objetos[objetoEscolhido].SetActive(true);
       /* GameObject parents = new GameObject("Obstacle");
        transform.parent = parents.transform;
        checkPlayer.transform.parent = parents.transform;
        objetos[objetoEscolhido].transform.parent = parents.transform;*/

    }
    
}
