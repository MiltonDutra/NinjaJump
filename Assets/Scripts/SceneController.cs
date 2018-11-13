using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static SceneController sceneController;
    private void Start()
    {
        if (sceneController == null) {
            sceneController = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    public void LoadSceneController(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
