using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 3 did movement, shooting, teleporting
    // teleporting and movement together

    public int lives;

    public GameManager gameManager;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerSpeed = 6f;
        lives = 3;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ChangeLivesText(lives);
        
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
        Movement();
    }
    public void LoseALife()
    {
        lives--; 
        gameManager.ChangeLivesText(lives);
        if(lives ==0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

    }

    void Movement()
    {
        //read wasd - "horizontal" and "vertical" axis
        horizontalInput = Input.GetAxis("Horizontal"); // Needs to be capitalized
        verticalInput = Input.GetAxis("Vertical"); // Needs to be capitalized
        //translate takes in a vector(direction) multiplied by time and speed
        transform.Translate(new Vector3(horizontalInput,0,0) * Time.deltaTime * playerSpeed);

        float horizontalScreenSize = gameManager.horizontalScreenSize;
        
        //player leaves horizontally (position is x,y,z)
        if(transform.position.x > horizontalScreenSize || transform.position.x <= -horizontalScreenSize)
        {
        transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        
    }

    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // 1. what we're spawning, 2. position we spawn it at, 3. rotation we spawn it at
            Instantiate(bulletPrefab, transform.position + new Vector3 (0,1,0), Quaternion.identity);
        }
    }
}
