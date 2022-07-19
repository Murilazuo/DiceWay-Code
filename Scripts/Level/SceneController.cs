using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static string currentScene;
    public static void GoTo(string sceneName){
        if(sceneName == "MainMenu") GameManager.Instance.ResetAllStars();

        currentScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }
    public static void Reload(){
        GoTo(currentScene);
    }
}
