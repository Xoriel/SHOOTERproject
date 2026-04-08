using UnityEngine;

public class Player : MonoBehaviour
{
    // 3 did movement, shooting, teleporting
    // teleporting and movement together

    public GameObject bulletPrefab;
    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;

    private float horizontalScreenLimit = 9.5f;
    private float verticalScreenLimit = 6.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerSpeed = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
        Movement();
    }

    void Movement()
    {
        //read wasd - "horizontal" and "vertical" axis
        horizontalInput = Input.GetAxis("Horizontal"); // Needs to be capitalized
        verticalInput = Input.GetAxis("Vertical"); // Needs to be capitalized
        //translate takes in a vector(direction) multiplied by time and speed
        transform.Translate(new Vector3(horizontalInput,verticalInput,0) * Time.deltaTime * playerSpeed);
        //player leaves horizontally (position is x,y,z)
        if(transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
        transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        if(transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        {
        transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
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
