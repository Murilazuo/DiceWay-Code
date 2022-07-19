using UnityEngine;
using System;
public class ThrowDice : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private int diceToSpawn;
    [SerializeField] private GameObject dicePrafab;
    [SerializeField] private float timeToSpawnDice;
    [SerializeField] private Dice currentDice;

    [Header("Aim")]
    [SerializeField] private Animator handAnim;
    [SerializeField] private Vector2 clampX, clampY;
    [SerializeField] private float sensibility;
    [SerializeField] private Transform aim;

    [Header("Force")]
    [SerializeField] private float baseForce;
    [SerializeField] private float maxForce;
    [Header("End Game")]
    [SerializeField] private float timeToSaveDiceData;
    [SerializeField] private string nextScene;
    [SerializeField] private int nextLevel;
    public static Action OnEndLevel;

    private Vector3 rotation;
    private float rX, rY;

    delegate void State();
    State currentState;

    ForceGauge forceGauge;

    public static ThrowDice Instance;

    public static Action<int> OnChangeDiceCount;


    [SerializeField] GameObject[] hud;
    void DisableHud()
    { 
        foreach(GameObject o in hud)
        {
            o.SetActive(false);
        }
    }
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Instance = this;
    }
    private void Start()
    {
        forceGauge = ForceGauge.Instance;
        currentState = Hold;
        GameManager.Instance.nextLevel = nextLevel;

        OnChangeDiceCount?.Invoke(diceToSpawn);

        hud = new GameObject[3];
        hud[0] = GameObject.Find("Meter");
        hud[1] = GameObject.Find("Stars");
        hud[2] = GameObject.Find("DiceFaces");
    }
    private void Update()
    {
        currentState();
    }
    void Hold(){
        rY += Input.GetAxisRaw("Mouse X") * sensibility;
        rX += -Input.GetAxisRaw("Mouse Y") * sensibility;
        
        rX = Mathf.Clamp(rX, clampX.x, clampX.y);
        rY = Mathf.Clamp(rY, clampY.x, clampY.y);

        rotation.x = rX;
        rotation.y = rY;
        rotation.z = 0;

        transform.eulerAngles = rotation;

        if(Input.GetMouseButtonDown(0) && currentDice != null){
            currentState = SetForce;
            forceGauge.StartAnim();
        }

    }

    void SetForce(){
        if(Input.GetMouseButtonDown(0) && currentDice != null){
            OnChangeDiceCount?.Invoke(diceToSpawn);


            handAnim.SetTrigger("Throw");
            currentDice?.Throw(aim.forward,(forceGauge.GetForce() * maxForce) + baseForce);
            currentDice = null;

            if(--diceToSpawn > 0){
                Invoke(nameof(SpawnDice), timeToSpawnDice);
            }else{
                OnChangeDiceCount?.Invoke(diceToSpawn);

                Invoke(nameof(CallOnEndLevel), 2f);
                currentState = End;
                DisableHud();
                Camera.main.gameObject.GetComponent<Animator>().SetTrigger("Transition");
            }
        }
    }
    public void SaveDiceData(){
        GameManager.OnSaveStars?.Invoke();
        LevelManager.Instance.GetDiceDatas();
        SceneController.GoTo(nextScene);
    }
    void SpawnDice(){
        if(currentDice == null){
            currentDice = Instantiate(dicePrafab,transform).GetComponent<Dice>();

            OnChangeDiceCount?.Invoke(diceToSpawn);
            currentState = Hold;
        }
    }

    void CallOnEndLevel()
    {

        OnEndLevel?.Invoke();
    }

    void End(){}

}
