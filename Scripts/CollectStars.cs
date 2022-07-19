using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectStars : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Star")){
            gameManager.AddStar();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Star"))
        {
            gameManager.AddStar();
            GameManager.starCollect.Add(other.gameObject.GetComponent<StarSet>().id);
            Destroy(other.gameObject);
        }
    }
   
}
