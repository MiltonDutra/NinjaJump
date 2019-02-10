using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.touchCount>0)
        {
            gameObject.SetActive(false);
        }
    }
}
