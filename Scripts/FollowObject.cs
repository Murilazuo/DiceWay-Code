using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    internal Transform toFollow;

    private void Update()
    {
        if(toFollow){
            transform.position = toFollow.position;        
        }
    }
}
