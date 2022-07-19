using UnityEngine;

public enum DiceFaces{
    Normal, //0
    Switch, //1
    Spring, //2
    Spike, //3
    Fire, //4
    Ice, //5
}

[System.Serializable]
public class DiceData
{
    public DiceFaces diceFace;
    public Vector3 dicePosition;

    public DiceData (DiceFaces face, Vector3 position){
        this.diceFace = face;
        dicePosition = position;
    }
}
