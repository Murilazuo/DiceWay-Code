using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] PhysicsMaterial2D ice;
    [SerializeField] private Sprite[] sprites;
    public DiceData diceData;

    Animator anim;
    
    public void Initialize(DiceData dice){
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();    
        diceData = dice;

        DiceFaces diceFace = diceData.diceFace;

        spr.sprite = sprites[(int)diceFace];
        
        Vector3 newPosition = Vector3.zero;

        newPosition.x = diceData.dicePosition.z;
        newPosition.y = diceData.dicePosition.y;


        switch (diceFace){
            case DiceFaces.Switch:
                GetComponent<Switch>().enabled = true;
                anim.enabled = false;
                break;
            case DiceFaces.Fire:
            case DiceFaces.Spike:
            case DiceFaces.Spring:
                anim.SetInteger("AnimId",(int)diceFace);
                break;
            default:
                anim.enabled = false;
                break;
        }
        
        gameObject.tag = diceData.diceFace.ToString();

        transform.position = newPosition;
    }
}
