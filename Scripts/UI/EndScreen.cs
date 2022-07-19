using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public void GoToMenu() 
    {
        SceneController.GoTo("MainMenu");
    }

}
