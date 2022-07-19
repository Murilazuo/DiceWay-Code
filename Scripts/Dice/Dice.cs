using UnityEngine;
using System.Collections.Generic;

public class Dice : MonoBehaviour
{
    public bool canPass = true;
    Spech speech;

    [Header("LineCheck")]
    [SerializeField] LayerMask diceLayer;
    [SerializeField] Dice target;
    [SerializeField] float offset;
    [SerializeField] float size;
    LineRenderer line;

    [Header("Faces")]
    FaceDisplay faceDisplay;
    [SerializeField] private Sprite[] sprFaces;
    [SerializeField] private Vector3[] randomRotations;
    [SerializeField] Transform[] faces;


    [Header("Marker")]
    [SerializeField] private GameObject markerPrefab;
    [SerializeField] Marker marker;
    [SerializeField] Sprite cantPassSpr;

    bool stop = true;

    Rigidbody rig;
    SoundManager soundManager;
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        rig.isKinematic = true;
        soundManager = GetComponent<SoundManager>();
        line = GetComponent<LineRenderer>();

        transform.localEulerAngles = randomRotations[Random.Range(0,randomRotations.Length)];        
    }


    void CreateMarker(){
        if(marker == null){
            marker = Instantiate(markerPrefab,transform.position,Quaternion.identity).GetComponent<Marker>();
            marker.SetFollow(transform);
        }
    }
    private void Start() {
        faceDisplay = FaceDisplay.Instance;
        speech = Spech.Instance;
    }
    private void Update() {
        marker?.SetSprite(sprFaces[GetHeightFace()]);

        if(!stop && rig.isKinematic == false && rig.velocity == Vector3.zero){
            stop = true;
            DiceFaces diceFace = (DiceFaces)GetHeightFace();
            faceDisplay.AddDice(this);
            soundManager.PlayOneShotAudio(diceFace.ToString());
            speech.SetSpeech(diceFace);
        }
    }
    private void FixedUpdate() {
        if(target != null){
            line.SetPosition(0,transform.position);
            line.SetPosition(1,target.transform.position);
        }else{
            line.SetPosition(0,transform.position);
            line.SetPosition(1,transform.position);
        }

    }
    public void Throw(Vector3 dir,float force){
        soundManager.PlayOneShotAudio("Throw");

        rig.isKinematic = false;    
        Vector3 direction = dir * force;
        transform.SetParent(null);

        rig.AddForce(direction);
    }

    public int GetHeightFace(){
        int heightFace = 0;

        for (int i = 0; i < faces.Length; i++)
        {
            if(faces[i].transform.position.y >= faces[heightFace].position.y){
                heightFace = i;
            }
        }

        return heightFace; 
    }
    public DiceData GetDiceData() => new DiceData((DiceFaces)GetHeightFace(), transform.position);

    private void OnCollisionEnter(Collision other) {
        if(stop == true){
            stop = false;
            CreateMarker();
        }

        soundManager.PlayOneShotAudio("Collide");
        
    }
    void GetLeftDice(){
        Vector3 start1, start2;
        Vector3 end1, end2;
        start1 = end1 = end2 = start2 = transform.position;

        start1.z += offset;
        start2.z -= offset;

        end1.z += offset;
        end2.z -= offset;

        end1.x -= size;
        end2.x -= size;

        RaycastHit hit1, hit2;
        List<Dice> dices = new List<Dice>();

        if(Physics.Raycast(start1, Vector3.left, out hit1,size,diceLayer)){
            if(hit1.transform.gameObject.GetComponent<Dice>() != null){
                dices.Add(hit1.transform.gameObject.GetComponent<Dice>());
            }
        }
        if(Physics.Raycast(start2, Vector3.left, out hit2,size,diceLayer)){
            if(hit2.transform.gameObject.GetComponent<Dice>() != null){
                dices.Add(hit2.transform.gameObject.GetComponent<Dice>());
            }
        }

        foreach (Dice dice in dices)
        {
            if(dice != null && dice != this){
                dice.Disable();
                target = dice;
            }
        }
        
    }
    public void Disable(){
        canPass = false;
        marker.SetSprite(cantPassSpr,true);
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 start1, start2;
        Vector3 end1, end2;
        start1 = end1 = end2 = start2 = transform.position;

        start1.z += offset;
        start2.z -= offset;

        end1.z += offset;
        end2.z -= offset;

        end1.x -= size;
        end2.x -= size;

        Gizmos.DrawLine(start1,end1);
        Gizmos.DrawLine(start2,end2);
    }
    
    private void OnEnable()
    {
        ThrowDice.OnEndLevel += GetLeftDice;
    }
    private void OnDisable()
    {
        ThrowDice.OnEndLevel -= GetLeftDice;
    }
}
