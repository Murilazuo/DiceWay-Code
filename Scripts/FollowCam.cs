using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private void FixedUpdate() {
        transform.eulerAngles = Camera.main.gameObject.transform.eulerAngles;
    }
}
