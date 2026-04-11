using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject cloudPrefab;

    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public float horizontalScreenSize;
    public float verticalScreenSize;

    public TextMeshProUGUI livesText;
    

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;
        
        CreateSky();

        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 1, 10);
    }

    void CreateSky()
    {
        for(int i = 0; i< 30; i++)
        {
            //thing you spawn in, vector position, rotation
            //x, y, z
            //X random between horizontal negative and positive
            //Y random between vertical nagative and positive
            //0

            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);



        }
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) *.9f, verticalScreenSize, 0), Quaternion.identity);
    }

    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) *.9f, verticalScreenSize, 0), Quaternion.identity);
    }

    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
        //"lives + 2" NOT
        //"lives 2"
    }

    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
    }

}
