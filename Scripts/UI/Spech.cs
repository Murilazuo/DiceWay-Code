using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct Speechs
{
    [SerializeField]private string name;
    [TextArea] public string[] speechs;
}
public class Spech : MonoBehaviour
{
    [SerializeField] float timeToDisable;
    [SerializeField] GameObject speechObj;
    [SerializeField] TMP_Text text;

    [SerializeField] Speechs[] speechs;

    public static Spech Instance;
    private void Awake()
    {
        Instance = this;

        speechObj.SetActive(false);
    }

    public void SetSpeech(DiceFaces face)
    {
        int random = Mathf.Clamp(Random.Range(1, speechs[(int)face + 1].speechs.Length),0, speechs[(int)face + 1].speechs.Length-1);
        
        text.text = speechs[(int)face + 1].speechs[random];

        string disable = nameof(DisableSpech);

        if (IsInvoking(disable))
        {
            CancelInvoke(disable);
        }

        speechObj.SetActive(true);
        Invoke(disable, timeToDisable);
    }

    void DisableSpech() {
        speechObj.SetActive(false);
    }
}
