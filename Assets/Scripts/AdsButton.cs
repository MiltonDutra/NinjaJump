using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UnityEngine.UI.Button))]
public class AdsButton : MonoBehaviour
{
    private UnityEngine.UI.Button adButton;
    void Start()
    {
        adButton = GetComponent<UnityEngine.UI.Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(adButton)
        {
            adButton.interactable = ADS.instance.adIsReady();
        }
    }
}
