using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [Header("Behavior")]
    [SerializeField] int[] layerId;
    [SerializeField] Sprite[] sprites;
    private bool isSolid = true;

    SpriteRenderer spr;
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }
    void SwitchState(){
        isSolid = !isSolid;
        int id = 1;
        if(isSolid) id = 0;
        try{
        gameObject.layer = layerId[id];
        spr.sprite = sprites[id];
        }catch{}
    }
   
    private void OnEnable()
    {
        Player.OnJump += SwitchState;
    }
    private void OnDisable()
    {
        Player.OnJump = SwitchState;
    }
}
