using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NinjaController : MonoBehaviour {

    private Ninja ninjaScript;
    public float forceTorque;
    public AudioClip effect1, effect2;
    public Transform x, lingua;
    private SpriteRenderer spriteRenderer;
    private Character player;
    public static NinjaController Instance;
    private bool dontUseTouch = false;
    void Start () {
        SetCharacter();
        ninjaScript = new Ninja(GetComponent<HingeJoint2D>(),
            GetComponent<Rigidbody2D>(),GetComponent<Animator>(), forceTorque, effect1);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Instance = this;
	}
	void Update () {

#if UNITY_EDITOR || UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        bool mouseInput0 = Input.GetMouseButton(0);
        bool mouseInput1 = Input.GetMouseButtonUp(0);
        if(!EventSystem.current.IsPointerOverGameObject()){
            ninjaScript.InputNinja(mouseInput0, mouseInput1);
        }
#elif UNITY_ANDROID
        bool touchPress = false;
        bool touchEnd = false;
        if (Input.touchCount > 0)
        {
            
            Touch touch = Input.GetTouch (0); 
            if(!EventSystem.current.IsPointerOverGameObject(touch.fingerId)){
                touchPress = !dontUseTouch;
                if (touch.phase == TouchPhase.Ended)
                {
                    touchEnd = !dontUseTouch;
                    dontUseTouch = false;
                }
            }else{
                dontUseTouch = true;
            }

        }
        ninjaScript.InputNinja(touchPress, touchEnd);
#endif
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ConnectedBody"))
        {
            if (ninjaScript.ConnetedRigidbody(collision.gameObject.GetComponent<Rigidbody2D>()))
            {
                CameraController.SuccessConnectedBody(collision.transform.position);
                SpawnObjectsController.spawnObjectsController.InstantiateConnectedBodyInSpace();
                GameController.gameController.Score();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ninjaScript.Dead();
            
        }
    }
    private void SetCharacter()
    {
        player = Inventory.inventory.GetCurrentCharacter();
        GetComponent<SpriteRenderer>().sprite = player.sprite;
        x.localPosition = player.postionX;
        if (player.lingua)
        {
            lingua.localPosition = player.postionLingua;
        }
        else
        {
            lingua.gameObject.SetActive(false);
        }
    }
    public void SetSpecialSprite(bool active)
    {
        if (active)
        {
            if (player.specialSprite != null)
            {
                spriteRenderer.sprite = player.specialSprite;
            }
        }
        else
        {
            spriteRenderer.sprite = player.sprite;
        }
    }
}
