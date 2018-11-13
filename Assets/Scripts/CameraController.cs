using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static bool checkCameraPosition= false;
    private static Vector3 targetPosition;
    private const float deltaDistanceTarget=3.2f;
    public static float speedX= 0.0f, smoothTime= 10f;
    private void Start()
    {
        Camera.main.aspect = 1080f / 1920f;
/*#if UNITY_STANDALONE_WIN
        smoothTime = 50f;
        Debug.Log("oerni3r3orno3iivr3");
#endif*/
    }
    void Update () {
        if (checkCameraPosition)
        {
            
            float distance = Mathf.Abs(Camera.main.transform.position.x-(targetPosition.x+deltaDistanceTarget));
            if (distance <= 0.1f)
            {
                //Camera.main.transform.position = new Vector2(targetPosition.x+deltaDistanceTarget, Camera.main.transform.position.y);
                checkCameraPosition = false;
                return;
            }
            else
            {

                float newPosition = Mathf.SmoothDamp(Camera.main.transform.position.x, targetPosition.x+deltaDistanceTarget, ref speedX, smoothTime*Time.deltaTime);
                Camera.main.transform.position = new Vector3(newPosition, Camera.main.transform.position.y, Camera.main.transform.position.z);
            }
        }
	}
    public static void SuccessConnectedBody(Vector3 targetPosition)
    {
        CameraController.targetPosition = targetPosition;
        checkCameraPosition = true;
    }
}
