using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    // 3 did movement, shooting, teleporting
    // teleporting and movement together

    public int lives;

    public GameManager gameManager;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;

    public int weaponType;
    public bool shieldActive;

    private float speed;
    private float horizontalInput;
    private float verticalInput;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shieldActive = false;
        weaponType = 1;
        speed = 6f;
        lives = 3;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ChangeLivesText(lives);
        gameManager.PlaySound(6);
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
        Movement();

    }
    public void LoseALife()
    {
        if(!shieldActive)
        {
            lives--;
        }
        if(shieldActive)
        {
            shieldPrefab.SetActive(false);
            shieldActive = false;
        } 
        gameManager.ChangeLivesText(lives);
        if(lives ==0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            gameManager.GameOver();

            Destroy(this.gameObject);
        }

    }

IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(5);
        shieldPrefab.SetActive(false);
        shieldActive = false;
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);
    }
IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5);
        speed = 5f;
        thrusterPrefab.SetActive(false);
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);
    }
IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(5);
        weaponType = 1;
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "Powerup")
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);
            switch (whichPowerup)
            {
                case 1:
                    //Picked up speed
                    speed = 10f;
                    thrusterPrefab.SetActive(true);
                    gameManager.ManagePowerupText(1);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    weaponType = 2; //Picked up double weapon
                    gameManager.ManagePowerupText(2);
                    StartCoroutine(WeaponPowerDown());
                    break;
                case 3:
                    weaponType = 3; //Picked up triple weapon
                    gameManager.ManagePowerupText(3);
                    StartCoroutine(WeaponPowerDown());
                    break;
                case 4:
                    //Picked up shield
                    //Do I already have a shield?
                    //If yes: do nothing
                    //If not: activate the shield's visibility
                    shieldPrefab.SetActive(true);
                    shieldActive = true;
                    gameManager.ManagePowerupText(4);
                    StartCoroutine(ShieldPowerDown());
                    break;
            }
        }

        if(whatDidIHit.tag == "Coin")
        {
            Destroy(whatDidIHit.gameObject);
            gameManager.PlaySound(3);
            gameManager.AddScore(1);
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        if (transform.position.x <= -horizontalScreenSize || transform.position.x > horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        if (transform.position.y <= -verticalScreenSize || transform.position.y > verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
        
    }

    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch(weaponType)
            {
                case 1:
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.Euler(0, 0, 45));
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.Euler(0, 0, -45));
                    break;
            }
            gameManager.PlaySound(4);
        }
    }
}
