using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceDisplay : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] List<Image> images;
    [SerializeField] Image lastImage;
    [SerializeField] List<Dice> dices;
    Dice lastDice;
    [SerializeField] RectTransform facesTransform;
    [SerializeField] GameObject facePrefab;

    public static FaceDisplay Instance;
    private void Awake() {
        Instance = this;
    } 
    void Start()
    {
        dices = new List<Dice>();
    }
    public void AddDice(Dice dice){
        if(!dices.Contains(dice)){
            dices.Add(dice);
            lastDice = dice;
        }
        SetFaceRender();
    }
    void SetFaceRender(){
        lastImage.color = Color.white;

        foreach(Dice dice in dices){
            int i = dices.IndexOf(dice);

            if(dice != lastDice){
                images[i].sprite = sprites[dice.GetHeightFace()];
                images[i].color = Color.white;
            }
        }

        lastImage.sprite = sprites[lastDice.GetHeightFace()];
        
    }
}
