using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public void Next()
    {
        Time.timeScale = 1;

        GameManager.OnSaveStars?.Invoke();

        SceneController.GoTo(gameManager.NextLevel());
    }
    public void Restart()
    {
        Time.timeScale = 1;

        SceneController.GoTo(gameManager.Reset());
    }
    public void Menu() {
        Time.timeScale = 1;
        SceneController.GoTo("MainMenu");
    }
}
