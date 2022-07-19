using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSet : MonoBehaviour
{
    public int id;
    void Start()
    {
        if (GameManager.Instance.CheckStarCollect(id))
        {
            Destroy(gameObject);
        }       
    }
}
