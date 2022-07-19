using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuFunctions : MonoBehaviour
{
    [SerializeField] private GameObject credits;
    private bool isOpen = false;

    public void StartGame() 
    {
        if(GameManager.Instance == null)
        {
            SceneController.GoTo("Fase_1");
        }
        else{
            SceneController.GoTo(GameManager.Instance.Reset());
        }
    }

    public void ToggleCredits() 
    {
        if (isOpen)
        {
            credits.SetActive(false);
            isOpen = false;
        }
        else 
        {
            credits.SetActive(true);
            isOpen = true;
        }
    }




}
