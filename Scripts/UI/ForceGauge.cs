using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceGauge : MonoBehaviour
{
    [SerializeField] private float animSpeed = 1;
    private Animator anim;
    
    [SerializeField] RectTransform meter;
    RectTransform myRect;

    public static ForceGauge Instance;
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);       
    }

    void Start()
    {
        myRect = GetComponent<RectTransform>();
        anim = GetComponent<Animator>();

        StopAnim();
    }

    public float GetForce(){
        StopAnim();
        return (meter.localPosition.y + 50f) / 100f;
    }

    public void StopAnim(){
        transform.localScale = Vector3.zero;
        anim.speed = 0;
    }
    public void StartAnim(){
        transform.localScale = Vector3.one;
        anim.speed = animSpeed;
    }
}
