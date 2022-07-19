using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static List<int> starCollect; 

    public int nextLevel;
    [SerializeField] private int stars;
    [SerializeField] private int currentStar = 0;


    public Action OnCollectStars;
    public static Action OnSaveStars;
    public static Action OnDead;
    public static GameManager Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);

            OnDead += ResetStars;
            OnSaveStars += SaveStar;

            starCollect = new List<int>();
        }
        else Destroy(gameObject);
    }
    private void OnDisable()
    {
        OnDead -= ResetStars;
        OnSaveStars -= SaveStar;
    }
    public int GetStars() => stars + currentStar;
    public void AddStar()
    {
        currentStar++;
        OnCollectStars?.Invoke();
    } 
    void SaveStar()
    {
        stars += currentStar;
        ResetStars();
    }
    void ResetStars()
    {
        currentStar = 0;
    }
    public void ResetAllStars(){
        ResetStars();
        currentStar = 0;
        starCollect.Clear();
    }
    public string NextLevel()
    {
        if (nextLevel == 6)
        {
            return "End";
        }
        return ("Fase_" + nextLevel);
    }
    public string Reset() {
        return ("Fase_" + (nextLevel - 1));
    }
    
    public bool CheckStarCollect(int toCheck)
    {
        if (starCollect.Contains(toCheck)){
            return true;
        }
        else return false;
    }
}
