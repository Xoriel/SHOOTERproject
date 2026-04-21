using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    public GameObject cloudPrefab;
    public GameObject gameOverMenu;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject powerupPrefab;
    public GameObject coinPrefab;
    public GameObject audioPlayer;

    public AudioClip powerUpSound;
    public AudioClip powerDownSound;
    public AudioClip coinSound;
    public AudioClip shootSound;
    public AudioClip explosionSound;
    public AudioClip planeLoopSound;

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI powerupText;
    public TextMeshProUGUI scoreText;
    
    private bool gameOver;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;

        score = 0;
        AddScore(0);

        gameOver = false;
        
        CreateSky();

        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 1, 10);

        StartCoroutine(SpawnPowerup());
        StartCoroutine(SpawnCoin());

        powerupText.text = "No Power-Ups";
    }

    void Update()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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

    void CreatePowerup()
    {
        Instantiate(powerupPrefab, new Vector3(Random.Range(-horizontalScreenSize * .8f, horizontalScreenSize * .8f), Random.Range(-verticalScreenSize * .8f, verticalScreenSize *.8f), 0), Quaternion.identity);
    }

    void CreateCoin()
    {
        Instantiate(coinPrefab, new Vector3(Random.Range(-horizontalScreenSize * .8f, horizontalScreenSize * .8f), Random.Range(-verticalScreenSize * .8f, verticalScreenSize *.8f), 0), Quaternion.identity);
    }

    IEnumerator SpawnPowerup()
    {
        float spawnTime = Random.Range(5, 7);
        yield return new WaitForSeconds(spawnTime);
        CreatePowerup();
        StartCoroutine(SpawnPowerup());
    }

    IEnumerator SpawnCoin()
    {
        float spawnTime = Random.Range(7, 9);
        yield return new WaitForSeconds(spawnTime);
        CreateCoin();
        StartCoroutine(SpawnCoin());
    }

    public void ManagePowerupText(int powerupType)
    {
        switch(powerupType)
        {
            case 1:
                powerupText.text = "Speed";
                break;
            case 2:
                powerupText.text = "Double Weapon!";
                break;
            case 3:
                powerupText.text = "Triple Weapon!";
                break;
            case 4:
                powerupText.text = "Shield!";
                break;
            default:
                powerupText.text = "No Power-Ups";
                break;
        }
    }

    public void PlaySound(int whichSound)
    {
        switch(whichSound)
        {
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerUpSound, 0.7f);
                break;
            case 2: 
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerDownSound, 0.4f);
                break;
            case 3:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(coinSound, 0.6f);
                break;
            case 4:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(shootSound, 0.8f);
                break;
            case 5:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(explosionSound, 0.3f);
                break;
            case 6:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(planeLoopSound, 0.05f);
                break;
        }
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
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        //set our game over object menu to true
        gameOverMenu.SetActive(true);
        //game over to be true
        gameOver = true;

    }
}
