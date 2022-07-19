using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    bool force;
    FollowObject followObject;
    SpriteRenderer spr;
    private void Awake()
    {
        spr = GetComponentInChildren<SpriteRenderer>();        
    }
    public void SetFollow(Transform toFollow){
        followObject = GetComponent<FollowObject>();
        followObject.toFollow = toFollow;
    }
    public void SetSprite(Sprite sprite){
        if(!force){
            spr.sprite = sprite;
        }
    }
    public void SetSprite(Sprite sprite, bool isForce){
        force = isForce;
        spr.sprite = sprite;
    }
}
