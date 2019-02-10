using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja {
    private HingeJoint2D hindeJoint;
    private Rigidbody2D rgbNinja;
    public static bool desconected;
    private float forceTorque;
    private const float multiForce= 600f;
    private Rigidbody2D currentRgbConneted;
    private const float raioAnchor= 2.39f;
    private Animator animNinja;
    private bool gameOver=false;
    public static int comboForceTroque=0;
    private AudioClip effect;
   
    //public UnityEngine.UI.Text texto;
    public Ninja(HingeJoint2D hindeJoint, Rigidbody2D rgbNinja,Animator animNinja, 
        float forceTorque, AudioClip effect )
    {
        this.effect = effect;
        this.animNinja = animNinja;
        this.hindeJoint = hindeJoint;
        this.rgbNinja = rgbNinja;
        this.forceTorque = forceTorque*multiForce;
        this.currentRgbConneted = hindeJoint.connectedBody;
        desconected = false;
        //texto = GameObject.Find("Texto").GetComponent<UnityEngine.UI.Text>();
    }
    public void InputNinja(bool torqueInput, bool jumpInput)
    {
        if (!gameOver)
        {
            if (!desconected)
            {
                if (torqueInput)
                {
                    rgbNinja.AddTorque(forceTorque * Time.deltaTime+comboForceTroque);
                    if (rgbNinja.angularVelocity >= 400f)
                    {
                        rgbNinja.angularVelocity = 400f;
                    }
                    //texto.text = rgbNinja.angularVelocity.ToString(); 
                }

                if (jumpInput)
                {
                    currentRgbConneted.angularVelocity = 0;
                    hindeJoint.enabled = false;
                    desconected = true;
                    Vector3 direction = rgbNinja.transform.right;
                    direction.Normalize();
                    //Debug.Log(direction);
                    rgbNinja.AddForce(direction * 3700);
                    try
                    {
                        AudioController.PlayOneShotAudio(effect, AudioController.TypeSound.Effects);
                    }
                    catch (System.Exception)
                    {
                        Debug.Log("Erro");
                    }
                }

            }
            else
            {
                Vector3 direction = rgbNinja.velocity;
                direction.Normalize();
                float speed = Vector3.Cross(direction, -rgbNinja.transform.right).z;
                rgbNinja.angularVelocity = speed * 1000;
            }
            CheckGameOver();
        }
    }
    public bool ConnetedRigidbody(Rigidbody2D rgbConnected)
    {
        if (!rgbConnected.Equals(currentRgbConneted))
        {

            //MonoBehaviour.Destroy(currentRgbConneted.gameObject.transform.parent.gameObject, 0.5f);
            SpawnObjectsController.spawnObjectsController.DesactiveObstacle(currentRgbConneted.transform.parent.gameObject, 0.5f);
            hindeJoint.connectedBody = rgbConnected;
            hindeJoint.enabled = true;
            desconected = false;
            currentRgbConneted = rgbConnected;
            return true;
        }
        return false;
    }
    public void ForceConectedCurrentObstacle()
    {
        
        rgbNinja.transform.position = currentRgbConneted.position;
        hindeJoint.connectedBody = currentRgbConneted;
        desconected = false;
        gameOver = false;

        rgbNinja.velocity = Vector2.zero;
        animNinja.SetBool("Dead", false);
        GameController.gameController.smokePlayer.SetActive(false);
        currentRgbConneted.angularVelocity = 0;
        rgbNinja.angularVelocity = 0;
        hindeJoint.enabled = true;
    }
    private void CheckGameOver()
    {
        if (rgbNinja.transform.position.x >= (Camera.main.transform.position.x + 5.62f)||
            rgbNinja.transform.position.x <= (Camera.main.transform.position.x - 5.62f))
        {
            Dead();
        }
    }
    public void Dead()
    {
        if (!gameOver)
        {
            animNinja.SetBool("Dead", true);
            GameController.gameController.GameOver();
            gameOver = true;
        }
    }
    
}
