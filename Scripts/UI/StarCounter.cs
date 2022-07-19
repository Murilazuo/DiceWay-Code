using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarCounter : MonoBehaviour
{
    TMP_Text text;
    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
        text = GetComponent<TMP_Text>();
        GameManager.Instance.OnCollectStars += SetStars;

        SetStars();
    }
    private void OnDisable()
    {
        GameManager.Instance.OnCollectStars -= SetStars;
    }
    void SetStars(){
        text.text = gameManager.GetStars().ToString();
    }
}
