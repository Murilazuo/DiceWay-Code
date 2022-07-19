using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<DiceData> diceDatas;
    public Vector3 endLevel;
    public List<Vector3> stars;
    public static LevelManager Instance;
    private void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(this);
        }else Destroy(gameObject);
    }
    public void GetDiceDatas() {
        List<Dice> dices = new List<Dice>(FindObjectsOfType<Dice>());
        diceDatas = new List<DiceData>();

        for (int i = 0; i < dices.Count; i++)
        {
            if (dices[i].canPass) {
                diceDatas.Add(dices[i].GetDiceData());
            }
        }

        Star2D[] stars2d = FindObjectsOfType<Star2D>();
        stars = new List<Vector3>();

        foreach (Star2D star in stars2d)
        {
            stars.Add(star.transform.position);
        }

        endLevel = GameObject.FindGameObjectWithTag("End").transform.position;
    }
}
