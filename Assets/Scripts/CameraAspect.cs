using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspect : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        Camera.main.aspect = 1080f / 1920f;
        //Screen.SetResolution(360, 640, false);
        //Screen.SetResolution(480, 1280, true);
    }
	
	
}
