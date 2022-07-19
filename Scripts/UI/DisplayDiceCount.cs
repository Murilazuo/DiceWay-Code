using UnityEngine;
using TMPro;

public class DisplayDiceCount : MonoBehaviour
{
    TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();

        ThrowDice.OnChangeDiceCount += SetDiceToSpanw;
    }
    private void OnDisable()
    {
        ThrowDice.OnChangeDiceCount -= SetDiceToSpanw;
    }

    void SetDiceToSpanw(int dices)
    {
        text.text = dices.ToString();
    }
    
}
