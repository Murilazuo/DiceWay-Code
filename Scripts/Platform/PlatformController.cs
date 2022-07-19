using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private GameObject endPrefab;
    void Start()
    {
        LevelManager levelManager = LevelManager.Instance;
        DiceData[] dices = levelManager.diceDatas.ToArray();
        Vector3[] stars = levelManager.stars.ToArray();
        
        foreach (DiceData dice in dices)
        {
            Instantiate(platformPrefab,Vector3.zero, Quaternion.identity).GetComponent<Platform>().Initialize(dice);
        }

        foreach (Vector3 starPos in stars)
        {
            Vector2 position = Vector2.zero;
            position.x = starPos.z;
            position.y = starPos.y;

            SpriteRenderer starSpr = Instantiate(starPrefab, position, Quaternion.identity).GetComponent<SpriteRenderer>();
            Color starColor = starSpr.color;
            starColor.a = 1;
        }

        Vector3 endPosition = Vector3.zero;
        endPosition.x = levelManager.endLevel.z;    
        endPosition.y = levelManager.endLevel.y;

        Instantiate(endPrefab,endPosition, Quaternion.identity);
    }
}
