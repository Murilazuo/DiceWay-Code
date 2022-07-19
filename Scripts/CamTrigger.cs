using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    void EndAnimation(){
        ThrowDice.Instance.SaveDiceData();
    }
}
