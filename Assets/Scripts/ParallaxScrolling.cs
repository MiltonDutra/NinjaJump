using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour {

    public Rigidbody2D rgbParallax;
    public Transform repeatBackGround, repeatBackGround2;
    public GameObject backGroundsAleat;
    public float speed;
    private bool flipTransform = false;
    private Transform checkTransform;
    float distance;
    private void Start()
    {
        int aleatBackGround = Random.Range(0,backGroundsAleat.transform.childCount);
        backGroundsAleat.transform.GetChild(aleatBackGround).gameObject.SetActive(true);
        checkTransform = repeatBackGround;
        distance =Mathf.Abs(repeatBackGround.localPosition.x - repeatBackGround2.localPosition.x);
    }
    void FixedUpdate () {
        if (CameraController.checkCameraPosition)
        {
            rgbParallax.velocity = new Vector2(speed*CameraController.speedX, 0);
        }
        else
        {
            rgbParallax.velocity = Vector2.zero;
        }
        if (Camera.main.transform.position.x >= checkTransform.position.x)
        {
            FlipTransform();
        }
    }
    private void FlipTransform()
    {
        if (flipTransform)
        {
            repeatBackGround.localPosition =new Vector3(repeatBackGround2.localPosition.x + distance, repeatBackGround.localPosition.y,
                repeatBackGround.localPosition.z);
            checkTransform = repeatBackGround;
            flipTransform = false;
        }
        else
        {
            repeatBackGround2.localPosition = new Vector3(repeatBackGround.localPosition.x + distance, repeatBackGround2.localPosition.y,
                repeatBackGround2.localPosition.z);
            checkTransform = repeatBackGround2;
            flipTransform = true;
        }
        
    }
}
